using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Postprocessor : MonoBehaviour
{
    [SerializeField] IProcessor[] processors;
    RenderTexture handle1,handle2;
    private void Start()
    {
        handle1=new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.ARGB32);
        handle2=new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.ARGB32);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src,handle2);
        for (int i = 0; i < processors.Length; i++)
        {
            if(i%2==0){
                processors[i].Proccess(handle2,handle1);
            }
            else{
                processors[i].Proccess(handle1,handle2);
            }
        }
        if(processors.Length%2==0){
            Graphics.Blit(handle2,dest);
        }else{
            Graphics.Blit(handle1,dest);
        }
    }
}
