using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //https://answers.unity.com/questions/760533/best-way-to-set-a-gameobject-layer-programmaticall.html
     public static int GetLayerFromMask(LayerMask mask)
    {
        uint val = (uint)mask.value;
        if (val  == 0)
            return -1;
        int layer = 0;
        if (val > 0xFFFF) // XXXX XXXX XXXX XXXX 0000 0000 0000 0000
        {
            layer += 16;
            val >>= 16;
        }
        if (val > 0xFF) // XXXX XXXX 0000 0000
        {
            layer += 8;
            val >>= 8;
        }
        if (val > 0xF) // XXXX 0000
        {
            layer += 4;
            val >>= 4;
        }
        if (val > 0x3) // XX00
        {
            layer += 2;
            val >>= 2;
        }
        if ((val & 0x2) != 0) // X0
            layer += 1;
        return layer;
    }
    // public static RenderTexture BlurTexture(RenderTexture t,int recursion){

    // }
}
