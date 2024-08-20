using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ImpulseSource : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource source;

    private void Start()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeEffect()
    {
        Debug.Log(this.gameObject.name + "�� ȣ��");
        ImpulseManger.Instance.CameraShake(source);
    }


}
