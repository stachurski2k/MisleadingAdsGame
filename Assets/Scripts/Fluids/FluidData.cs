using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FluidData 
{
   public FluidConfig config;
   [HideInInspector]
   public RenderTexture h1,h2;
   public void Initialize(float aspect){
        h1=new  RenderTexture((int)(256.0f*aspect),256,0,RenderTextureFormat.R16);
        h2=new  RenderTexture((int)(256.0f*aspect),256,0,RenderTextureFormat.R16);
   }
}
