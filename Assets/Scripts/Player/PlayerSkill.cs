using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("스텔라 스트라이크")]
    public Transform stellaPos;
    public Vector2 stellaBox;
    [Space(1)]
    [Header("메테오라이트 슬래시")]
    public Transform meteorPos;
    public Vector2 meteorBox;





    private bool isHealing;
    private SkillSet.skill isUsingSkill = SkillSet.skill.none;

    private float skillCounter;
    private float skillCool;
    private bool isAttacked = false;

    private PlayerMove playerMove;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = this.gameObject.GetComponent<PlayerMove>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        check_input();
        healing();
        stella_strike();
    }

    void check_input()
    {
        if (isUsingSkill == SkillSet.skill.none && !isHealing)
        {
            if (Input.GetKeyDown(KeyCode.S) && playerMove.IsGrounded)
            {
                playerMove.PauseMove();
                playerMove.PlayAnimation(PlayerMove.animationType.block, 1);
                Debug.Log(playerMove.name);
                isHealing = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                int index = (this.GetComponent<PlayerTransformation>().PlayerType == "Melee" ? 0 : 1);
                start_skill(SkillData.Instance.settedSkill[index, 0]);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                int index = (this.GetComponent<PlayerTransformation>().PlayerType == "Melee" ? 0 : 1);
                start_skill(SkillData.Instance.settedSkill[index, 1]);
            }
        }
    }
    private void start_skill(SkillSet.skill skilltype)
    {
        switch (skilltype)
        {
            case SkillSet.skill.stellarStrike:

                playerMove.UseSkill(true);

                if (!playerMove.IsGrounded)
                {
                    rigid.gravityScale = 0;
                }

                isUsingSkill = skilltype;
                break;
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
                    if (PlayerHealth.instance.CurHP < PlayerHealth.instance.MaxHP) PlayerHealth.instance.modify_HP(1);
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

    private void stella_strike()
    {
        if (isUsingSkill == SkillSet.skill.stellarStrike)
        {
            skillCounter += Time.deltaTime;

            if (skillCounter > 0.3f && !isAttacked)
            {
                isAttacked = true;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(stellaPos.position, stellaBox, 0);

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<EnemyData>().TakeDamage(10 * (ProgressData.Instance.reinforcementCount + 1));
                        Debug.Log(collider.name + " 에게 " + 10 * (ProgressData.Instance.reinforcementCount + 1) + "의 데미지를 입힘");
                    }
                }
            }

            if (skillCounter > 1)
            {
                playerMove.UseSkill(false);
                isUsingSkill = SkillSet.skill.none;
                skillCounter = 0;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(stellaPos.position, stellaBox);
        Gizmos.DrawWireCube(meteorPos.position, meteorBox);
    }
}
