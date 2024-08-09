using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCam : MonoBehaviour
{
    AudioListener audioListener;

    private void Awake()
    {
        audioListener = GetComponent<AudioListener>();

        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        if (audioListeners.Length > 1)
        {
            Destroy(audioListener);
        }
    }
}
