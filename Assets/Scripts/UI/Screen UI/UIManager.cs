using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<GameObject> screen = new List<GameObject>();
    private List<bool> isShowing = new List<bool>();
    //0 : Inspector, 1 : Map

    private bool isShowingUI = false;
    public bool showingPopUp = false;
    private GameObject pauseScreen;
    private bool isPause = false;

    public bool ShowingPopUp
    {
        get 
        {
            return showingPopUp;
        }    
    }


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


        Cursor.visible = isShowingUI;
        Cursor.lockState = CursorLockMode.Locked;

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
        else if (Input.GetKeyDown(KeyCode.X) && showingPopUp) showingPopUp = false;
    }
    void show_screen(int a)
    {
        if (a == -1) 
        {
            if (!showingPopUp)
            {
                if (isShowingUI)
                {
                    isShowingUI = false;
                    Time.timeScale = 1;

                    Cursor.visible = isShowingUI;
                    Cursor.lockState = CursorLockMode.Locked;

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
                    Cursor.visible = true;
                    Cursor.lockState =
                        isPause ? CursorLockMode.None : CursorLockMode.Locked;
                    Time.timeScale = Time.timeScale == 1 ? 0 : 1;
                    pauseScreen.SetActive(isPause);
                }
            }
            else showingPopUp = false;
        }
        else if (!isPause && !showingPopUp)
        {

            isShowingUI = false;
            Cursor.lockState = CursorLockMode.Locked;


            if (!isShowing[a])
            {

                isShowingUI = true;
                Cursor.lockState = CursorLockMode.None;

                for (int i = 0; i < isShowing.Count; i++)
                {
                    if (isShowing[i])
                    {
                        isShowing[i] = false;
                        screen[i].SetActive(isShowing[i]);
                        Time.timeScale = 1;
                    }
                }
            }



            Cursor.visible = isShowingUI;
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            isShowing[a] = !isShowing[a];
            screen[a].SetActive(isShowing[a]);
        }
    }

    public void show_popup() 
    {
        showingPopUp = true;
    }

    public void off_PopUp()
    {
        showingPopUp = false;
    }
}
