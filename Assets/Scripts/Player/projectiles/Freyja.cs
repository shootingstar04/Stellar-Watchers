using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freyja : MonoBehaviour
{
    public int dir=0;

    private bool islaunched = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(charging());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (islaunched)
        {
            if (collision.CompareTag(Define.GroundTag) || collision.CompareTag(Define.OneWayTag))
            {
                Destroy(this.gameObject);
            }
            else if (collision.CompareTag(Define.EnemyTag))
            {
                collision.GetComponent<EnemyData>().TakeDamage(10 * (ProgressData.Instance.reinforcementCount + 1));
                Debug.Log(collision.name + " 에게 " + 10 * (ProgressData.Instance.reinforcementCount + 1) + "의 데미지를 입힘");
            }
            else if (collision.tag == "BOMB")
            {
                Debug.Log("폭탄 맞음");
                Bomb.onFire();
            }
            else if (collision.GetComponent<ElevatorSwitch>() != null)
            {
                Debug.Log("스위치 작동");
                collision.GetComponent<ElevatorSwitch>().SwitchFlick();
            }
            else if (collision.GetComponent<Chest>() != null)
            {
                collision.gameObject.GetComponent<Chest>().Distroyed();
            }
            else if (collision.GetComponent<GeneralDoor>() != null)
            {
                collision.GetComponent<GeneralDoor>().OpenDoor();
            }
        }
    }

    private void launch()
    {
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();

        rigid.velocity = new Vector2(dir * 15, 0);
    }

    private IEnumerator charging()
    {
        Color color = Color.white;
        color.a = 0;
        this.GetComponent<SpriteRenderer>().color = color;
        for (int i = 0; i < 60; i++)
        {
            color.a += 1 / 60.0f;
            this.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(1 / 60.0f);
        }
        launch();
        islaunched = true;


        yield return new WaitForSeconds(4);

        Destroy(this.gameObject);
    }
}
