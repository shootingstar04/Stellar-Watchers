using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UIâ�� ���� �ݴ� ���� �����ϴ� ��ũ��Ʈ

/*���� �⺻ ���̵��
    XXscreen�� XX�� ������Ҹ� ���� �ڽ����� �ִ´�
    ��Ȳ�� ���� XXscreen���� ��Ȱ��ȭ�ϰų� Ȱ��ȭ�Ѵ�
    �ڽĵ鵵 ���� ������ ������
 */
public class UIManager : MonoBehaviour
{
    private List<GameObject> screen = new List<GameObject>();//UIâ ����
    private List<bool> isShowing = new List<bool>();//UI�� ���̰� �ִ��� ����
    //0 : Inspector, 1 : Map

    public bool isShowingUI = false;//UI�� ���� �����ְ� �ֳ�?
    private GameObject pauseScreen;//�Ͻ�����â ����
    private bool isPause = false;//���質?


    // Start is called before the first frame update
    void Start()
    {
        screen.Add(GameObject.Find("inspectorscreen"));//â ����
        screen.Add(GameObject.Find("mapscreen"));
        pauseScreen = GameObject.Find("pausescreen");

        for (int i = 0; i < screen.Count; i++) {//â �� ����
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

    //�Է¿� ���� UI�����ִ� �Լ� ȣ��
    void check_input()
    {
        if (Input.GetKeyDown(KeyCode.I)) show_screen(0);
        if (Input.GetKeyDown(KeyCode.M)) show_screen(1);
        if (Input.GetKeyDown(KeyCode.Escape)) show_screen(-1);
    }

    //UI�����ִ� �Լ�
    //-1: �Ͻ����� 0: ����â 1: ����
    void show_screen(int a)
    {
        if (a == -1) //ESC������
        {
            if (isShowingUI)//UI�� ���������� UI�� ���� �ڵ�
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
            else//UI�� �� ���������� �Ͻ�����â ����
            {
                isPause = !isPause;
                Time.timeScale = Time.timeScale == 1 ? 0 : 1;
                pauseScreen.SetActive(isPause);
            } 
        }
        else if (screen[a] && !isPause)//esc�� ������ �ƴϴ�
        {//�� Ű�� ���� UI�� ���� �ڵ�
            isShowingUI = false;

            if (!isShowing[a])//UI�� ����������
            {
                isShowingUI = true;// UI ���� �����ش�

                for (int i = 0; i < isShowing.Count; i++)//�ٸ� UI�ٲ��� �ڵ�
                {
                    if (isShowing[i])
                    {
                        isShowing[i] = false;
                        screen[i].SetActive(isShowing[i]);
                    }
                }
            }
            
            isShowing[a] = !isShowing[a];//UI�� ���������� Ű�� ���������� ����
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;//�ð� ����
            screen[a].SetActive(isShowing[a]);
        }
    }
}
