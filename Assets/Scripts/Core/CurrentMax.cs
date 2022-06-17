using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class CurrentMax :MonoBehaviour
{
    public  UnityEvent OnChange;
    public virtual int GetCurrent(){return 0;}
    public virtual int GetMax(){ return 0;}
}
