using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public string EnemyName = "";

    public void TakeDamage(float damage)
    {
        if (EnemyName == "Bandit")
        {
            this.GetComponent<Bandit>().TakeDamage(damage);
        }
        if (EnemyName == "Bringer")
        {
            this.GetComponent<Bringer>().TakeDamage(damage);
        }
        if (EnemyName == "Evil Wizard")
        {
            
        }
        if (EnemyName == "Goblin")
        {
            this.GetComponent<Goblin>().TakeDamage(damage);
        }
        if (EnemyName == "Hider")
        {
            
        }
        if (EnemyName == "Mushroom")
        {
            this.GetComponent<Mushroom>().TakeDamage(damage);
        }
        if (EnemyName == "Skeleton")
        {
            this.GetComponent<Skeleton>().TakeDamage(damage);
        }
        if (EnemyName == "Warrior")
        {
            this.GetComponent<Warrior>().TakeDamage(damage);
        }
    }
}
