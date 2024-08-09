using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    public string PlayerType = "Melee";

    private Transform currentTransform;
    private PlayerMove playerMove;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerMove = this.GetComponent<PlayerMove>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        currentTransform = gameObject.transform;
        VirtualCameraManager.Instance.ChangeCameraTarget(currentTransform);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !playerMove.isAttacking)
        {
            Transform();
            PlayerAttackCombo.instance.ComboInvocation();
        }
    }

    void Transform()
    {
        Vector3 position = currentTransform.position;
        Quaternion rotation = currentTransform.rotation;
        StartCoroutine(color_change());

        if (PlayerType == "Melee")
        {
            PlayerType = "Ranged";
            gameObject.GetComponent<PlayerAttackMelee>().enabled = false;
            gameObject.GetComponent<PlayerAttackRanged>().enabled = true;
        }
        else
        {
            PlayerType = "Melee";
            gameObject.GetComponent<PlayerAttackMelee>().enabled = true;
            gameObject.GetComponent<PlayerAttackRanged>().enabled = false;
            
        }

        currentTransform = gameObject.transform;
    }

    IEnumerator color_change()
    {
        spriteRenderer.color = Color.yellow;
        Debug.Log(1);
        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;        
    }
}