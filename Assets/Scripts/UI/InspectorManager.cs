using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����â ���� ��ũ��Ʈ

/*���� �⺻ ���̵��
    ���� ���� ����Ʈ�� ����
    ����Ʈ �ε����� ���� ��ġ ����
    0�� ���� �������� ������,1�� ���� �¿�
    0���� �Ǹ� ȭ�� �߾����� �ε巴�� �̵�
    ������,1�� ���� 0 �� ���� ����
    �ε巴�� �� �����δ�
 */
public class InspectorManager : MonoBehaviour
{
    private List<GameObject> info = new List<GameObject>();//����â�� �ǵ��� ������ ����Ʈ

    // Start is called before the first frame update
    void Start()
    {
        info.Add(GameObject.Find("Stat"));//info ����Ʈ�� �� ����
        info.Add(GameObject.Find("Item"));
        info.Add(GameObject.Find("Constellation"));
    }

    // Update is called once per frame
    void Update()
    {
        set_pos();
    }

    public void move_info(int dir) //�ǵ��� ����Ʈ �� ��ġ�� �ٲ��ִ� �Լ�
    {

        if (dir > 0)//>����
        {//���� ���� ���� 0���� �����ϰ� ���� ���� ���� ����
            info.Insert(0, info[info.Count - 1]);
            info.RemoveAt(info.Count - 1);
        }
        else//<����
        {//ù��° ���� ����Ʈ�� ���� �����ϰ� ù��° ���� ����
            info.Add(info[0]);
            info.RemoveAt(0);
        }
    }

    private void set_pos()//�ǵ��� ����Ʈ�� ��ġ�� ���� ���� ��ġ�� �ٲپ��ִ� �Լ�
    {
        if (Mathf.Abs(info[0].GetComponent<RectTransform>().offsetMin.x) > 1)
        //0�� ���� ȭ�� �߽����� �����̰��Ѵ�.
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

        //1�� �ǰ� ������ ���� ���� 0�� ���� �¿�� �̵���Ų��
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



        for (int i = 2; i < info.Count - 1; i++)//������ ���� ���� ��Ƴ��� �ڵ�
        {
            info[i].GetComponent<RectTransform>().offsetMin
                = new Vector2(1920, 0);
            info[i].GetComponent<RectTransform>().offsetMax
                = new Vector2(1920, 0);
        }
    }
}
