using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<GameObject> screen = new List<GameObject>();
    private List<bool> isShowing = new List<bool>();
    //0 : Inspector, 1 : Map


    // Start is called before the first frame update
    void Start()
    {
        screen.Add(GameObject.Find("inspectorscreen"));
        screen.Add(GameObject.Find("mapscreen"));

        for (int i = 0; i < screen.Count; i++) {
            isShowing.Add(false);
            if (screen[i]) screen[i].SetActive(isShowing[i]);
        }


    }

    // Update is called once per frame
    void Update()
    {
        check_input();
    }

    void check_input()
    {
        if (Input.GetKeyDown(KeyCode.I)) show_screen(0);
        if (Input.GetKeyDown(KeyCode.M)) show_screen(1);
        if (Input.GetKeyDown(KeyCode.Escape)) show_screen(-1);
    }
    void show_screen(int a)
    {
        if (a == -1) 
        {
            for (int i = 0; i < isShowing.Count; i++)
            {
                if (isShowing[i])
                {
                    isShowing[i] = false;
                    screen[i].SetActive(isShowing[i]);
                }
            }
        }
        else if (screen[a])
        {
            if (!isShowing[a])
            {
                for (int i = 0; i < isShowing.Count; i++)
                {
                    if (isShowing[i])
                    {
                        isShowing[i] = false;
                        screen[i].SetActive(isShowing[i]);
                    }
                }
            }
            
            isShowing[a] = !isShowing[a];
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            screen[a].SetActive(isShowing[a]);
        }
    }
}
