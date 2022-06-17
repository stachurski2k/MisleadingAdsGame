Shader "Unlit/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _Weights[6];

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv.xy, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               float3 col=tex2D(_MainTex,i.uv)*_Weights[0];
                for(float j=1;j<6;j+=1){
                    col+=tex2D(_MainTex,i.uv+float2(_MainTex_TexelSize.x*j,0)).rgb*_Weights[j];
                    col+=tex2D(_MainTex,i.uv-float2(_MainTex_TexelSize.x*j,0)).rgb*_Weights[j];
                }
                return float4(col,1);
            }
            ENDCG
        }
        GrabPass{}
        Pass{
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

            sampler2D _GrabTexture;
            float4 _GrabTexture_ST;
            float4 _GrabTexture_TexelSize;
            float _Weights[6];

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =  TRANSFORM_TEX(v.uv.xy, _GrabTexture);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 col=tex2D(_GrabTexture,i.uv)*_Weights[0];
                for(float j=1;j<6;j+=1){
                    col+=tex2D(_GrabTexture,i.uv+float2(0,_GrabTexture_TexelSize.x*j)).rgb*_Weights[j];
                    col+=tex2D(_GrabTexture,i.uv-float2(0,_GrabTexture_TexelSize.x*j)).rgb*_Weights[j];
                }
                return float4(col,1);
            }
            ENDCG
        }
    }
}
