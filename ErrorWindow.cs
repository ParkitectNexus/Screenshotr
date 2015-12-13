using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Screenshotr
{
    class ErrorWindow : MonoBehaviour
    {
        public string ErrorMessage;
        public string ErrorText;

        string[] stringToEdit;

        public Rect windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50);

        void OnGUI()
        {
            windowRect = GUILayout.Window(1, windowRect, DoMyWindow, "My Window");
        }

        void DoMyWindow(int windowID)
        {
            GUILayout.Label(ErrorMessage);
            GUILayout.TextArea(ErrorMessage);

            if (GUILayout.Button("Ok"))
            {
                Destroy(GetComponent<ErrorWindow>());
            }
        }
    }
}
