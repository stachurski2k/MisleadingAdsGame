using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollector : CurrentMax
{
    public int coinsToCollect=5;
    public int coinsCollected=0;
    public UnityEvent OnCoinsCollected;
    public void CollectCoin(){
        coinsCollected+=1;
        OnChange?.Invoke();
        if(coinsCollected>=coinsToCollect){
            OnCoinsCollected?.Invoke();
        }
    }
    public override int GetCurrent()
    {
        return coinsCollected;
    }
    public override int GetMax()
    {
        return coinsToCollect;
    }
}
