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
    [SerializeField] GameObject openPos;
    [SerializeField] Vector3 closePos;

    SpawnPoint spawnpoint;

    bool isDoorClosed = true;

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

            if(openPos == null)
            {
                openPos = this.gameObject.transform.GetChild(0).gameObject;
            }

            if(closePos == null)
            {
                closePos = this.gameObject.GetComponent<Transform>().position;
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

            if(openPos != null)
            {
                Destroy(openPos);
            }
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
            ParticleManager.instance.particle_generation(ParticleManager.particleType.WallDebris, this.transform.position);
            isDisabled = true;
            this.gameObject.SetActive(false);
            returnBool();
        }
        else
        {
            if(isDisabled)
            {
                return;
            }
            ParticleManager.instance.particle_generation(ParticleManager.particleType.DoorDust, this.transform.position);
            //Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + Mathf.Abs(this.transform.position.y) + 3f, this.transform.position.z); //old code
            Vector3 pos = openPos.GetComponent<Transform>().position;
            isDisabled = true;
            this.transform.DOMove(pos, duration).OnComplete(() => Endfunction());
        }


    }

    public void Endfunction()
    {
        //this.gameObject.SetActive(false);
        //returnBool(); //old code

        //트리거 바꿔주는 코드 추가/ 혹은 oncomplete라는 특성 이용해 상호작용 가능 여부 바꾸기(returnBool에만 사용했던 isDisabled활용)
        isDisabled = false;
        isDoorClosed = !isDoorClosed;
    }

    public override void CloseDoor()
    {
        if(!isInteractable)
        {
            return;
        }

        if (isDisabled)
        {
            return;
        }
        Vector3 pos = closePos;
        isDisabled = true;
        this.transform.DOMove(pos, duration/2).OnComplete(() => Endfunction());
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
