using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public enum particleType
    {
        Hitted,
        EnemyHit,
        WallDebris
    }

    public GameObject hittedParticle;
    public GameObject enemyHittedParticle;
    public GameObject wallDebris;

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

    public void particle_generation(particleType type, Transform pos)
    {
        GameObject particle;
        switch (type)
        {
            case particleType.Hitted:
                particle = Instantiate(hittedParticle, pos.position, pos.rotation);
                break;
            case particleType.EnemyHit:
                particle = Instantiate(enemyHittedParticle, pos.position, Quaternion.AngleAxis(Random.Range(-40f, 220f), Vector3.forward));
                break;
            case particleType.WallDebris:
                particle = Instantiate(wallDebris, pos.position, pos.rotation.normalized);
                break;
        }
    }

}
