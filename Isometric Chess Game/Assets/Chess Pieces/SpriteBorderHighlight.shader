Shader "Custom/SpriteBorderHighlight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BorderColor ("Border Color", Color) = (1, 0, 0, 1) // Red by default
        _BorderWidth ("Border Width", Float) = 0.01 // Width of the border
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
            float4 _BorderColor;
            float _BorderWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                float2 uv = i.uv;

                // Check if the current pixel is near the edge of the sprite
                float border = 0.0;
                if (uv.x < _BorderWidth || uv.x > 1.0 - _BorderWidth ||
                    uv.y < _BorderWidth || uv.y > 1.0 - _BorderWidth)
                {
                    border = 1.0;
                }

                // Blend the border color with the sprite's texture
                fixed4 finalColor = lerp(texColor, _BorderColor, border);
                return finalColor;
            }
            ENDCG
        }
    }
}