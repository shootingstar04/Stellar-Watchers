using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public enum particleType
    {
        Hitted,
        EnemyHit,
        WallDebris,
        DoorDust,
        CoinDust,
        HealEffect
    }

    public GameObject hittedParticle;
    public GameObject enemyHittedParticle;
    public GameObject wallDebris;
    public GameObject doorDust;
    public GameObject coinDust;
    public GameObject healEffect;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject particle_generation(particleType type, Vector3 pos)
    {
        GameObject particle = null;
        switch (type)
        {
            case particleType.Hitted:
                particle = Instantiate(hittedParticle, pos, Quaternion.identity);
                break;
            case particleType.EnemyHit:
                particle = Instantiate(enemyHittedParticle, pos, Quaternion.AngleAxis(Random.Range(-40f, 220f), Vector3.forward));
                break;
            case particleType.WallDebris:
                particle = Instantiate(wallDebris, pos, Quaternion.identity);
                break;
            case particleType.DoorDust:
                particle = Instantiate(doorDust, pos, Quaternion.identity);
                break;
            case particleType.CoinDust:
                particle = Instantiate(coinDust, pos, Quaternion.identity);
                break;
            case particleType.HealEffect:
                particle = Instantiate(healEffect, pos, Quaternion.identity);
                break;
        }
        return particle;
    }

}
