using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    public string PlayerType = "Melee";

    private Transform currentTransform;

    void Start()
    {
        currentTransform = gameObject.transform;
        VirtualCameraManager.Instance.ChangeCameraTarget(currentTransform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Transform();
            PlayerAttackCombo.instance.ComboInvocation();
        }
    }

    void Transform()
    {
        Vector3 position = currentTransform.position;
        Quaternion rotation = currentTransform.rotation;

        if (PlayerType == "Melee")
        {
            PlayerType = "Ranged";
            gameObject.GetComponent<PlayerAttackMelee>().enabled = false;
            gameObject.GetComponent<PlayerAttackRanged>().enabled = true;
        }
        else
        {
            PlayerType = "Melee";
            gameObject.GetComponent<PlayerAttackMelee>().enabled = true;
            gameObject.GetComponent<PlayerAttackRanged>().enabled = false;
            
        }

        currentTransform = gameObject.transform;
    }
}