Shader "Unlit/Fluids"
{
    Properties
    {
        _FluidColor("Fluid Color",Color)=(0,0,0,0)
        _FluidTex("Fluid Texture",2D)="white" {}
        _TextureInfluence("Texture Influence", Range(0,1))=0.3
        _MainTex ("Texture", 2D) = "white" {}
        _FluidsMask("Texture",2D)="white" {}
        _FluidBorder("Fluid Border",Float)=0.4
        _FluidBorderWidth("Border Width",Float )=0.02
        _FluidBorderColor("Border Color" , Color) =(0,0,0,0)
        _Speed("Texture Speed",Float)=0.1
        _Wave("Wave",Float)=2
        _WaveSpeed("Wave Speed",Float)=0.1
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
            sampler2D _FluidsMask;
            sampler2D _FluidTex;
            float4 _FluidTex_ST;
            float4 _MainTex_ST;
            float4 _FluidColor;
            float4 _FluidBorderColor;
            float _FluidBorder;
            float _TextureInfluence;
            float _Speed;
            float _FluidBorderWidth;
            float _Wave,_WaveSpeed;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float a=tex2D(_FluidsMask,i.uv).r;
                float4 col=tex2D(_MainTex,i.uv);
                if(a<_FluidBorder){
                    if(a>_FluidBorder-_FluidBorderWidth){
                        float x=1-((_FluidBorder-a)/_FluidBorderWidth);
                        return _FluidBorderColor*x+col*(1-x);
                    }
                    return col;
                }else{
                    float2 uv=float2(i.vertex.x*_FluidTex_ST.x*0.01,i.vertex.y*_FluidTex_ST.y*0.01);
                    uv.x+=+_Time.x*_Speed;
                    uv.y+=sin(_Time.w*_WaveSpeed)*_Wave;
                    float4 fluidTex=tex2D(_FluidTex,uv);
                    return  fluidTex*_TextureInfluence+_FluidColor*(1-_TextureInfluence);
                }
            }
            ENDCG
        }
    }
}
