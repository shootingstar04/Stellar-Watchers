using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GeneralDoor : Door
{
    [SerializeField] bool isInteractable = true;
    [SerializeField] Collider2D interactTrigger;


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

    public override void OpenDoor()
    {
        isDisabled = true;
        this.gameObject.SetActive(false);
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



}
