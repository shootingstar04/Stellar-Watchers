using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//지도 UI를 관리하는 스크립트

/*대충 기본 아이디어
    map오브잭트에 지도의 큰 틀을 작성한다
    그 자식 오브젝트로 핀이나 세부 지도를 표현한다
 */
public class MapManage : MonoBehaviour
{

    private GameObject map;//지도 오브잭트를 저장할 변수

    public int maxMapSize = 28;//최대 확대 횟수
    private int mapSize = 0;//시작 확대 횟수 | 0 : 최소크기
    private float mapSizeX = 1920f;//지도 오브젝트의 가로 크기
    private float mapSizeY = 1080f;//지도 오브젝트의 세로 크기
    public float magnification = 1.1f;//확대 배율

    private List<Vector2> childLocate = new List<Vector2>();//지도 조각들의 위치
    private List<Vector2> childSize = new List<Vector2>();//지도 조각들의 크기


    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("map");//map 찾기

        if (map)//예외처리
        {//map의 자식 오브젝트 정보 저장

            for (int i = 0; i < map.transform.childCount; i++)
            {
                childLocate.Add(map.transform.GetChild(i).transform.position);
            }

            for (int i = 0; i < map.transform.childCount; i++)
            {
                childSize.Add(map.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta);
            }
        }

        return;
    }

    // Update is called once per frame
    void Update()
    {
        map_size_wheel();
    }

    //지도의 배율을 조정하는 함수
    void map_size() 
    {
        map.GetComponent<RectTransform>().sizeDelta
            = new Vector2(mapSizeX, mapSizeY) * Mathf.Pow(magnification, mapSize);

        map.transform.parent.GetComponent<RectTransform>().sizeDelta
            = new Vector2(mapSizeX, mapSizeY) * Mathf.Pow(magnification, mapSize);

        for (int i = 0; i < map.transform.childCount; i++)
        {
            map.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta
                = childSize[i] * Mathf.Pow(magnification, mapSize);
        }


        for (int i = 0; i < map.transform.childCount; i++)
        {
            map.transform.GetChild(i).transform.position
                = childLocate[i] * Mathf.Pow(magnification, mapSize) + (Vector2)map.transform.position;
        }
    }

    //휠입력이 들어오면 mapSize변수를 수정하는 함수
    void map_size_wheel()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput != 0)
        {
            if (wheelInput > 0 && mapSize < maxMapSize)
            {
                mapSize += 1;
            }

            else if (wheelInput < 0 && mapSize > 0)
            {
                mapSize -= 1;
            }

            map_size();
        }
    }

    //버튼을 누르면 mapSize변수를 수정하는 함수
    public void button(int dtSize)
    {

        mapSize += 5 * dtSize;
        if (mapSize < 0) mapSize = 0;
        else if (mapSize > maxMapSize) mapSize = maxMapSize;
        
        map_size();
    }
}