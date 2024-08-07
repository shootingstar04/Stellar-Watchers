using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public enum particleType
    {
        Hitted   
    }

    public GameObject hittedParticle;

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
        switch (type)
        {
            case particleType.Hitted:
                GameObject particle = Instantiate(hittedParticle, pos.position, pos.rotation);
                break;
        }    
    }

}
