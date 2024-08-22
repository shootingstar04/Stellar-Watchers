using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerdoors : MonoBehaviour
{
    private float duration = 0.5f;
    private float size = 5.2f;
    public void CloseDoor()
    {
        ParticleManager.instance.particle_generation(ParticleManager.particleType.DoorDust, this.transform.position);
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y - Mathf.Abs(size + 0.7f), this.transform.position.z);

        this.transform.DOMove(pos, duration).OnComplete(() => Endfunction());
    }

    public void OpenDoor()
    {
        ParticleManager.instance.particle_generation(ParticleManager.particleType.DoorDust, this.transform.position);
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + Mathf.Abs(size + 0.7f), this.transform.position.z);

        this.transform.DOMove(pos, duration);
    }


    public void Endfunction()
    {
        //spawnboss
    }
}
