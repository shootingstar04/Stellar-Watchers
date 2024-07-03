using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameObject Player;
    private Collider2D P_Collider;
    private Collider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        P_Collider = Player.GetComponent<Collider2D>();
        Collider = this.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == Player.name)
        {
            Destroy(Player);
        }
    }
}
