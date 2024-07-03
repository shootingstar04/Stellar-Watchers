using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == Player.name)
        {
            P_rigd.AddForce(transform.up * BoostPower);
        }
    }
}
