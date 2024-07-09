using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

namespace UnityEngine
{
    public class weda : MonoBehaviour
    {
        Process note;

        private void Start()
        { 
            note = Process.Start("notepad.exe");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(note.Id);
            }
        }
    }
}