Shader "Unlit/AlphaRevealOptimized"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Cutoff("Cutoff", Range(0,1)) = 1
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
            LOD 100

            Pass
            {
                // Set blending mode to optimize for transparency
                Blend SrcAlpha OneMinusSrcAlpha
                ZWrite Off // Disable depth writing for transparent objects
                Cull Off   // Render both sides (if necessary, depending on sprite usage)

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0 // Target shader model 2.0 for compatibility with older devices

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _Cutoff;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                // Instead of discard, use alpha to simulate clipping (better for performance)
                if (i.uv.y < _Cutoff)
                    col.a = 0; // Make the pixel transparent instead of discarding

                return col;
            }
            ENDCG
        }
        }

            // FallBack is turned off to avoid adding unnecessary passes
                FallBack Off
}
