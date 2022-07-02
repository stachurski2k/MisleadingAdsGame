using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRightLeft : PlatformUpDown
{
    [SerializeField] Transform otherDestination2;
    Transform currentDist;
    private void Start()
    {
        currentDist=otherDestination2;
    }
      public override void Move1(){
        Vector3 dir=(destination.position-platform.position);
        if(dir.sqrMagnitude<=snapDist*snapDist){
            finished=true;
            platform.position=destination.position;
        }else{
            platform.position+=dir.normalized*moveSpeed*Time.deltaTime;
        }
    }
    public override void Move2(){
        Vector3 dir=(currentDist.position-otherPlatform.position);
        if(dir.sqrMagnitude<=snapDist*snapDist){
            if(currentDist==otherDestination){
                currentDist=otherDestination2;
            }
            else{
                currentDist=otherDestination;
            }
        }else{
            otherPlatform.position+=dir.normalized*moveSpeed*Time.deltaTime;
        }
    }
}
