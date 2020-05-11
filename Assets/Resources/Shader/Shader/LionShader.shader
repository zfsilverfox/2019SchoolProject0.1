Shader "Custom/LionShader" {
	Properties {
		_TintColor("TintColor",Color) =(1,1,1,1)
		_Transparency("Transparency",Range(0,0.5)) = 0.25
		_Speed("Speed",Float) = 1
		_Amplitude("Amplitude", Float) = 1
		_Distance("Distance",Float) = 1
		_Amount("Amount",Range(0.0,1.0)) = 0.5

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
					#pragma fragment frag

			#include "UnityCG.cginc"


			
		struct appdata
		{
			float4 vertex: POSITION;
		

		};

		struct v2f
		{
		
			float4 vertex: SV_POSITION;

		};


		float4 _TintColor;
		float  _Transparency;
		float _CutoutThresh;
		float _Distance;
		float _Amplitude;
		float _Speed;
		float _Amount;


		v2f vert(appdata v)
		{
			v2f o;
			v.vertex.x += sin(_Time.y * _Speed * _Amplitude) * _Distance * _Amount;
			o.vertex = UnityObjectToClipPos(v.vertex);
			return o;
		}


		fixed4 frag(v2f i ): SV_TARGET
		{
			fixed4 col = _TintColor;
		col.a = _Transparency;


		return col;

		}

		



		

		
		ENDCG




		}


	
	}
	FallBack "Diffuse"
}
