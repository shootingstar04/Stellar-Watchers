using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCombo : MonoBehaviour
{
    public enum PlayerType { Player1, Player2 }
    public PlayerType playerType;

    private bool xKeyPressed = false;
    private float xKeyPressTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            xKeyPressed = true;
            xKeyPressTime = Time.time;
        }

        if (xKeyPressed && Input.GetKeyDown(KeyCode.LeftShift) && Time.time - xKeyPressTime <= 2f)
        {
            Debug.Log("Combo");
            if (playerType == PlayerType.Player1)
            {
                PerformRangedAttack();
            }
            else if (playerType == PlayerType.Player2)
            {
                PerformMeleeAttack();
            }

            xKeyPressed = false;
        }

        if (xKeyPressed && Time.time - xKeyPressTime > 2f)
        {
            xKeyPressed = false;
        }
    }

    void PerformRangedAttack()
    {
        Debug.Log("Player1 performs a ranged attack!");
    }

    void PerformMeleeAttack()
    {
        Debug.Log("Player2 performs a melee attack!");
    }
}
