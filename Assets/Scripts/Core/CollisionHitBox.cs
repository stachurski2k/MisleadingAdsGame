using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class HitBoxEvent:UnityEvent<GameObject>{}
public class CollisionHitBox : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    public HitBoxEvent OnHit;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if((layer.value & (1 << other.gameObject.layer)) != 0){
            OnHit?.Invoke(other.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if((layer.value & (1 << other.gameObject.layer)) != 0){
            OnHit?.Invoke(other.gameObject);
        }
    }
}
