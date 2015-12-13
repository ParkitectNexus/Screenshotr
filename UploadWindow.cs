using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Screenshotr
{
    class UploadWindow : MonoBehaviour
    {
        private readonly string _screenShotUrl = "https://parkitectnexus.com/api/screenshot/create";

        private Rect _windowRect = new Rect(100, 100, 200, 50);

        private GameObject _canvas;

        public byte[] Screenie;

        public Texture2D ScreenieTexture;

        private string title;

        private bool _show = true;

        void Start()
        {
            _canvas = new GameObject("Canvas");

            RawImage ri = _canvas.AddComponent<RawImage>();
            Canvas ca = _canvas.AddComponent<Canvas>();

            ri.texture = ScreenieTexture;
            ca.renderMode = RenderMode.ScreenSpaceCamera;

            title = "Screenshot on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        void OnGUI()
        {
            if(_show)
                _windowRect = GUILayout.Window(0, _windowRect, DoMyWindow, "Upload");
        }

        void DoMyWindow(int windowID)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Title");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            title = GUILayout.TextField(title, 100);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Upload to ParkitectNexus"))
            {
                StartCoroutine(UploadScreenshot());
                _show = false;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Cancel"))
            {
                Destroy(this);
            }
            if (GUILayout.Button("Options"))
            {
                if (Screenshotr.Instance.GetComponent<ScreenshotrOptions>() == null)
                    Screenshotr.Instance.gameObject.AddComponent<ScreenshotrOptions>();
            }
            GUILayout.EndHorizontal();
        }

        void OnDestroy()
        {
            Destroy(_canvas);
        }
        
        IEnumerator UploadScreenshot()
        {
            // Create a Web Form
            WWWForm form = new WWWForm();

            form.AddBinaryData("screenshot", Screenie, "screenShot.png", "image/png");
            form.AddField("title", title);

            Dictionary<string, string> headers = form.headers;

            headers["Authorization"] = Screenshotr.Instance.ApiKey;

            // Upload to a cgi script
            WWW w = new WWW(_screenShotUrl, form.data, headers);

            yield return w;
            if (!string.IsNullOrEmpty(w.error))
            {
                print(w.text);

                ErrorWindow ew = gameObject.AddComponent<ErrorWindow>();

                ew.ErrorMessage = w.error;
                ew.ErrorText = w.text;
            }

            Screenie = null;
            Destroy(this);
        }
    }
}
