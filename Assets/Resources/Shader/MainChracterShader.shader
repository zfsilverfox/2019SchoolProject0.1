Shader "Unlit/MainChracterShader"
{
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		_Glossiness("Glossiness", Float) = 32
	}
		SubShader{
		Pass{
		Tags{ "LightMode" = "ForwardBase" }
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"
		float4 _Color;

	float4 _AmbientColor;
	float	_Glossiness;
	struct a2v {
		float4 vertex : POSITION;
		float4 uv : TEXCOORD0;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		float3 viewDir : TEXCOORD1;
		float3 worldNormal:NORMAL;
		SHADOW_COORDS(2)
	};

	v2f vert(a2v v)
	{
		v2f o;
		// Transform the vertex from object space to projection space
		o.pos = UnityObjectToClipPos(v.vertex);
		o.viewDir = WorldSpaceViewDir(v.vertex);
		o.worldNormal = UnityObjectToWorldNormal(v.normal);
		return o;
	}

	float4  frag(v2f i) : SV_Target
	{
		float3 normal = normalize(i.worldNormal);
		float3 viewDir = normalize(i.viewDir);
		float NdotL = saturate(dot(_WorldSpaceLightPos0, normal));
		float shadow = SHADOW_ATTENUATION(i);
		float lightIntensity = smoothstep(0, 0.01, NdotL*shadow);
		float4 light = lightIntensity * _LightColor0;
		return (_Color)*(_AmbientColor+ lightIntensity);
	}
		ENDCG
	}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}

}
