using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<GameObject> screen = new List<GameObject>();
    private List<bool> isShowing = new List<bool>();
    //0 : Inspector, 1 : Map

    public bool isShowingUI = false;
    public bool showingPopUp = false;
    private GameObject pauseScreen;
    private bool isPause = false;


    // Start is called before the first frame update
    void Start()
    {
        screen.Add(GameObject.Find("inspectorscreen"));
        screen.Add(GameObject.Find("mapscreen"));

        pauseScreen = GameObject.Find("pausescreen");

        for (int i = 0; i < screen.Count; i++) {
            isShowing.Add(false);
            if (screen[i]) screen[i].SetActive(isShowing[i]);
        }

        if (pauseScreen) pauseScreen.SetActive(isPause);

    }

    // Update is called once per frame
    void Update()
    {
        check_input();
    }

    void check_input()
    {
        if (Input.GetKeyDown(KeyCode.I)) show_screen(0);
        else if (Input.GetKeyDown(KeyCode.M)) show_screen(1);
        else if (Input.GetKeyDown(KeyCode.Escape)) show_screen(-1);
    }
    void show_screen(int a)
    {
        if (a == -1 && !showingPopUp) 
        {
            if (isShowingUI)
            {
                isShowingUI = false;
                for (int i = 0; i < isShowing.Count; i++)
                {
                    if (isShowing[i])
                    {
                        isShowing[i] = false;
                        screen[i].SetActive(isShowing[i]);
                    }
                }
            }
            else
            {
                isPause = !isPause;
                Time.timeScale = Time.timeScale == 1 ? 0 : 1;
                pauseScreen.SetActive(isPause);
            } 
        }
        else if (screen[a] && !isPause)
        {
            isShowingUI = false;

            if (!isShowing[a])
            {
                isShowingUI = true;

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

    void show_popup() 
    {
        showingPopUp = !showingPopUp;
    }
}
