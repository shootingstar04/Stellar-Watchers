using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManage : MonoBehaviour
{

    private GameObject map;
    private GameObject mapScreen;

    public int maxMapSize = 28;
    private int mapSize = 0;
    private float mapSizeX = 1920f;
    private float mapSizeY = 1080f;
    public float magnification = 1.1f;
    private bool isShowing = false;

    private List<Vector2> childLocate = new List<Vector2>();
    private List<Vector2> childSize = new List<Vector2>();


    // Start is called before the first frame update
    void Start()
    {
        GameObject a = GameObject.Find("map");
        GameObject b = GameObject.Find("mapscreen");

        if (a) 
        { 
            map = a;

            for (int i = 0; i < map.transform.childCount; i++)
            {
                childLocate.Add(map.transform.GetChild(i).transform.position);
            }

            for (int i = 0; i < map.transform.childCount; i++)
            {
                childSize.Add(map.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta);
            }
        }
        if (b) {
            mapScreen = b;
            b.SetActive(false);
        }

        return;
    }

    // Update is called once per frame
    void Update()
    {
        show_map();
        map_size_wheel();
    }

    void show_map()
    {

        if (Input.GetKeyDown(KeyCode.M) && mapScreen)
        {
            isShowing = !isShowing;
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            mapScreen.SetActive(isShowing);
        }

    }
    void map_size() {
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

    void map_size_wheel()
    {
        if (isShowing)
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
    }

    public void map_size_button(int dtSize)
    {
        if (isShowing)
        {
            mapSize += 5 * dtSize;
            if (mapSize < 0) mapSize = 0;
            else if (mapSize > maxMapSize) mapSize = maxMapSize;
            map_size();
        }
    }
}