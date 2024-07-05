using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정보창 관리 스크립트

/*대충 기본 아이디어
    탭을 전부 리스트에 저장
    리스트 인덱스에 따라 위치 조정
    0은 지금 보여질거 마지막,1은 각각 좌우
    0번이 되면 화면 중앙으로 부드럽게 이동
    마지막,1은 각각 0 양 옆에 붙음
    부드럽게 잘 움직인다
 */
public class InspectorManager : MonoBehaviour
{
    private List<GameObject> info = new List<GameObject>();//정보창의 탭들을 저장할 리스트

    // Start is called before the first frame update
    void Start()
    {
        info.Add(GameObject.Find("Stat"));//info 리스트에 탭 저장
        info.Add(GameObject.Find("Item"));
        info.Add(GameObject.Find("Constellation"));
    }

    // Update is called once per frame
    void Update()
    {
        set_pos();
    }

    public void move_info(int dir) //탭들의 리스트 속 위치를 바꿔주는 함수
    {

        if (dir > 0)//>방향
        {//제일 끝의 탭을 0번에 복사하고 제일 끝의 탭을 지움
            info.Insert(0, info[info.Count - 1]);
            info.RemoveAt(info.Count - 1);
        }
        else//<방향
        {//첫번째 탭을 리스트의 끝에 복사하고 첫번째 탭을 지움
            info.Add(info[0]);
            info.RemoveAt(0);
        }
    }

    private void set_pos()//탭들의 리스트속 위치에 따라 실제 위치를 바꾸어주는 함수
    {
        if (Mathf.Abs(info[0].GetComponent<RectTransform>().offsetMin.x) > 1)
        //0번 탭을 화면 중심으로 움직이게한다.
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

        //1번 탭과 마지막 탭을 각각 0번 탭의 좌우로 이동시킨다
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



        for (int i = 2; i < info.Count - 1; i++)//나머지 탭을 대충 모아놓는 코드
        {
            info[i].GetComponent<RectTransform>().offsetMin
                = new Vector2(1920, 0);
            info[i].GetComponent<RectTransform>().offsetMax
                = new Vector2(1920, 0);
        }
    }
}
