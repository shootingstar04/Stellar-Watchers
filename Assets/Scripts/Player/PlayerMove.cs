using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float jumpForce = 10f;
    private float jumpTimeCounter;
    private float maxJumpTime = 0.5f;
    private float maxDbJumpTime = 0.3f;
    private float maxJumpHeight = 5f;
    private float maxDbJumpHeight = 3f;
    private bool dbJumpCount = true;

    private float moveSpeed = 5f;
    private float lastDirection = 1f;

    private float dashSpeed = 20f;
    private float dashDuration = 0.2f;
    private float dashCoolDown = 0.1f;

    private float invinDuration = 1f;

    private bool isJumping = false;
    private bool isDbJumping = false;
    private bool isGrounded = true;
    private bool isDashing = false;
    private bool isInvins = false;

    private float dashTimeCounter;
    private float invincibilityCounter;

    private Collider2D col;
    private Rigidbody2D rigid;

    [SerializeField] private LayerMask Jumpables;
    private Transform jumpCheck;
    private float groundCheckRadius = 0.2f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpCheck = transform.Find("GroundCheck");

    }

    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        if (!isDashing)
        {
            float moveInput = Input.GetAxis("Horizontal");

            if (moveInput != 0)
            {
                lastDirection = moveInput > 0 ? 1f : -1f;
            }

            if (moveInput < 0)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else if (moveInput > 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            rigid.velocity = new Vector2(moveInput * moveSpeed, rigid.velocity.y);
        }
    }

    private void Jump()
    {
        //������ 2������ ���� - �⺻ ����, 2��(�ر�)����
        //������ ������ ����
        //�⺻ : ���� ����� �� 1�� ����. ���� �ٽ� ��Ҵٸ� �ʱ�ȭ <<- isGrounded�� true�� ����
        //2��  : �ر� �ʿ� && ���߿� ���� �� 1�� ����. ���� ��Ե� �ٽ� ���߿� �ִٸ� �ʱ�ȭ
        isGrounded = Physics2D.OverlapCircle(jumpCheck.position, groundCheckRadius, Jumpables); //���� �ӵ��� 0�϶��� �����ϰ� �ϴ� ������ �ʿ��Ѱ� -- �̸��¸� �������� ���� ���� ���ƴ� - �̶��� ���� �ʱ�ȭ o/x�� ����?

        if (isGrounded) //�̺κ� ���� �ȵ�µ�... - �������� ���� ���� --> �̰� ���� ���� �� 2������ ����, ���������� ������ �� 2�������� �����ϰ� �Ͽ��� - �������� 2������ ī��Ʈ �ʱ�ȭ.(true�� ���� ������ ���� - ���� int�� �Ƚ���.) 
        {
            dbJumpCount = true;
        }

        //---------�⺻����----------------

        if (isGrounded && Input.GetKeyDown(KeyCode.Z))                          //������ ���� && Ű ������ ��(����)
        {
            Debug.Log("�⺻����");
            isJumping = true;                                                  //���� ��
            jumpTimeCounter = 0f;                                              //maxJump���� ����ϴ� Ÿ�̸�
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }

        if (isJumping && Input.GetKey(KeyCode.Z))                               //�������϶��� && Ű ���� ����(����)
        {
            if (jumpTimeCounter < maxJumpTime)                                  //Ÿ�̸Ӱ� maxJump�̸��϶���
            {
                Debug.Log("�⺻����");
                jumpTimeCounter += Time.deltaTime;
                float t = jumpTimeCounter / maxJumpTime;
                float currentJumpForce = Mathf.Lerp(jumpForce, maxJumpHeight, t);
                rigid.velocity = new Vector2(rigid.velocity.x, currentJumpForce);
            }
            else                                                                //Ÿ�̸Ӱ� �Ѿ��
            {
                isJumping = false;                                              //���� ���� �ı�
            }
        }

        if (isJumping && Input.GetKeyUp(KeyCode.Z))                             //���� �������϶�(������) && ������ Ű�� ����
        {
            isJumping = false;                                                  //���� ���� �ı�
        }

        if (!isGrounded && !isJumping && rigid.velocity.y > 0)                  //��������? - ���߿��� && ������(������)�ƴ� && y������϶� <-- �Ƹ� ���� �ı� �� ���� �ߴ� �κ��ϵ�
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f); //1/2�� y�ӷ� ����
        }

        //-------------2������---------------------

        if (!isGrounded && Input.GetKeyDown(KeyCode.Z) && dbJumpCount)         //���߿� ���� �� && Ű ������ �� && ����ī��Ʈ ������ ��(����)
                                                                               //�߰��� ���⿡�� && (�ر� ���� Ȯ��) �߰��� ��
        {
            Debug.Log("2������");
            jumpTimeCounter = 0;                                               //Ÿ�̸� �ʱ�ȭ - ���� �Ȱ��ļ� - ������ �̰� �ߵ��ϸ� �����Է� �����°Ŵϱ� - ��Ȱ���ص� ������µ�
            isDbJumping = true;                                                //����������
            dbJumpCount = false;                                               //����ī��Ʈ ���� (1 -> 0)
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }

        if (isDbJumping && Input.GetKey(KeyCode.Z))                            //���������� && Ű ������ ��(����)
        {
            if (jumpTimeCounter < maxDbJumpTime)                               //Ÿ�̸Ӱ� maxDbJump�̸��϶���
            {
                Debug.Log("2�ܿ���");
                jumpTimeCounter += Time.deltaTime;
                float t = jumpTimeCounter / maxDbJumpTime;
                float currentJumpForce = Mathf.Lerp(jumpForce, maxDbJumpHeight, t);
                rigid.velocity = new Vector2(rigid.velocity.x, currentJumpForce);
            }
            else                                                               //Ÿ�̸Ӱ� �Ѿ��
            {
                isDbJumping = false;                                           //�������� �ı�
            }
        }

        if (isDbJumping && Input.GetKeyUp(KeyCode.Z))                          //�������̴��� && Ű�� ����
        {
            isDbJumping = false;                                               //���� ���� �ı�
        }

        if (!isGrounded && !isDbJumping && rigid.velocity.y > 0)               //�̰͵� �������� �����ٸ� �ӵ� ���ӽ�Ű�� �κ��ϵ�
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }

    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            isInvins = true;
            dashTimeCounter = dashDuration;
            rigid.velocity = new Vector2(dashSpeed * lastDirection, rigid.velocity.y);

        }
        if (isDashing)
        {
            gameObject.layer = 8;
            dashTimeCounter -= Time.deltaTime;
            if (dashTimeCounter <= 0)
            {
                isInvins = false;
                dashTimeCounter = dashCoolDown;
                dashTimeCounter -= Time.deltaTime;
                if (dashTimeCounter <= 0)
                {
                    isDashing = false;
                }
            }
        }
    }
}