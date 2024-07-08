using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorManager : MonoBehaviour
{
    private List<GameObject> info = new List<GameObject>();

    private float mid = 1920f, left = 1920f, right = 1920f;

    // Start is called before the first frame update
    void Start()
    {
        info.Add(GameObject.Find("Stat"));
        info.Add(GameObject.Find("Item"));
        info.Add(GameObject.Find("Constellation"));
    }

    // Update is called once per frame
    void Update()
    {
        set_pos();
    }

    public void move_info(int dir) 
    {

        if (dir > 0)//>방향
        {
            info.Insert(0, info[info.Count - 1]);
            info.RemoveAt(info.Count - 1);
        }
        else//<방향
        {
            info.Add(info[0]);
            info.RemoveAt(0);
        }
    }

    private void set_pos()
    {
        if (Mathf.Abs(info[0].GetComponent<RectTransform>().offsetMin.x) > 1)
        {
            info[0].transform.position
                = new Vector3(info[0].transform.position.x, info[0].transform.position.y, -3);
            info[0].GetComponent<RectTransform>().offsetMin
                = new Vector2(info[0].GetComponent<RectTransform>().offsetMin.x * 0.98f, 0);
            info[0].GetComponent<RectTransform>().offsetMax
                = new Vector2(info[0].GetComponent<RectTransform>().offsetMax.x * 0.98f, 0);
        }
        else
        {
            info[0].GetComponent<RectTransform>().offsetMin = Vector2.zero;
            info[0].GetComponent<RectTransform>().offsetMax = Vector2.zero;
        }

        info[1].transform.position
            = new Vector3(info[1].transform.position.x, info[0].transform.position.y, -2);
        info[1].GetComponent<RectTransform>().offsetMin
            = new Vector2(info[0].GetComponent<RectTransform>().offsetMin.x + 1920, 0);
        info[1].GetComponent<RectTransform>().offsetMax
            = new Vector2(info[0].GetComponent<RectTransform>().offsetMin.x + 1920, 0);

        info[info.Count - 1].transform.position
            = new Vector3(info[1].transform.position.x, info[0].transform.position.y, -2);
        info[info.Count - 1].GetComponent<RectTransform>().offsetMin
            = new Vector2(info[0].GetComponent<RectTransform>().offsetMin.x - 1920, 0);
        info[info.Count - 1].GetComponent<RectTransform>().offsetMax
            = new Vector2(info[0].GetComponent<RectTransform>().offsetMin.x - 1920, 0);



        for (int i = 2; i < info.Count - 1; i++)
        {
            info[i].GetComponent<RectTransform>().offsetMin
                = new Vector2(1920, 0);
            info[i].GetComponent<RectTransform>().offsetMax
                = new Vector2(1920, 0);
        }
    }
}
