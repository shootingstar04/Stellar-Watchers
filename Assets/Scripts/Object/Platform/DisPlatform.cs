using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisPlatform : MonoBehaviour
{
    private bool isTriggered = false;
    private float DisappearTime = 1f;
    private float timer = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }
}
