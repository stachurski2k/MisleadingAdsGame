using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrawer : IProcessor
{
    [SerializeField] LayerMask fluidLayer;
    [SerializeField] Shader fluidMaskShader;
    [SerializeField] int blurRecursion;
    [SerializeField] Material blurMaterial;
    [SerializeField] FluidData[] fluids;
    Camera fluidCamera;
    RenderTexture tex,handle1,handle2;
    float[] weights={0.227027f, 0.1945946f, 0.1216216f, 0.054054f, 0.016216f,0.00574053f};
    private void Start()
    {
        float d=(float)Screen.width/Screen.height;
        fluidCamera=new GameObject("fluidCamera").AddComponent<Camera>();
        tex=new RenderTexture((int)(256.0f*d),256,0,RenderTextureFormat.R16);
        handle1=new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.ARGB64);
        handle2=new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.ARGB64);

        foreach (var item in fluids)
        {
            item.Initialize(d);
        }
        fluidCamera.CopyFrom(Camera.main);
        fluidCamera.targetTexture=tex;
        fluidCamera.cullingMask=fluidLayer;
        fluidCamera.backgroundColor=new Color(0,0,0,0);
        fluidCamera.enabled=false;

        blurMaterial.SetFloatArray("_Weights",weights);
    }
    public override void Proccess(RenderTexture src, RenderTexture dest)
    {
        fluidCamera.enabled=true;
        for(int i=0;i<fluids.Length;i++){
            FluidData current=fluids[i];
            fluidCamera.cullingMask=current.config.fluidLayer;
            fluidCamera.RenderWithShader(fluidMaskShader,"");
            
            Graphics.Blit(tex,current.h1,blurMaterial);
            for(int j=1;j<blurRecursion;j++){
                if(j%2==0){
                    Graphics.Blit(current.h2,current.h1,blurMaterial);
                }else{
                    Graphics.Blit(current.h1,current.h2,blurMaterial);
                }
            }
        }
        fluidCamera.enabled=false;

        Graphics.Blit(src,handle2);
        for(int i=0;i<fluids.Length;i++){
            FluidData current=fluids[i];
            if(blurRecursion%2==0){
                current.config.fluidMaterial.SetTexture("_FluidsMask",current.h1);
            }else{
                current.config.fluidMaterial.SetTexture("_FluidsMask",current.h2);
            }
            if(i%2==0){
                Graphics.Blit(handle2,handle1,current.config.fluidMaterial);
            }else{
                Graphics.Blit(handle1,handle2,current.config.fluidMaterial);
            }
        }
        if(fluids.Length%2==0){
            Graphics.Blit(handle2,dest);
        }else{
            Graphics.Blit(handle1,dest);
        }
    }
}
