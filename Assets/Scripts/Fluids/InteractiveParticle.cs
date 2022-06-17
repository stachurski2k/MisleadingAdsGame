using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveParticle : Particle
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if((fluid.interactionFluidLayer.value & (1 << other.gameObject.layer)) != 0&&other.gameObject.activeSelf&&gameObject.activeSelf){
            FluidManager.instance.ConvertToStone(other.gameObject);
            FluidManager.instance.ConvertToStone(gameObject);
        }
    }
}
