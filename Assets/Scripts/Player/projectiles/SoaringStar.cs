using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoaringStar : MonoBehaviour
{
    public Transform bottom;

    private GameObject target = null;

    private float skillcount = 0;
    private float xRange = 0;

    private List<GameObject> hitted;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        move_to_target();
        charge();
    }

    private void move_to_target()
    { 
        if (target != null)
        {
            
            Vector2 Pos = target.transform.position;

            Pos.y += 3f;

            this.transform.position = Pos;

            Collider2D ground = Physics2D.OverlapCircle(bottom.position, 0.1f, LayerMask.NameToLayer("Ground"));

            while (ground == null)
            {
                Debug.Log(1);
                Pos.y -= 0.1f;
                this.transform.position = Pos;

                ground = Physics2D.OverlapCircle(bottom.position, 0.1f, LayerMask.NameToLayer("Ground"));
            }
        }
    }

    private void charge()
    {
        if (skillcount > 4f)
        {
            Destroy(this.gameObject);
        }

        skillcount += Time.deltaTime;
        if (skillcount > 2f)
        {
            xRange += 1.5f * Time.deltaTime;
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(xRange, 7), 0);

            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && collider.tag == "Enemy")
                {
                    if (!hitted.Contains(collider.gameObject))
                    {
                        hitted.Add(collider.gameObject);
                    }
                }
            }
        }
    }

    IEnumerator attack()
    {
        while (true)
        {
            foreach (GameObject enemy in hitted)
            {
                Debug.Log(enemy.name);
                enemy.GetComponent<EnemyData>().TakeDamage(5);
                Debug.Log(enemy.name + " 에게 " + 10 * (ProgressData.Instance.reinforcementCount + 1) + "의 데미지를 입힘");
            }

            yield return new WaitForSeconds(1 / 15);
        }
    }

    public void set_target(GameObject enemy)
    {
        target = enemy;
        if (enemy != null)
        {
            Debug.Log("set target, " + enemy.name);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(xRange, 7));
    }
}
