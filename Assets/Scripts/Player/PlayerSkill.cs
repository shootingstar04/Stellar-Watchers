using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    //[Header("힐")]
    //public float healDelay1 = 0.3f;
    //public float healDelay2 = 0.7f;

    //[Space(1)]
    [Header("강 휘두르기")]
    public Transform stellaPos;
    public Vector2 stellaBox;
    public Animator stellaEffect;
    public SpriteRenderer stellaSprite;
    public float stellaDelay1 = 0.3f;
    public float stellaDelay2 = 0.7f;

    [Space(1)]
    [Header("일점 찌르기")]
    public Transform pointPos;
    public Vector2 pointBox;
    public SpriteRenderer pointSprite;
    public float pointDelay1 = 1f;
    public float pointDelay2 = 2f;

    [Space(1)]
    [Header("강타")]
    public Transform meteorPos;
    public Vector2 meteorBox;
    public Animator meteorEffect1;
    public SpriteRenderer meteorSprite1;
    public Animator meteorEffect2;
    public SpriteRenderer meteorSprite2;
    public float meteorDelay1 = 0.7f;
    public float meteorDelay2 = 1f;
    private float endTime = 0;

    [Space(1)]
    [Header("유성탄")]
    public Transform freyjaPos;
    public GameObject freyjaBullet;
    public float freyjaDelay1 = 1.5f;

    [Space(1)]
    [Header("빛의 기둥")]
    public Transform soaringPos;
    public GameObject soaringBullet;
    public float soaringDelay1 = 2f;

    [Space(1)]
    [Header("트윈 스타즈")]
    public Transform stunPos;
    public GameObject stunBullet1;
    public GameObject stunBullet2;
    public float stunDelay1 = 0.3f;
    public float stunDelay2 = 1.2f;



    private float skillCounter;

    private SkillSet.skill isUsingSkill = SkillSet.skill.none;

    private bool isHealing;
    private bool isAttacked = false;
    private bool inAir = false;
    private bool isEffectOnce = true;

    private PlayerMove playerMove;
    private Rigidbody2D rigid;
    private Animator animator;

    private GameObject effectObj;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = this.gameObject.GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        meteorSprite1.color = new Color(1, 1, 1, 0);
        meteorSprite2.color = new Color(1, 1, 1, 0);
        pointSprite.color = new Color(1, 1, 1, 0);
        stellaSprite.color = new Color(1, 1, 1, 0);
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
        twin_stars();
    }

    void check_input()
    {
        if (isUsingSkill == SkillSet.skill.none && !isHealing && Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetButtonDown("heal") && playerMove.IsGrounded)
            {
                playerMove.PauseMove();
                playerMove.PlayAnimation(PlayerMove.animationType.block, 1);
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
            case SkillSet.skill.hardSwing:
                if (PlayerSP.instance.CurSP > 0)
                {
                    playerMove.UseSkill(true);
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
                    playerMove.UseSkill(true);
                    PlayerSP.instance.modify_SP(-1);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
                }
                break;
            case SkillSet.skill.smite:
                if (PlayerSP.instance.CurSP > 2)
                {
                    playerMove.UseSkill(true);
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
            case SkillSet.skill.meteorBomb:
                if (PlayerSP.instance.CurSP > 0)
                {
                    playerMove.UseSkill(true);
                    PlayerSP.instance.modify_SP(-1);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
                }
                break;
            case SkillSet.skill.pillarOfLight:
                if (PlayerSP.instance.CurSP > 2)
                {
                    playerMove.UseSkill(true);
                    PlayerSP.instance.modify_SP(-3);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
                }
                break;
            case SkillSet.skill.twinstars:
                if (PlayerSP.instance.CurSP > 0)
                {
                    playerMove.UseSkill(true);
                    PlayerSP.instance.modify_SP(-1);
                    if (!playerMove.IsGrounded)
                    {
                        rigid.gravityScale = 0;
                        inAir = true;
                    }
                    isUsingSkill = skilltype;
                }
                break;
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

            if (isEffectOnce)
            {
                effectObj = ParticleManager.instance.particle_generation(ParticleManager.particleType.HealEffect, new Vector3(this.transform.position.x, this.transform.position.y + 0.75f, this.transform.position.z));
                isEffectOnce = false;
            }
            if (skillCounter > 1.5f)
            {
                if (PlayerSP.instance.CurSP > 0)
                {
                    PlayerSP.instance.modify_SP(-1);
                    if (PlayerHealth.instance.CurHP < PlayerHealth.instance.MaxHP)
                    {
                        ParticleManager.instance.particle_generation(ParticleManager.particleType.SendHelp, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
                        PlayerHealth.instance.modify_HP(1);
                    }
                }
                skillCounter = 0;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                isHealing = false;
                isEffectOnce = true;
                skillCounter = 0;
                playerMove.RestartMove();
                playerMove.PlayAnimation(PlayerMove.animationType.block, 0);
                Destroy(effectObj);
            }
        }

    }

    //private IEnumerator Heal_Effect()
    //{
    //    while (!Input.GetKeyUp(KeyCode.S))
    //    {
    //        GameObject obj = ParticleManager.instance.particle_generation(ParticleManager.particleType.HealEffect, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));

    //        yield return 1.0f;

    //        Destroy(obj);
    //    }

    //    yield return null;
    //}

    private void stella_strike()
    {
        if (isUsingSkill == SkillSet.skill.hardSwing)
        {
            if (skillCounter == 0)
            {
                animator.SetTrigger("Attack1");
                stellaSprite.color = new Color(1, 1, 1, 1);
                stellaEffect.SetTrigger("Go");
            }

            if (skillCounter > stellaDelay1 + 0.1f && isAttacked)
            {
                stellaSprite.color = new Color(1, 1, 1, 0);
            }

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

            if (skillCounter > stellaDelay1 + stellaDelay2)
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

            if (skillCounter > pointDelay1 - 0.15f && !isAttacked)
            {
                animator.SetTrigger("Attack1");
                pointSprite.color = new Color(1, 1, 1, 1);
            }
            if (skillCounter > pointDelay1 + 0.15f && isAttacked)
            {
                pointSprite.color = new Color(1, 1, 1, 0);
            }

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

            if (skillCounter > pointDelay1 + pointDelay2)
            {
                end_skill();
            }
        }
    }
    private void meteorite_slash()
    {
        if (isUsingSkill == SkillSet.skill.smite)
        {
            if (!inAir)
            {
                if (skillCounter == 0)
                {
                    animator.SetTrigger("Attack1");
                    meteorSprite1.color = new Color(1, 1, 1, 1);
                    meteorEffect1.SetTrigger("Go");
                }
                skillCounter += Time.deltaTime;

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

                if (skillCounter > meteorDelay1 + 0.1f)
                {
                    meteorSprite1.color = new Color(1, 1, 1, 0);
                }

                if (skillCounter > meteorDelay1 + meteorDelay2)
                {
                    end_skill();
                }
            }

            else
            {
                if (skillCounter == 0)
                {
                    animator.SetTrigger("Attack1");
                    meteorSprite2.color = new Color(1, 1, 1, 1);
                    meteorEffect2.SetTrigger("Go");
                }

                skillCounter += Time.deltaTime;

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

                if (endTime == 0 && (playerMove.IsGrounded || groundCheck != null))
                {
                    endTime = skillCounter;

                    meteorEffect2.SetBool("OnGround", true);

                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<EnemyData>().TakeDamage(15 * (ProgressData.Instance.reinforcementCount + 1) + (skillCounter / 0.3f) * 1);
                            Debug.Log(collider.name + " 에게 " + (15 * (ProgressData.Instance.reinforcementCount + 1) + (skillCounter / 0.3f) * 1) + "의 데미지를 입히고 2초간 스턴 효과 부여");
                        }
                    }
                }

                if (endTime != 0 && !isAttacked && skillCounter > endTime + 0.3f)
                {
                    isAttacked = true;
                    meteorEffect2.SetBool("OnGround", false);
                    meteorSprite2.color = new Color(1, 1, 1, 0);
                }

                if (skillCounter > endTime + meteorDelay2)
                {
                    endTime = 0;
                    end_skill();
                }
            }
        }
    }


    private void freyja()
    {
        if (isUsingSkill == SkillSet.skill.meteorBomb)
        {
            skillCounter += Time.deltaTime;

            if (!isAttacked)
            {
                animator.SetTrigger("Attack1");
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
        if (isUsingSkill == SkillSet.skill.pillarOfLight)
        {
            skillCounter += Time.deltaTime;

            if (!isAttacked)
            {
                animator.SetTrigger("Attack1");
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
                    if (enemy.TryGetComponent<EnemyData>(out EnemyData a))
                    {
                        float dis = ((Vector2)(this.transform.position - enemy.transform.position)).sqrMagnitude;

                        if (dis < shortestDis)
                        {
                            target = enemy;
                            shortestDis = dis;
                        }
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
    private void twin_stars()
    {
        if (isUsingSkill == SkillSet.skill.twinstars)
        {
            if (skillCounter == 0)
            {
                animator.SetTrigger("Attack1");
                GameObject instance = Instantiate(stunBullet1, stunPos.position, stunPos.rotation);
                instance.GetComponent<TwinStars>().dir = (int)playerMove.LastDirection;
            }

            skillCounter += Time.deltaTime;

            if (!isAttacked && skillCounter > stunDelay1)
            {
                isAttacked = true;

                GameObject instance = Instantiate(stunBullet2, stunPos.position, stunPos.rotation);
                instance.GetComponent<TwinStars>().dir = (int)playerMove.LastDirection;
            }

            if (skillCounter > freyjaDelay1)
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
