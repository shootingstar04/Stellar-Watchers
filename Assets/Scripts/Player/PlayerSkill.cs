using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    //[Header("힐")]
    //public float healDelay1 = 0.3f;
    //public float healDelay2 = 0.7f;

    //[Space(1)]
    [Header("스텔라 스트라이크")]
    public Transform stellaPos;
    public Vector2 stellaBox;
    public float stellaDelay1 = 0.3f;
    public float stellaDelay2 = 0.7f;

    [Space(1)]
    [Header("일점 찌르기")]
    public Transform pointPos;
    public Vector2 pointBox;
    public float pointDelay1 = 1f;
    public float pointDelay2 = 2f;

    [Space(1)]
    [Header("메테오라이트 슬래시")]
    public Transform meteorPos;
    public Vector2 meteorBox;
    public float meteorDelay1 = 0.7f;
    public float meteorDelay2 = 1f;

    [Space(1)]
    [Header("프레이야")]
    public Transform freyjaPos;
    public GameObject freyjaBullet;
    public float freyjaDelay1 = 1.5f;

    [Space(1)]
    [Header("소어링 스타")]
    public Transform soaringPos;
    public GameObject soaringBullet;
    public float soaringDelay1 = 2f;




    private bool isHealing;
    private SkillSet.skill isUsingSkill = SkillSet.skill.none;

    private float skillCounter;
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
        point_sting();
        meteorite_slash();

        freyja();
        soaring_star();
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
                if (PlayerSP.instance.CurSP > 0)
                {
                    PlayerSP.instance.modify_SP(-1);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
                }
                break;
            case SkillSet.skill.pointSting:
                if (PlayerSP.instance.CurSP > 0)
                {
                    PlayerSP.instance.modify_SP(-1);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
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
                    isUsingSkill = skilltype;
                }
                break;
            case SkillSet.skill.bigShot:
                if (PlayerSP.instance.CurSP > 0)
                {
                    PlayerSP.instance.modify_SP(-1);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
                }
                break;
            case SkillSet.skill.laserAttack:
                if (PlayerSP.instance.CurSP > 2)
                {
                    PlayerSP.instance.modify_SP(-3);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
                }
                break;
        }

        if (isUsingSkill != SkillSet.skill.none)
        {
            playerMove.UseSkill(true);
        }

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

            if (skillCounter > stellaDelay1 && !isAttacked)
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
                    else if (collider.tag == "BOMB")
                    {
                        Debug.Log("폭탄 맞음");
                        Bomb.onFire();
                    }
                    else if (collider.GetComponent<ElevatorSwitch>() != null)
                    {
                        Debug.Log("스위치 작동");
                        collider.GetComponent<ElevatorSwitch>().SwitchFlick();
                    }
                    else if (collider.GetComponent<Chest>() != null)
                    {
                        collider.gameObject.GetComponent<Chest>().Distroyed();
                    }
                    else if (collider.GetComponent<GeneralDoor>() != null)
                    {
                        collider.GetComponent<GeneralDoor>().OpenDoor();
                    }
                }
            }

            if (skillCounter > stellaDelay1+stellaDelay2)
            {
                end_skill();
            }
        }
    }
    private void point_sting()
    {
        if (isUsingSkill == SkillSet.skill.pointSting)
        {
            skillCounter += Time.deltaTime;

            if (skillCounter > pointDelay1 && !isAttacked)
            {
                isAttacked = true;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(stellaPos.position, stellaBox, 0);

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<EnemyData>().TakeDamage(9 * (ProgressData.Instance.reinforcementCount + 1));
                        Debug.Log(collider.name + " 에게 " + 9 * (ProgressData.Instance.reinforcementCount + 1) + "의 데미지를 입힘");
                    }
                    else if (collider.tag == "BOMB")
                    {
                        Debug.Log("폭탄 맞음");
                        Bomb.onFire();
                    }
                    else if (collider.GetComponent<ElevatorSwitch>() != null)
                    {
                        Debug.Log("스위치 작동");
                        collider.GetComponent<ElevatorSwitch>().SwitchFlick();
                    }
                    else if (collider.GetComponent<Chest>() != null)
                    {
                        collider.gameObject.GetComponent<Chest>().Distroyed();
                    }
                    else if (collider.GetComponent<GeneralDoor>() != null)
                    {
                        collider.GetComponent<GeneralDoor>().OpenDoor();
                    }
                }
            }

            if (skillCounter > pointDelay1+pointDelay2)
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
                if (skillCounter > meteorDelay1 && !isAttacked)
                {

                    isAttacked = true;
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(meteorPos.position, meteorBox, 0);

                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<EnemyData>().TakeDamage(15 * (ProgressData.Instance.reinforcementCount + 1));
                            Debug.Log(collider.name + " 에게 " + 15 * (ProgressData.Instance.reinforcementCount + 1) + "의 데미지를 입히고 2초간 스턴 효과 부여");
                        }
                        else if (collider.tag == "BOMB")
                        {
                            Debug.Log("폭탄 맞음");
                            Bomb.onFire();
                        }
                        else if (collider.GetComponent<ElevatorSwitch>() != null)
                        {
                            Debug.Log("스위치 작동");
                            collider.GetComponent<ElevatorSwitch>().SwitchFlick();
                        }
                        else if (collider.GetComponent<Chest>() != null)
                        {
                            collider.gameObject.GetComponent<Chest>().Distroyed();
                        }
                        else if (collider.GetComponent<GeneralDoor>() != null)
                        {
                            collider.GetComponent<GeneralDoor>().OpenDoor();
                        }
                    }
                }

                if (skillCounter > meteorDelay1+meteorDelay2)
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
                        if (collider.transform.position.y - 1.8f > meteorPos.position.y)
                        {
                            collider.transform.position = new Vector2(collider.transform.position.x, meteorPos.position.y + 1.8f);
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
                    Invoke("end_skill", meteorDelay2);
                }
            }
        }
    }


    private void freyja()
    {
        if (isUsingSkill == SkillSet.skill.bigShot)
        {
            skillCounter += Time.deltaTime;

            if (!isAttacked)
            {
                Debug.Log(skillCounter);
                isAttacked = true;

                GameObject instance = Instantiate(freyjaBullet, freyjaPos.position, freyjaPos.rotation);

                instance.GetComponent<Freyja>().dir = (int)playerMove.LastDirection;
            }

            if (skillCounter > freyjaDelay1)
            {
                end_skill();
            }
        }
    }
    private void soaring_star()
    {
        if (isUsingSkill == SkillSet.skill.laserAttack)
        {
            skillCounter += Time.deltaTime;

            if (!isAttacked)
            {
                isAttacked = true;


                List<GameObject> enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag(Define.EnemyTag));

                float shortestDis = 100000;
                GameObject target = null; ;

                if (enemys[0] != null)
                {
                    shortestDis = ((Vector2)(this.transform.position - enemys[0].transform.position)).sqrMagnitude;

                    target = enemys[0];
                }

                foreach (GameObject enemy in enemys)
                {

                    float dis = ((Vector2)(this.transform.position - enemy.transform.position)).sqrMagnitude;

                    if (dis < shortestDis)
                    {
                        target = enemy;
                        shortestDis = dis;
                    }
                }

                GameObject instance = Instantiate(soaringBullet, soaringPos.position, soaringPos.rotation);

                if (target != null)
                {
                    float targetX = Camera.main.WorldToViewportPoint(target.transform.position).x;
                    float targetY = Camera.main.WorldToViewportPoint(target.transform.position).y;

                    if (targetX >= 0 && targetX <= 1 && targetY >= 0 && targetY <= 1)
                    {
                        instance.GetComponent<SoaringStar>().set_target(target);
                    }
                    else
                    {
                        instance.GetComponent<SoaringStar>().set_target(null);
                    }
                }
                else
                {
                    instance.GetComponent<SoaringStar>().set_target(null);
                }
            }

            if (skillCounter > soaringDelay1)
            {
                end_skill();
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(stellaPos.position, stellaBox);
        Gizmos.DrawWireCube(meteorPos.position, meteorBox);
        Gizmos.DrawWireCube(pointPos.position, pointBox);
    }
}
