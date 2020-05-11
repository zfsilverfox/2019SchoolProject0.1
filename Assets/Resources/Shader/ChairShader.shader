Shader "Custom/ChairShader" {
	Properties {
		
		_MainTex("MainTex", 2D) = "white" {}
	_Color("Color",Color) =(1,1,1,1)
		[HDR]
		_AmbientColor("Ambient Color",Color) =(0.4,0.4,0.4,1)
			[HDR]
		_SpecularColor("Specular", Color) =(0.9,0.9,0.9,1)

	
	}
		SubShader{

				Pass
			{
				Tags
				{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
				}


				CGPROGRAM
				// Physically based Standard lighting model, and enable shadows on all light types
		#pragma vertex vert
#pragma fragment frag 

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
#include "AutoLight.cginc"

		struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};
		struct v2f
		{
			float4 pos : SV_POSITION;
			float3 worldNormal :NORMAL;
			float2 uv : TEXCOORD0;
			float3 viewDir : TEXCOORD1;
			SHADOW_COORDS(2)
		};
		sampler2D _MainTex;
		float4 _MainTex_ST;
		v2f vert(appdata v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.worldNormal = UnityObjectToWorldNormal(v.normal);
			o.viewDir = WorldSpaceViewDir(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv,_MainTex);
			TRANSFER_SHADOW(o)
			return o;
		}
		float4 _Color;
		float4 _AmbientColor;
		float4 _SpecularColor;
		float4 _RimColor;
		float _RimAmount;
		float _RimThreshold;
		float _Glossiness;

		float4 frag(v2f i) : SV_Target
		{
			float3 normal = normalize(i.worldNormal);
			float3 dir = normalize(i.viewDir);

			float NDotL = dot(_WorldSpaceLightPos0, normal);
			float shadow = SHADOW_ATTENUATION(i);
			// Partition the intensity into light and dark, smoothly interpolated
			// between the two to avoid a jagged break.
			float lightIntensity = smoothstep(0, 0.01, NDotL * shadow);
			// Multiply by the main directional light's intensity and color.
			float4 light = lightIntensity * _LightColor0;
			float3 halfVector = normalize(_WorldSpaceLightPos0 + dir);
			float NDotH = dot(normal, halfVector);
			float specularIntensity = pow(NDotH *lightIntensity, _Glossiness *_Glossiness);
			float specularSmoothDamp = smoothstep(0, 0.01, specularIntensity);
			float4 specularColor = specularSmoothDamp * _SpecularColor;

			float rimDot = 1 - dot(dir,normal);

			float rimIntensity = rimDot * pow(NDotL, _RimThreshold);

			rimIntensity = smoothstep(_RimAmount -0.01, _RimAmount + 0.01,rimIntensity);

			float4 rim = rimIntensity * _RimColor;

			float4 sample = tex2D(_MainTex, i.uv);

			return  (light + _AmbientColor + specularColor + rim) *_Color *sample;

		}

	


		

		ENDCG
		}

		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}

}
