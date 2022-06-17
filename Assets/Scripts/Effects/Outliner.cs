using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outliner : IProcessor
{
    [SerializeField] Color outlineColor;
    [SerializeField] LayerMask outlineable;
    [SerializeField] Shader BlomShader;
    [SerializeField] Shader BlurShader;
    [SerializeField] Shader OutlineShader;
    [SerializeField][Range(1,10)] int recursion=2;
    GameObject toOutline;
    Camera post;
    Material outlineMaterial,blurMaterial;
    RenderTexture bloom,blur,handle;
    LayerMask layerHandle;
    float[] weights={0.227027f, 0.1945946f, 0.1216216f, 0.054054f, 0.016216f,0.00574053f};
    private void Start()
    {
        post=new GameObject("Post ProcessingCamera").AddComponent<Camera>();
        post.enabled=false;
        outlineMaterial=new Material(OutlineShader);
        post.CopyFrom(Camera.main);
        post.backgroundColor=Color.black;
        post.clearFlags=CameraClearFlags.Color;
        post.cullingMask=1<<LayerMask.NameToLayer("Outline");


        blurMaterial=new Material(BlurShader);
        blurMaterial.SetFloatArray("_Weights",weights);
        bloom=new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.R8);
        blur=new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.R8);
        handle=new RenderTexture(Screen.width,Screen.height,0,RenderTextureFormat.R8);
        blur.filterMode=bloom.filterMode=FilterMode.Point;

        post.targetTexture=bloom;
    }
    private void Update()
    {
        Vector3 mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit=Physics2D.Raycast(mousePos,Vector2.zero,1000f,outlineable);
        if(hit.collider!=null){
            toOutline=hit.collider.gameObject;
        }
        else{
            toOutline=null;
        }
    }
    public override void Proccess(RenderTexture src, RenderTexture dest)
    {
        if(toOutline==null){
            Graphics.Blit(src,dest);
            return;
        }
        layerHandle=toOutline.layer;
        toOutline.layer=LayerMask.NameToLayer("Outline");
        for (int i = 0; i < toOutline.transform.childCount; i++)
        {
            toOutline.transform.GetChild(i).gameObject.layer=LayerMask.NameToLayer("Outline");
        }

        post.RenderWithShader(BlomShader,"");

        Graphics.Blit(bloom,blur,blurMaterial);
        for(int i=1;i<recursion;i++){
            if(i%2==0){
                Graphics.Blit(handle,blur,blurMaterial);
            }else{
                Graphics.Blit(blur,handle,blurMaterial);
            }
        }

        outlineMaterial.SetColor("_OutlineColor",outlineColor);
        outlineMaterial.SetTexture("_SceneTex",src);
        if(recursion%2==0){
            outlineMaterial.SetTexture("_BlurTex",blur);
        }else{
            outlineMaterial.SetTexture("_BlurTex",handle);
        }
        Graphics.Blit(bloom,dest,outlineMaterial);
        
        toOutline.layer=layerHandle;
        for (int i = 0; i < toOutline.transform.childCount; i++)
        {
            toOutline.transform.GetChild(i).gameObject.layer=layerHandle;
        }
    }
}
