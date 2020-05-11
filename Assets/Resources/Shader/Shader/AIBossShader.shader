Shader "Custom/AIBossShader" {
	Properties{
		_Color("Color",Color)=(1,1,1,1)
		[HDR]
		_AmbientColor("AmbientColor",Color) = (0.4,0.4,0.4,1)
			[HDR]
		_SpecularColor("SpecularColor",Color) = (0.9,0.9,0.9,1)
			[HDR]
			_RimColor("RimColor",Color) = (1,1,1,1)
			_RimThreshold("Rim Threshold",Range(0, 1)) =0.1
		

	}
	SubShader{

	Pass
	{

	



		CGPROGRAM


		#pragma vertex vert
		#pragma fragment frag
	
#pragma multi_compile_fwdbase
	
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "AutoLight.cginc"

			struct appdata
		{
		float4 vertex : POSITION;
		float3 normal : NORMAL;

		SHADOW_COORDS(2)
		};
			struct v2f
			{
			float4 pos : SV_POSITION;
			float3 worldNormal : NORMAL;
		
			float3 viewDir : TEXCOORD1;



			};



			float4 _Color;

			float4 _AmbientColor;

			float4 _SpecularColor;
		

		float4 _RimColor;
		float _RimAmount;
		float _RimThreshold;
		v2f vert(appdata v)
		{
			v2f o;

			o.pos = UnityObjectToClipPos(v.vertex);
			o.worldNormal =UnityObjectToWorldNormal (v.normal) ;
	
			o.viewDir = WorldSpaceViewDir(v.vertex);



			return o;
		}

		float4 frag(v2f i): SV_TARGET
		{
			float3 normal = normalize(i.worldNormal);
			float3 viewDir = normalize(i.viewDir);

			float NDotL = dot(_WorldSpaceLightPos0, normal);


			float shadow = saturate(NDotL);

			float LightIntersity = smoothstep(0, 0.01, NDotL * shadow);

			float4 _light = _LightColor0 * LightIntersity;


			float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);


			float NDotH = dot(normal,halfVector);


			float specularIntensity = saturate(NDotH);

			float specularIntensitySmooth = smoothstep(0, 0.01, specularIntensity);

			float4 _specular = specularIntensitySmooth * _SpecularColor;

			float rimDot =1 - dot(viewDir,normal);
			
			float rimIntensity = rimDot * pow(NDotL, _RimThreshold);
			rimIntensity = smoothstep(_RimAmount-0.01,_RimAmount+ 0.01,rimIntensity);
			float4 _rim = rimIntensity * _RimColor;


			return (_light + _specular + _rim) *_Color;

		}
		ENDCG
	}
	}
	FallBack "Diffuse"
}
