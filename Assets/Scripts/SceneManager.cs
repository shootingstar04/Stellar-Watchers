using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextMap()
    {
        StartCoroutine(LoadMap());
    }

    IEnumerator LoadMap()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("nothuman_mapTPtest");
        transitionAnim.SetTrigger("Start");
    }

}
