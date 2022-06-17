using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidManager : MonoBehaviour
{
    [SerializeField] Transform waterParent;
    [SerializeField]public FluidConfig waterConfig;
    [SerializeField] Transform stoneParent;
    [SerializeField]public FluidConfig stoneConfig;
    [SerializeField] public Transform lavaParent;
    [SerializeField] int numOfParticlesUp=5;
    [SerializeField] float timeBetwenLavaUp;///idk the name
    [SerializeField] float lavaUpForce=10f;
    public static FluidManager instance;
    StoneParticle[] stoneBuffer;
    int stoneBufferIndex=0;
    float lavaTimer=0;
    private void Awake()
    {
        if(instance==null){
            instance=this;
        }
        else{
            Destroy(this);
        }
    }
    void AddparticleComponentTO(Transform t,FluidConfig c){
        if(t==null||c==null){
            return;
        }
        for (int i = 0; i < t.childCount; i++)
        {
            var p2=t.GetChild(i).gameObject.AddComponent<Particle>();
            p2.fluid=c;
        }
    }
    void CreateStoneBuffer(){
        if(waterParent==null){
            return;
        }
        stoneBuffer=new StoneParticle[lavaParent.childCount+waterParent.childCount];
        for(int i=0;i<lavaParent.childCount+waterParent.childCount;i++){
            var s=Instantiate<Particle>(stoneConfig.particlePrefab) as StoneParticle;
            s.transform.SetParent(stoneParent);
            s.gameObject.layer=stoneConfig.FluidLayer;
            stoneBuffer[i]=s;
            s.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        lavaTimer=timeBetwenLavaUp;
        CreateStoneBuffer();
    }
    private void Update()
    {
        lavaTimer-=Time.deltaTime;
        if(lavaTimer<0){
            lavaTimer=timeBetwenLavaUp;
            for(int i=0;i<numOfParticlesUp&&i<lavaParent.childCount;i++){
                int r=Random.Range(0,lavaParent.childCount-1);
                lavaParent.GetChild(r).GetComponent<Rigidbody2D>().AddForce(Vector3.up*lavaUpForce);
            }
        }
       
    }
    public void ConvertToStone(GameObject g){
        if(stoneBufferIndex==stoneBuffer.Length||!g.activeSelf){
            return;
        }
        g.SetActive(false);
        var s=stoneBuffer[stoneBufferIndex];
        s.transform.position=g.transform.position;
        s.gameObject.SetActive(true);
        stoneBufferIndex++;
    }
}
