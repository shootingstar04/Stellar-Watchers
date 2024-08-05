using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class GeneralDoor : Door
{
    [SerializeField] bool isInteractable = true;
    [SerializeField] Collider2D interactTrigger;

    SpawnPoint spawnpoint;

    private void Awake()
    {
        if(isInteractable)
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
        isDisabled = true;
        this.gameObject.SetActive(false);
        returnBool();

    }
    public override void CloseDoor()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            bool isInput = Input.GetKey(KeyCode.UpArrow);
            Debug.Log(isInput);
            if(isInput)
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
