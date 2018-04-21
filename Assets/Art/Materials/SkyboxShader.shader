// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Skybox/Gradient Skybox2"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1, 1, 1, 0)
        _Color2 ("Color 2", Color) = (1, 1, 1, 0)
        _Color3 ("Color 3",Color) = (1,1,1,0)
        _Color4 ("Color 4",Color) = (1,1,1,0)
        _DayFactor ("Day factor",Float) = 0
        _ContractionFactor ("Contraction factor",Float) = 0
        _OffsetY ("Offset Y",Float) = 0
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    struct appdata
    {
        float4 position : POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    struct v2f
    {
        float4 position : SV_POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    half4 _Color1;
    half4 _Color2;
    half4 _Color3;
    half4 _Color4;

    float _DayFactor;
    float _ContractionFactor;
    float _OffsetY;

    v2f vert (appdata v)
    {
        v2f o;
        o.position = UnityObjectToClipPos (v.position);
        o.texcoord = v.texcoord;
        return o;
    }
    
    fixed4 frag (v2f i) : COLOR
    {
    	float f;

    	_DayFactor = clamp(_DayFactor,0.0,1.0);
    	float4 colorUp = lerp(_Color1,_Color3,_DayFactor);
    	float4 colorDown = lerp(_Color2,_Color4,_DayFactor);

    	f = 1.0-clamp(1.0-_ContractionFactor*i.texcoord.y+_OffsetY,0.0,1.0);
        return lerp(colorUp,colorDown,f);
    }

    ENDCG

    SubShader
    {
        Tags { "RenderType"="Background" "Queue"="Background" }
        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
