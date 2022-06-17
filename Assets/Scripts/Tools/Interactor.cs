using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;
    Interactable interactable=null;
    Vector3 moveOffset=new Vector3();
    Vector3 lastMousePos;
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit=Physics2D.Raycast(mousePos,Vector2.zero,1000f,interactableMask);
            if(hit.collider==null){
                return;
            }
            if(hit.collider.TryGetComponent<Interactable>(out Interactable k)){
                interactable=k;
                moveOffset=interactable.transform.position-mousePos;
                lastMousePos=mousePos;
            }
        }else if(Input.GetMouseButton(0)&&interactable!=null){
            Vector3 mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 moveDelta=mousePos-lastMousePos;

            interactable.Move(moveDelta);
            interactable.ClampMovement();
            lastMousePos=mousePos;
        }else if(Input.GetMouseButtonUp(0)){
            interactable=null;
        }
    }
}
