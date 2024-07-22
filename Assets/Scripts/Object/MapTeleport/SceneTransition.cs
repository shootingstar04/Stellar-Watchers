using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    [SerializeField] Animator transitionAnim;

    private GameObject Player;

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

    public void NextMap(Transform targetPos, sbyte offset)
    {
        StartCoroutine(LoadMap(targetPos, offset));
    }

    IEnumerator LoadMap(Transform targetPos, sbyte offset)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(2);
        Player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        Player.transform.position = new Vector3(targetPos.position.x + offset * 2, targetPos.position.y, Player.transform.position.z);
        transitionAnim.SetTrigger("Start");
    }

}