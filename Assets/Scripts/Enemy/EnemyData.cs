using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public string EnemyName = "";

    public void TakeDamage(float damage)
    {
        ParticleManager.instance.particle_generation(ParticleManager.particleType.Hitted, this.transform);

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
            this.GetComponent<Hider>().TakeDamage(damage);
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
        if (EnemyName == "Flame Wizard")
        {
            this.GetComponent<FireWizard>().TakeDamage(damage);
        }
        if (EnemyName == "Heavy Armor1")
        {
            this.GetComponent<HeavyArmor1>().TakeDamage(damage);
        }
        if (EnemyName == "Heavy Armor2")
        {
            this.GetComponent<HeavyArmor2>().TakeDamage(damage);
        }
    }
}
