using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManage : MonoBehaviour
{
    public float sizeSensitivity;

    private GameObject map;
    private GameObject mapScreen;

    private float mapSize = 1f;
    private bool isShowing = false;

    private List<Vector2> childSize = new List<Vector2>();
    private List<Vector2> childLocate = new List<Vector2>();


    // Start is called before the first frame update
    void Start()
    {
        GameObject a = GameObject.Find("map");
        GameObject b = GameObject.Find("mapscreen");

        if (a) map = a;
        if (b) mapScreen = b;

        for (int i = 0; i < map.transform.childCount; i++)
        {
            childSize.Add(map.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta);
        }

        for (int i = 0; i < map.transform.childCount; i++)
        {
            childLocate.Add(map.transform.GetChild(i).transform.position);
        }

        b.SetActive(false);

        return;
    }

    // Update is called once per frame
    void Update()
    {
        show_map();
        map_size();
    }

    void show_map()
    {

        if (Input.GetKeyDown(KeyCode.M) && mapScreen)
        {
                isShowing = !isShowing;
                mapScreen.SetActive(isShowing);
        }

    }

    void map_size()
    {
        if (isShowing && map.GetComponent<Image>().enabled == true)
        {

            float wheelInput = Input.GetAxis("Mouse ScrollWheel");

            if (wheelInput > 0 && mapSize < 5)
            {
                Debug.Log("1");
                mapSize += sizeSensitivity;
            }

            else if (wheelInput < 0 && mapSize > 1)
            {
                mapSize -= sizeSensitivity;
            }

            map.GetComponent<RectTransform>().sizeDelta
                = new Vector2(1920 * mapSize, 1080 * mapSize);

            map.transform.parent.GetComponent<RectTransform>().sizeDelta
                = new Vector2(1920 * mapSize, 1080 * mapSize);

            for (int i = 0; i < map.transform.childCount; i++)
            {
                map.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta
                   = childSize[i] * mapSize;
            }

            for (int i = 0; i < map.transform.childCount; i++)
            {
                map.transform.GetChild(i).transform.position
                   = childLocate[i] * mapSize + (Vector2) map.transform.position;
            }
        }
    }
}