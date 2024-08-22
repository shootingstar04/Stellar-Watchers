using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class secret_wall : MonoBehaviour
{
    [SerializeField] int mapnum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            Color color = Color.white;
            confinderChange.instance.ConfinderChangeSecret(mapnum);

            color.a = 0.5f;
            this.GetComponent<Tilemap>().color = color;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            Color color = Color.white;
            this.GetComponent<Tilemap>().color = color;
        }
    }
}
