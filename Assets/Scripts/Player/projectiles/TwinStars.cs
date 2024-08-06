using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStars : MonoBehaviour
{
    public int dir = 0;

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
                collision.GetComponent<EnemyData>().TakeDamage(7.5f * (ProgressData.Instance.reinforcementCount + 1));
                Debug.Log(collision.name + " ¿¡°Ô " + 7.5f * (ProgressData.Instance.reinforcementCount + 1) + "ÀÇ µ¥¹ÌÁö¸¦ ÀÔÈû");
            }
            else if (collision.tag == "BOMB")
            {
                Debug.Log("ÆøÅº ¸ÂÀ½");
                Bomb.onFire();
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

        rigid.velocity = new Vector2(dir * 50, 0);

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(this.transform.position, 0.5f);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider != null)
            {
                if (collider.CompareTag(Define.GroundTag) || collider.CompareTag(Define.OneWayTag))
                {
                    Destroy(this.gameObject);
                }
                else if (collider.CompareTag(Define.EnemyTag))
                {
                    collider.GetComponent<EnemyData>().TakeDamage(7.5f * (ProgressData.Instance.reinforcementCount + 1));
                    Debug.Log(collider.name + " ¿¡°Ô " + 7.5f * (ProgressData.Instance.reinforcementCount + 1) + "ÀÇ µ¥¹ÌÁö¸¦ ÀÔÈû");
                    Destroy(this.gameObject);
                }
                else if (collider.tag == "BOMB")
                {
                    Debug.Log("ÆøÅº ¸ÂÀ½");
                    Bomb.onFire();
                    Destroy(this.gameObject);
                }
                else if (collider.GetComponent<Chest>() != null)
                {
                    collider.gameObject.GetComponent<Chest>().Distroyed();
                    Destroy(this.gameObject);
                }
                else if (collider.GetComponent<GeneralDoor>() != null)
                {
                    collider.GetComponent<GeneralDoor>().OpenDoor();
                    Destroy(this.gameObject);
                }
            }
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }
}
