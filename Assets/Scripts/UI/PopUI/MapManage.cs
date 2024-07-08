using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//���� UI�� �����ϴ� ��ũ��Ʈ

/*���� �⺻ ���̵��
    map������Ʈ�� ������ ū Ʋ�� �ۼ��Ѵ�
    �� �ڽ� ������Ʈ�� ���̳� ���� ������ ǥ���Ѵ�
 */
public class MapManage : MonoBehaviour
{

    private GameObject map;//���� ������Ʈ�� ������ ����

    public int maxMapSize = 28;//�ִ� Ȯ�� Ƚ��
    private int mapSize = 0;//���� Ȯ�� Ƚ�� | 0 : �ּ�ũ��
    private float mapSizeX = 1920f;//���� ������Ʈ�� ���� ũ��
    private float mapSizeY = 1080f;//���� ������Ʈ�� ���� ũ��
    public float magnification = 1.1f;//Ȯ�� ����

    private List<Vector2> childLocate = new List<Vector2>();//���� �������� ��ġ
    private List<Vector2> childSize = new List<Vector2>();//���� �������� ũ��


    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("map");//map ã��

        if (map)//����ó��
        {//map�� �ڽ� ������Ʈ ���� ����

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

    //������ ������ �����ϴ� �Լ�
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

    //���Է��� ������ mapSize������ �����ϴ� �Լ�
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

    //��ư�� ������ mapSize������ �����ϴ� �Լ�
    public void button(int dtSize)
    {

        mapSize += 5 * dtSize;
        if (mapSize < 0) mapSize = 0;
        else if (mapSize > maxMapSize) mapSize = maxMapSize;
        
        map_size();
    }
}