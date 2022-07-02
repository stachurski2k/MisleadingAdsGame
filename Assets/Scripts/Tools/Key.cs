using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    [SerializeField] bool parentRelative=false;
    [SerializeField] float maxForwadDistance,maxBackwardsDistance;
    Vector3 maxForwardPoint,maxBackwardsPoint,startPos;
    public Vector3 Forward{
        get{
            return transform.right;
        }
    }
    private void Start()
    {
        if(parentRelative){
            startPos=transform.localPosition;
            maxForwardPoint=transform.localPosition+Forward*maxForwadDistance;
            maxBackwardsPoint=transform.localPosition-Forward*maxBackwardsDistance;
        }else{
            startPos=transform.position;
            maxForwardPoint=transform.position+Forward*maxForwadDistance;
            maxBackwardsPoint=transform.position-Forward*maxBackwardsDistance;
        }
      
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.cyan;
        Gizmos.DrawWireSphere(transform.position+Forward*maxForwadDistance,0.5f);
        Gizmos.DrawWireSphere(transform.position-Forward*maxBackwardsDistance,0.5f);
    }
    public override void Move(Vector3 moveDelta){
        Vector3 axisMovement=Vector3.Project(moveDelta,Forward);
        if(parentRelative){
            transform.localPosition+=axisMovement;
        }else{
            transform.position+=axisMovement;
        }
    }
    public override void ClampMovement(){
        Vector3 t=(parentRelative)?transform.localPosition:transform.position;
        Vector3 dir=(t-startPos).normalized;
        if(Mathf.Approximately(Vector3.Dot(dir,Forward),1)){
            if(Vector3.Distance(t,startPos)>maxForwadDistance){
                if(parentRelative){
                    transform.localPosition=maxForwardPoint;
                }else{
                    transform.position=maxForwardPoint;
                }
            }
        }else{
            if(Vector3.Distance(t,startPos)>maxBackwardsDistance){
                if(parentRelative){
                    transform.localPosition=maxBackwardsPoint;
                }else{
                    transform.position=maxBackwardsPoint;
                }
            }
        }
    }
}
