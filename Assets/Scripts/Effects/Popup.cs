using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
   [SerializeField] GameObject target;
   [SerializeField] float timeToDeactivate=3f;
   private void Start()
   {
       target.SetActive(true);
   }
   void ClearPopup(){
        target.SetActive(false);
        Destroy(this);
   }
    void Update()
    {
        timeToDeactivate-=Time.deltaTime;
        if(timeToDeactivate<0){
            ClearPopup();
        }
        if(Input.GetMouseButtonDown(0)){
            ClearPopup();
        }
    }
}
