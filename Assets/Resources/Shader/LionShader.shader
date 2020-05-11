Shader "Custom/LionShader" {
	Properties {
		_TintColor("TintColor",Color) =(1,1,1,1)
		_Transparency("Transparency",Range(0,0.5)) = 0.25



	}
		SubShader{
				Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
				LOD 100

				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha

				Pass
			{
				CGPROGRAM
					#pragma vertex vert
					#pragma frag fragment 

			#include "UnityCG.cginc"


			
		struct appdata
		{
			float4 vertex: POSITION;
			float uv : TEXCOORD0;

		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex: SV_POSITION;

		};


		float4 _TintColor;
		float  _Transparency;
		float _CutoutThresh;
		float _Distance;
		float _Amplitude;
		float _Speed;
		float _Amount;


		



		

		
		ENDCG




		}


	
	}
	FallBack "Diffuse"
}
