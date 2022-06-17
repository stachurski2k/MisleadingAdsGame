Shader "Unlit/BlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass//Horizontal
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Weights[6];

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =  o.vertex.xy / 2 + 0.5;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float col=tex2D(_MainTex,i.uv)*_Weights[0];
                for(float j=1;j<6;j+=1){
                    col+=tex2D(_MainTex,i.uv+float2(_MainTex_TexelSize.x*j,0)).r*_Weights[j];
                    col+=tex2D(_MainTex,i.uv-float2(_MainTex_TexelSize.x*j,0)).r*_Weights[j];
                }
                return float4(col,0,0,1);
            }
            ENDCG
        }
        GrabPass{}

        Pass//Vertical
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            float _Weights[6];

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =  o.vertex.xy / 2 + 0.5;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float col=tex2D(_GrabTexture,i.uv)*_Weights[0];
                for(float j=1;j<6;j+=1){
                    col+=tex2D(_GrabTexture,i.uv+float2(0,_GrabTexture_TexelSize.x*j)).r*_Weights[j];
                    col+=tex2D(_GrabTexture,i.uv-float2(0,_GrabTexture_TexelSize.x*j)).r*_Weights[j];
                }
                return float4(col,0,0,1);
            }
            ENDCG
        }
    }
}
