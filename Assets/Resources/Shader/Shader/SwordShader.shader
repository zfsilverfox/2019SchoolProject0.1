Shader "Custom/SwordShader" {
	Properties {
		
		_Diffuse("Diffuse",Color) =(1,1,1,1)

	}
	SubShader {
		Pass
		{
		CGPROGRAM

			#pragma vertex vert 
			#pragma fragment frag 

			#include "Lighting.cginc"

		
		struct appdata 
		{
		float4 vertex: POSITION;
		float3 normal: NORMAL;
		};


		struct v2f
		{
			float4  pos: SV_POSITION;
			fixed3 color : COLOR;

		};

		fixed4 _Diffuse;


		v2f vert(appdata v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);

			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

			fixed3 normal = normalize(v.normal);


			fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb* normal;
			o.color = ambient + diffuse ;

			return o;
		}

		fixed4 frag(v2f i): SV_TARGET
		{

			return fixed4(i.color,1);
		}





	
		ENDCG
		}


		
	}
	FallBack "Diffuse"
}
