using UnityEngine;

namespace Screenshotr
{
    class ScreenshotrOptions : MonoBehaviour
    {
        private Rect _windowRect = new Rect(100, 200, 200, 50);
        
        void OnGUI()
        {
            _windowRect = GUILayout.Window(1, _windowRect, DoMyWindow, "Options");
        }

        void DoMyWindow(int windowID)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Api key");
            Screenshotr.Instance.ApiKey = GUILayout.TextField(Screenshotr.Instance.ApiKey, 32);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
            {
                Destroy(GetComponent<ScreenshotrOptions>());
            }
            GUILayout.EndHorizontal();
        }
    }
}
