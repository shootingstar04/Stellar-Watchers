using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{


    private bool isHealing;
    private float skillCounter;
    
    
    private PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = this.gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        check_input();
        healing();
    }

    void check_input()
    {
        if (Input.GetKeyDown(KeyCode.S) && playerMove.IsGrounded)
        {
            playerMove.PauseMove();
            playerMove.PlayAnimation(PlayerMove.animationType.block, 1);
            Debug.Log(playerMove.name);
            skillCounter = 0;
            isHealing = true;
        }
    }

    private void healing()
    {
        if (isHealing)
        {
            skillCounter += Time.deltaTime;
            
            if (skillCounter > 1.5f)
            {
                if (PlayerSP.instance.CurSP > 0)
                {
                    PlayerSP.instance.modify_SP(-1);
                    if(PlayerHealth.instance.CurHP < PlayerHealth.instance.MaxHP) PlayerHealth.instance.modify_HP(1);
                }
                skillCounter = 0;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                isHealing = false;
                skillCounter = 0;
                playerMove.RestartMove();
                playerMove.PlayAnimation(PlayerMove.animationType.block, 0);
            }
        }

    }
}
