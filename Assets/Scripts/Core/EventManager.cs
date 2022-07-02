using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] CoinCollector collector;
    public void OnCollsionEvent(GameObject go){
        if(go.TryGetComponent<Particle>(out Particle p)&&health!=null){
            if(p.fluid.damage!=0){
                health.TakeDamage(p.fluid.damage);
                Destroy(p.gameObject);
            }
        }else if(go.TryGetComponent<Coin>(out Coin c)&&collector!=null){
            collector.CollectCoin();
            Destroy(go);
        }
    }
}
