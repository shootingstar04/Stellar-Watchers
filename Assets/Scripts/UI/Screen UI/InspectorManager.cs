using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InspectorManager : MonoBehaviour
{
    public List<GameObject> info = new List<GameObject>();

    private UIManager UIManager;

    // Start is called before the first frame update
    void Awake()
    {
        info.Add(GameObject.Find("Progress"));
        info.Add(GameObject.Find("Collection"));
        info.Add(GameObject.Find("Constellation"));

        UIManager = GameObject.Find("PopUpUI").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        check_input();
        set_pos();
    }

    private void OnEnable()
    {
        if (info[0].transform.Find("Buttons").transform.Find("start"))
        {
            info[0].transform.Find("Buttons").transform.Find("start").GetComponent<Button>().Select();
        }
    }



    void check_input()
    {

        if (Time.timeScale == 0 && !UIManager.showingPopUp)
        {
            if (Input.GetKeyDown(KeyCode.A)) move_info(-1);
            else if (Input.GetKeyDown(KeyCode.D)) move_info(1);
        }
    }

    public void move_info(int dir) 
    {

        if (dir > 0)//>����
        {
            info.Add(info[0]);
            info.RemoveAt(0);
        }
        else//<����
        {
            info.Insert(0, info[info.Count - 1]);
            info.RemoveAt(info.Count - 1);
        }

        if(info[0].transform.Find("Buttons").transform.Find("start"))
            info[0].transform.Find("Buttons").transform.Find("start").GetComponent<Button>().Select();
    }

    private void set_pos()
    {

        if (Mathf.Abs(info[0].GetComponent<RectTransform>().offsetMin.x) > 1)
        {
            info[0].transform.position
                = new Vector3(info[0].transform.position.x, info[0].transform.position.y, -3);
            info[0].GetComponent<RectTransform>().offsetMin
                = new Vector2(info[0].GetComponent<RectTransform>().offsetMin.x * Mathf.Pow(0.95f, 100 * Time.unscaledDeltaTime), 0);
            info[0].GetComponent<RectTransform>().offsetMax
                = new Vector2(info[0].GetComponent<RectTransform>().offsetMax.x * Mathf.Pow(0.95f, 100 * Time.unscaledDeltaTime), 0);
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
