using UnityEngine;

namespace Screenshotr
{
    public class Main : IMod
    {
        private GameObject _go;

        public void onEnabled()
        {
            _go = new GameObject("Screenshotr");

            _go.AddComponent<Screenshotr>();
            _go.GetComponent<Screenshotr>().Path = Path;
        }

        public void onDisabled()
        {
            Object.Destroy(_go);
        }

        public string Name { get { return "Screenshotr"; } }
        public string Description { get { return "Upload screenshots to ParkitectNexus"; } }
        public string Identifier { get; set; }
        public string Path { get; set; }
    }
}
