Shader "Custom/ShaderGround"
{

	Properties
	{
		_MainTex("MainTex ",2D)= "white "{}
	[NoScaleOffset] _FlowMap("Flow (RG)", 2D) = "black" {}

		_Color("Color",Color) =(1,1,1,1)
		_Glossiness("Glossiness",Range(0,1))=0.5
		_Metallic("Metallic",Range(0,1)) = 0.5
	}


	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		sampler2D _MainTex;

		sampler2D _FlowMap;
	struct Input {
		float2 uv_MainTex;
	};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;


	float3 FlowUV(float2 uv, float2 flowVector, float time) {

		float process = frac(time);
		float3 uvw;
		uvw.xy = uv - flowVector * process;
		uvw.z = 1;
		return uvw;
	}



	void surf(Input IN, inout SurfaceOutputStandard o) {
		float2 flowVector = tex2D(_FlowMap,IN.uv_MainTex).rg * 2 - 1;
		float3 uvw = FlowUV(IN.uv_MainTex, flowVector, _Time.y);


		fixed4 c = tex2D(_MainTex, uvw.xy )* uvw.z * _Color;
		o.Albedo = c.rgb;
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
