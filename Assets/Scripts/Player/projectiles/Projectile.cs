using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;
    public float projectileSpeed = 20f;

    [HideInInspector]
    public int dir;

    private float lifeCounter = 0;
    void Start()
    {
        Debug.Log(1);
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();

        rigid.velocity = new Vector2(dir * projectileSpeed, 0);
    }

    void Update()
    {
        lifeCounter += Time.deltaTime;
        if (lifeCounter > 4f)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject collider = collision.gameObject;

        if (collision.CompareTag(Define.EnemyTag))
        {
            collision.GetComponent<EnemyData>().TakeDamage(5 * (ProgressData.Instance.reinforcementCount + 1));
            Debug.Log(collision.name + " 에게 " + 5 * (ProgressData.Instance.reinforcementCount + 1) + "의 데미지를 입힘");
            //PlayerSP.instance.attackCount += 1;
            Destroy(this.gameObject, 0);
        }
        else if (collision.tag == "BOMB")
        {
            Debug.Log("폭탄 맞음");
            Bomb.onFire();
            Destroy(this.gameObject, 0);
        }
        else if (collision.GetComponent<ElevatorSwitch>() != null)
        {
            Debug.Log("스위치 작동");
            collision.GetComponent<ElevatorSwitch>().SwitchFlick();
            Destroy(this.gameObject, 0);
        }
        else if (collision.GetComponent<Chest>() != null)
        {
            collision.gameObject.GetComponent<Chest>().Distroyed();
            Destroy(this.gameObject, 0);
        }
        else if (collision.GetComponent<GeneralDoor>() != null)
        {
            collision.GetComponent<GeneralDoor>().OpenDoor();
            Destroy(this.gameObject, 0);
        }
        else if (collision.CompareTag(Define.GroundTag))
        {
            Destroy(this.gameObject, 0);
        }
    }
}
