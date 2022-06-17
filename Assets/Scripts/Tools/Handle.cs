using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : Interactable
{
    [SerializeField] float maxMoveDistance=10f;
    Vector3 startPos;
    private void Start()
    {
        startPos=transform.position;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.cyan;
        Gizmos.DrawWireSphere(transform.position,maxMoveDistance);
    }
    public override void Move(Vector3 m)
    {
        transform.position+=m;
    }
    public override void ClampMovement()
    {
        if((startPos-transform.position).sqrMagnitude>maxMoveDistance*maxMoveDistance){
            Vector3 dir=transform.position-startPos;
            transform.position=startPos+dir.normalized*maxMoveDistance;
        }
    }
}
