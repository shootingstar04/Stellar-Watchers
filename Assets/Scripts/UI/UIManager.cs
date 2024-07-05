using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI창을 열고 닫는 것을 관리하는 스크립트

/*대충 기본 아이디어
    XXscreen에 XX의 구성요소를 전부 자식으로 넣는다
    상황에 따라서 XXscreen들을 비활성화하거나 활성화한다
    자식들도 같이 꺼지고 켜진다
 */
public class UIManager : MonoBehaviour
{
    private List<GameObject> screen = new List<GameObject>();//UI창 저장
    private List<bool> isShowing = new List<bool>();//UI이 보이고 있는지 저장
    //0 : Inspector, 1 : Map

    public bool isShowingUI = false;//UI를 뭐라도 보여주고 있나?
    private GameObject pauseScreen;//일시정지창 저장
    private bool isPause = false;//멈췄나?


    // Start is called before the first frame update
    void Start()
    {
        screen.Add(GameObject.Find("inspectorscreen"));//창 저장
        screen.Add(GameObject.Find("mapscreen"));
        pauseScreen = GameObject.Find("pausescreen");

        for (int i = 0; i < screen.Count; i++) {//창 다 끄기
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

    //입력에 따라 UI보여주는 함수 호출
    void check_input()
    {
        if (Input.GetKeyDown(KeyCode.I)) show_screen(0);
        if (Input.GetKeyDown(KeyCode.M)) show_screen(1);
        if (Input.GetKeyDown(KeyCode.Escape)) show_screen(-1);
    }

    //UI보여주는 함수
    //-1: 일시정지 0: 정보창 1: 지도
    void show_screen(int a)
    {
        if (a == -1) //ESC누르면
        {
            if (isShowingUI)//UI가 켜져있으면 UI다 끄는 코드
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
            else//UI가 안 켜져있으면 일시정지창 띄우기
            {
                isPause = !isPause;
                Time.timeScale = Time.timeScale == 1 ? 0 : 1;
                pauseScreen.SetActive(isPause);
            } 
        }
        else if (screen[a] && !isPause)//esc를 누른게 아니다
        {//각 키에 따른 UI를 띄우는 코드
            isShowingUI = false;

            if (!isShowing[a])//UI가 꺼져있으면
            {
                isShowingUI = true;// UI 이제 보여준다

                for (int i = 0; i < isShowing.Count; i++)//다른 UI다끄는 코드
                {
                    if (isShowing[i])
                    {
                        isShowing[i] = false;
                        screen[i].SetActive(isShowing[i]);
                    }
                }
            }
            
            isShowing[a] = !isShowing[a];//UI를 꺼져있으면 키고 켜져있으면 끈다
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;//시간 멈춤
            screen[a].SetActive(isShowing[a]);
        }
    }
}
