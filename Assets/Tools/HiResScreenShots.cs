using UnityEngine; 
using System.Collections; 
using System.IO;

public class HiResScreenShots : MonoBehaviour { 
    public int resWidth = 1280; 
    public int resHeight = 720;
	private string screenshotPath;

	void Awake()
	{
		screenshotPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Screenshots" + Path.DirectorySeparatorChar;
	}

#if !UNITY_WEBPLAYER
   void Update () { 
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.RightAlt)) {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
			Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false); 

			foreach(Camera cam in Camera.allCameras)
			{
				cam.targetTexture = rt;
				cam.Render();
	            RenderTexture.active = rt; 
	            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0); 
				cam.targetTexture = null; 
	            RenderTexture.active = null; // JC: added to avoid errors 
	            Destroy(rt);
			}

            byte[] bytes = screenShot.EncodeToPNG();

			if(!Directory.Exists(screenshotPath))
				Directory.CreateDirectory(screenshotPath);

			string filename = screenshotPath + "screenshot" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png"; 
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
        }
   } 
#endif
} 