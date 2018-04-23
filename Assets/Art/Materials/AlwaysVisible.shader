// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/AlwaysVisible" {
	Properties {
		_Outline ("Outline width", Range (0.0, 0.03)) = .005
		_Color ("Always Visible Color", Color) = (1,1,1,1)
		_ColorOutline("Outline color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 100

		PASS
		{

			Cull off
			Zwrite off
			Ztest Always
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appData {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
			};

			float4 _Color;

			v2f vert(appData v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET
			{
				return _Color;
			}
			ENDCG
		}
		PASS {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		struct appData {
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		struct v2f {
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
			float3 color: COLOR;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		uniform float _Outline;
		uniform float4 _OutlineColor;

		v2f vert(appData v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);

			float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
			float2 offset = TransformViewToProjection(norm.xy);
 
			o.vertex.xy += offset * o.vertex.z * _Outline;
			o.color = _OutlineColor;
			return o;
		}

		fixed4 frag(v2f i) : SV_TARGET
		{
			fixed4 col = tex2D(_MainTex, i.uv);
			return col;
		}
		ENDCG
	}
}
	FallBack "Diffuse"
}
