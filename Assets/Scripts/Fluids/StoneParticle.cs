using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneParticle : Particle
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if((fluid.interactionFluidLayer.value & (1 << other.gameObject.layer)) != 0&&
         transform.position.y>other.transform.position.y){
            Vector3 h=transform.position;
            transform.position=other.transform.position;
            other.transform.position=h;
        }
    }
}
