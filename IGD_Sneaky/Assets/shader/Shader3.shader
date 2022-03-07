// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Shader3"
{

    Properties{

        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (0,0,0,1)

    }
    SubShader {

        Pass {
            CGPROGRAM

            #pragma vertex myvert
			#pragma fragment myfrag
            #include "UnityCG.cginc"

            float4 _Color1;
            float4 _Color2;

            float4 myvert(float4 position : POSITION, out float3 localPosition : TEXCOORD0, float2 uv : TEXCOORD0) : SV_POSITION{
                localPosition = position.xyz;
                return UnityObjectToClipPos(position);
            }
            float4 myfrag(float4 position : SV_POSITION, float3 localPosition : TEXCOORD0
			) : SV_TARGET{
                return float4(localPosition, 1);
            }

            ENDCG
        }
    }
}
