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
    private bool inAir = false;

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
        meteorite_slash();
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
        if (skilltype != SkillSet.skill.none)
        {
            playerMove.UseSkill(true);
        }

        switch (skilltype)
        {
            case SkillSet.skill.stellarStrike:
                if (PlayerSP.instance.CurSP > 0)
                {
                    PlayerSP.instance.modify_SP(-1);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                }
                break;

            case SkillSet.skill.meteorliteSlash:
                if (PlayerSP.instance.CurSP > 2)
                {
                    PlayerSP.instance.modify_SP(-3);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.velocity = new Vector2(0, -30);
                        rigid.gravityScale = 3;
                        inAir = true;
                    }
                }
                break;
        }

        isUsingSkill = skilltype;
    }

    private void end_skill()
    {
        playerMove.UseSkill(false);
        isUsingSkill = SkillSet.skill.none;
        skillCounter = 0;
        isAttacked = false;
        inAir = false;
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
                Debug.Log(skillCounter > 0.3f && !isAttacked);

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
                end_skill();
            }
        }
    }

    private void meteorite_slash()
    {
        if (isUsingSkill == SkillSet.skill.meteorliteSlash)
        {
            skillCounter += Time.deltaTime;

            if (!inAir)
            {
                if (skillCounter > 0.3f && !isAttacked)
                {
                    Debug.Log(skillCounter > 0.7f && !isAttacked);

                    isAttacked = true;
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(meteorPos.position, meteorBox, 0);

                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<EnemyData>().TakeDamage(15 * (ProgressData.Instance.reinforcementCount + 1));
                            Debug.Log(collider.name + " 에게 " + 15 * (ProgressData.Instance.reinforcementCount + 1) + "의 데미지를 입히고 2초간 스턴 효과 부여");
                        }
                    }
                }

                if (skillCounter > 1.7f)
                {
                    end_skill();
                }
            }

            else
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(meteorPos.position, meteorBox, 0);

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        if (collider.transform.position.y-1.8f > meteorPos.position.y)
                        {
                            collider.transform.position = new Vector2(collider.transform.position.x, meteorPos.position.y+1.8f);
                        }
                    }
                }


                Collider2D groundCheck = Physics2D.OverlapBox(meteorPos.position, meteorBox, 0, playerMove.groundLayer);

                if (playerMove.IsGrounded || groundCheck != null)
                {
                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<EnemyData>().TakeDamage(15 * (ProgressData.Instance.reinforcementCount + 1) + (int)(skillCounter / 0.3f) * 1);
                            Debug.Log(collider.name + " 에게 " + (15 * (ProgressData.Instance.reinforcementCount + 1) + (int)(skillCounter / 0.3f) * 1) + "의 데미지를 입히고 2초간 스턴 효과 부여");
                        }
                    }
                    end_skill();
                }
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
