Shader "Custom/CheckerBoard"
{
    Properties
    {
        col1 ("Color1", Color) = (1,1,1)
        col2 ("Color2", Color) = (1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Scale ("scale", int) = 24
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }

 SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            float3 col1; 
            float3 col2;
            int _Scale;
            static const float rows = _Scale;
            static const float columns = _Scale;
     
            float4 frag(v2f_img i) : COLOR
            {
                float total = floor(i.uv.x * rows) + floor(i.uv.y * columns);
                return float4(lerp(col1, col2, step(fmod(total, 2.0), 0.5)), 1.0);
            }
            ENDCG
        }
    }

    /*
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG

    }
    */
    //FallBack "Diffuse"

   FallBack "Hidden/Shader Graph/FallbackError"
}
