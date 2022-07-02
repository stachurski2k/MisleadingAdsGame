using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Health>(out Health h)){
            h.TakeDamage(damage);
        }
    }
}
