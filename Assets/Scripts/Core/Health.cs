using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : CurrentMax
{
    public int maxHealth;
    public UnityEvent OnDie;
    int health;
    public int CurrentHealth{
        get{return health;}
    }
    private void Start()
    {
        health=maxHealth;
    }
    public void TakeDamage(int damage){
        if(health<=0){
            return;
        }
        health-=damage;
        if(health<=0){
            OnDie?.Invoke();
        }else{
            OnChange?.Invoke();
        }
    }

    public override int GetCurrent()
    {
        return health;
    }

    public override int GetMax()
    {
        return maxHealth;
    }
}
