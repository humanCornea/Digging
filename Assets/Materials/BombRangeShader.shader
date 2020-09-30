Shader "Unlit/BombRangeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _radius  ("Radius", Float) = 0.4

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100


        Pass
        {
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed _radius;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed radius = _radius * 0.01; // * 0.01 はBombPrefabの大きさとの調整値
                fixed r = distance(i.uv, fixed2(0.5, 0.5));
                fixed4 red = fixed4(1,0,0,1);
                red.a = 0.1;
                fixed4 trans = 0;
                trans.a = 0;
                fixed4 col = lerp(red,trans,smoothstep(radius, radius + 0.001, r));
                                
                return col;
            }
            ENDCG
        }
    }
}
