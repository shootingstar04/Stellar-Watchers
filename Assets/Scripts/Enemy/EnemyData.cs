using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public string EnemyName = "";

    public void TakeDamage(int damage)
    {
        if (EnemyName == "Bandit")
        {
            this.GetComponent<Bandit>().TakeDamage(damage);
        }
    }
}
