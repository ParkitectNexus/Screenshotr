using System.Collections;
using System.IO;
using UnityEngine;

namespace Screenshotr
{
    class Screenshotr : MonoBehaviour
    {
        private string _folder;

        public string Path;

        public static Screenshotr Instance;

        private string _apiKey;

        public string ApiKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                File.WriteAllText(Path + "/key.txt", value);
                _apiKey = value;
            }
        }

        void Awake()
        {
            Instance = this;
        }
        
        void Start()
        {
            _folder = FilePaths.getFolderPath("Screenshots/");

            if (!Directory.Exists(_folder))
                Directory.CreateDirectory(_folder);

            if (File.Exists(Path + "/key.txt"))
            {
                _apiKey = File.ReadAllText(Path + "/key.txt");
            }
            else
            {
                gameObject.AddComponent<ScreenshotrOptions>();
            }
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.F11) || (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKeyUp(KeyCode.F11)))
            {
                StartCoroutine(TakeScreenshot());
            }
        }
        
        IEnumerator TakeScreenshot()
        {
            yield return new WaitForEndOfFrame();
            
            int width = Screen.width;
            int height = Screen.height;
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            UploadWindow uploadWindow = gameObject.AddComponent<UploadWindow>();

            uploadWindow.Screenie = tex.EncodeToPNG();
            uploadWindow.ScreenieTexture = new Texture2D(500, 500);
            uploadWindow.ScreenieTexture.LoadImage(uploadWindow.Screenie);

            Destroy(tex);
        }
    }
}
