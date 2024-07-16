using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public enum type 
    {
        collect = 0,
        star
    }

    public type itemType;// 0: collect, 1: star, 2: key 
    public int itemNum;

    private GameObject interact;
    private GameObject item;
    private bool isActived = false;

    private ParticleSystem p1;
    private ParticleSystem p2;


    // Start is called before the first frame update
    void Start()
    {
        item = this.transform.Find("item").gameObject;
        interact = this.transform.Find("interact").gameObject;

        p1 = this.transform.Find("Particle System").GetComponent<ParticleSystem>();
        p2 = this.transform.Find("Particle System").Find("Particle System").GetComponent<ParticleSystem>();

        interact.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActived && collision.GetComponent<CollectInventory>() != null) {
            interact.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isActived && collision.GetComponent<CollectInventory>() != null)
        {
            interact.SetActive(false);
        }
    }

    public IEnumerator destroy_item()
    {
        isActived = true;
        item.SetActive(false);
        interact.SetActive(false);

        p1.Stop();
        p2.Stop();

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
