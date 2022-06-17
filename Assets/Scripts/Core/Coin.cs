using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] LayerMask destructionMask;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if((destructionMask.value & (1 << other.gameObject.layer)) != 0){
            Destroy(gameObject);
        }
    }
}
