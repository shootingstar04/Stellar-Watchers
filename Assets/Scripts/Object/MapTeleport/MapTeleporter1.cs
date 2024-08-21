using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleporter1 : MonoBehaviour
{
    public GameObject TargetTp;
    public float offset;
    [SerializeField] private int MapNum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            SceneTransition.instance.FadeOut();
            collision.transform.position = new Vector3(TargetTp.transform.position.x + offset, TargetTp.transform.position.y, collision.transform.position.z);
            TargetTp.GetComponent<MapTeleporter1>().Teleported();
        }
    }

    public void Teleported()
    {
        Effect();
        confinderChange.instance.ConfinderChange(MapNum);
        
    }

    IEnumerator Effect()
    {
        yield return new WaitForSeconds(0.5f);

        SceneTransition.instance.FadeIn();
    }
}
