using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class GeneralDoor : Door
{
    [SerializeField] bool isInteractable = true;
    [SerializeField] Collider2D interactTrigger;
    [SerializeField] float duration;

    SpawnPoint spawnpoint;

    private void Awake()
    {
        if (isInteractable)
        {
            Collider2D[] colliders = this.gameObject.GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.isTrigger)
                {
                    interactTrigger = collider;
                }
            }
        }
        else
        {
            Collider2D[] colliders = this.gameObject.GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.isTrigger)
                {
                    interactTrigger = collider;
                }
            }
            Destroy(interactTrigger);
        }
    }

    private void Start()
    {
        spawnpoint = GetComponentInParent<SpawnPoint>();
    }

    public override void OpenDoor()
    {
        if (!isInteractable)
        {
            ParticleManager.instance.particle_generation(ParticleManager.particleType.WallDebris, this.transform);
            isDisabled = true;
            this.gameObject.SetActive(false);
            returnBool();
        }
        else
        {
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + Mathf.Abs(this.transform.position.y) + 3f, this.transform.position.z);
            isDisabled = true;
            this.transform.DOMove(pos, duration).OnComplete(() => Endfunction());
        }


    }

    public void Endfunction()
    {
        this.gameObject.SetActive(false);
        returnBool();
    }

    public override void CloseDoor()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            bool isInput = Input.GetKey(KeyCode.UpArrow);
            //Debug.Log(isInput);
            if (isInput)
            {
                OpenDoor();
            }
        }
    }

    public void returnBool()
    {
        spawnpoint.canSpawn = false;
    }

}
