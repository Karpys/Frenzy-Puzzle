Shader "Custom/UnlitAlpha"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color (RGBA)", Color) = (1, 1, 1, 1)
        _Alpha ("Alpha Override", Range(0.0, 1.0)) = 1.0
    }

    SubShader
    {
        Tags
        {
            "Queue"           = "Transparent"
            "RenderType"      = "Transparent"
            "IgnoreProjector" = "True"
        }

        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float4 pos    : SV_POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4    _MainTex_ST;
            float4    _Color;
            float     _Alpha;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos   = UnityObjectToClipPos(v.vertex);
                o.uv    = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                UNITY_TRANSFER_FOG(o, o.pos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;
                col *= i.color;
                col.a *= _Alpha;
                return col;
            }
            ENDCG
        }
    }

    FallBack "Transparent/VertexLit"
}
