using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="fluid",menuName ="Fluids/Create new Fluid")]
public class FluidConfig : ScriptableObject
{
    public int damage;
    public LayerMask fluidLayer;
    public LayerMask interactionFluidLayer;
    public Material fluidMaterial;
    public Particle particlePrefab;
    int _fluidLayer;
    public int FluidLayer{
        get{ return _fluidLayer;}
    }
    private void Start()
    {
        _fluidLayer=Utils.GetLayerFromMask(fluidLayer);
    }
    private void OnEnable()
    {
        _fluidLayer=Utils.GetLayerFromMask(fluidLayer);
    }
}
