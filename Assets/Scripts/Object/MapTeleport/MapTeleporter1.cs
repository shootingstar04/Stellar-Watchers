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
            StartCoroutine(EffectStart(collision));
        }
    }

    public void Teleported(Collider2D player)
    {
        Debug.Log(this.gameObject.name + " 에서 텔포받는다. Mapnum은 "+MapNum);
        player.GetComponent<PlayerMapLocation>().ChangeMapNum(MapNum);
        StartCoroutine(Effect(player));
        confinderChange.instance.ConfinderChange(MapNum);
        
    }

    IEnumerator Effect(Collider2D player)
    {
        yield return new WaitForSeconds(0.5f);

        SceneTransition.instance.FadeIn();
        player.GetComponent<PlayerMove>().RestartMove();
    }

    IEnumerator EffectStart(Collider2D collision)
    {
        collision.GetComponent<PlayerMove>().PauseMove();
        SceneTransition.instance.FadeOut();

        yield return new WaitForSeconds(1f);

        collision.transform.position = new Vector3(TargetTp.transform.position.x + offset, TargetTp.transform.position.y, collision.transform.position.z);
        TargetTp.GetComponent<MapTeleporter1>().Teleported(collision);
    }
}
