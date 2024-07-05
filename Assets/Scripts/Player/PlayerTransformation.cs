using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private GameObject currentPlayer;
    private Transform currentTransform;

    private float curTime = 0f;
    public float coolTime = 2f;

    void Start()
    {
        currentPlayer = Instantiate(player1Prefab, transform.position, transform.rotation);
        currentTransform = currentPlayer.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (curTime <= 0) 
            {
                Transform();
                PlayerAttackCombo.instance.ComboInvocation();
                curTime = coolTime;
            }
        }

        curTime -= Time.deltaTime;
    }

    void Transform()
    {
        Vector3 position = currentTransform.position;
        Quaternion rotation = currentTransform.rotation;

        Destroy(currentPlayer);

        if (currentPlayer.name.Contains("Player1"))
        {
            currentPlayer = Instantiate(player2Prefab, position, rotation);
        }
        else
        {
            currentPlayer = Instantiate(player1Prefab, position, rotation);
        }

        currentTransform = currentPlayer.transform;
    }
}

