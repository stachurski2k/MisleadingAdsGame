using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    [SerializeField] protected int numParticlesToMove=50;
    [SerializeField]protected Transform platform;
    [SerializeField] protected Transform destination;
    [SerializeField]protected  Transform otherPlatform;
    [SerializeField]protected Transform otherDestination;
    [SerializeField]protected float moveSpeed;
    [SerializeField]protected float snapDist;

    protected bool finished,otherfinished;
   protected bool isMoving=false;
    public void OnTriggerParticle(GameObject go){
        if(!isMoving&&go.TryGetComponent<Particle>(out Particle p)){
            numParticlesToMove-=1;
            if(numParticlesToMove<=0){
                isMoving=true;
            }
        }
    }
    private void Update()
    {
        if(!isMoving){
            return;
        }
        Move();
    }
    public virtual void Move1(){
        Vector3 dir=(destination.position-platform.position);
        if(dir.sqrMagnitude<=snapDist*snapDist){
            finished=true;
            platform.position=destination.position;
        }else{
            platform.position+=dir.normalized*moveSpeed*Time.deltaTime;
        }
    }
    public virtual void Move2(){
        Vector3 dir=(otherDestination.position-otherPlatform.position);
        if(dir.sqrMagnitude<=snapDist*snapDist){
            otherfinished=true;
            otherPlatform.position=otherDestination.position;
        }else{
            otherPlatform.position+=dir.normalized*moveSpeed*Time.deltaTime;
        }
    }
    void Move(){
        if(!finished){
            Move1();
        }
        if(!otherfinished){
            Move2();
        }
    }
}
