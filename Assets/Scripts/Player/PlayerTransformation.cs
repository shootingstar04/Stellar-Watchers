using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    private Transform currentTransform;

    void Start()
    {
        currentTransform = gameObject.transform;
        asd.Instance.ChangeCameraTarget(currentTransform);
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

        // if (currentPlayer.name.Contains("Player1"))
        // {
        //     currentPlayer = Instantiate(player2Prefab, position, rotation);
        // }
        // else
        // {
        //     currentPlayer = Instantiate(player1Prefab, position, rotation);
        // }

        currentTransform = gameObject.transform;
    }
}