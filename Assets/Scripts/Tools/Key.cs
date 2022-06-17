using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    [SerializeField] float maxForwadDistance,maxBackwardsDistance;
    Vector3 maxForwardPoint,maxBackwardsPoint,startPos;
    public Vector3 Forward{
        get{
            return transform.right;
        }
    }
    private void Start()
    {
        startPos=transform.position;
        maxForwardPoint=transform.position+Forward*maxForwadDistance;
        maxBackwardsPoint=transform.position-Forward*maxBackwardsDistance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.cyan;
        Gizmos.DrawWireSphere(transform.position+Forward*maxForwadDistance,0.5f);
        Gizmos.DrawWireSphere(transform.position-Forward*maxBackwardsDistance,0.5f);
    }
    public override void Move(Vector3 moveDelta){
        Vector3 axisMovement=Vector3.Project(moveDelta,Forward);
        transform.position+=axisMovement;
    }
    public override void ClampMovement(){
        Vector3 dir=(transform.position-startPos).normalized;
        if(Mathf.Approximately(Vector3.Dot(dir,Forward),1)){
            if(Vector3.Distance(transform.position,startPos)>maxForwadDistance){
                transform.position=maxForwardPoint;
            }
        }else{
            if(Vector3.Distance(transform.position,startPos)>maxBackwardsDistance){
                transform.position=maxBackwardsPoint;
            }
        }
    }
}
