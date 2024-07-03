using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
<<<<<<< HEAD
    private float bounce = 20f;
=======
    private GameObject Player;
    private Rigidbody2D P_rigd;
    private Collider2D col;
    private Collider2D p_col;

    private float BoostPower = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        P_rigd = Player.GetComponent<Rigidbody2D>();
        p_col = Player.GetComponent<Collider2D>();
        col = this.GetComponent<Collider2D>();
    }
>>>>>>> 794d122833713cfc82e9d187eb9a57b5ae4bb56f

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == Player.name)
        {
<<<<<<< HEAD
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
=======
            P_rigd.AddForce(transform.up * BoostPower);
>>>>>>> 794d122833713cfc82e9d187eb9a57b5ae4bb56f
        }
    }
}
