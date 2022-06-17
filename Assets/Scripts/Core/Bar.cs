using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField]  CurrentMax cm;
    [SerializeField][Range(0,1)] float baseFill=1;
    private void Start()
    {
        cm.OnChange.AddListener(OnChange);
        bar.fillAmount=baseFill;
    }
    public void OnChange(){
        bar.fillAmount=(float)cm.GetCurrent()/(int)cm.GetMax();
    }
}
