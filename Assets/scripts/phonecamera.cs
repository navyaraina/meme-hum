using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;

public class CameraController : MonoBehaviour
{
    private WebCamTexture frontCameraTexture;
    public RawImage displayImage;
    public Button button;

    void Start()
    {
        // Check if the device has a front camera
        if (WebCamTexture.devices.Length > 0)
        {
            // Find the front camera device
            foreach (WebCamDevice device in WebCamTexture.devices)
            {
                if (device.isFrontFacing)
                {
                    // Create a new WebCamTexture using the front camera
                    frontCameraTexture = new WebCamTexture(device.name, Screen.width, Screen.height);
                    break;
                }
            }

            // Check if the front camera texture is not null
            if (frontCameraTexture != null)
            {
                // Set the texture to the RawImage component
                displayImage.texture = frontCameraTexture;

                // Start the camera feed
                frontCameraTexture.Play();

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(OnButtonClick);
            }
            else
            {
                Debug.LogError("Front camera not found.");
            }
        }
        else
        {
            Debug.LogError("No cameras found on the device.");
        }
    }

    void Update()
    {
        // button.clicked=SavePhoto();
    }

    void OnButtonClick()
{
    StartCoroutine(SavePhotoCoroutine());
}

IEnumerator SavePhotoCoroutine()
{
    yield return new WaitForEndOfFrame();

    int width = Screen.width;
    int height = Screen.height;
    Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

    tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    tex.Apply();

    // Encode texture into PNG
    byte[] bytes = ImageConversion.EncodeToPNG(tex);
    Object.Destroy(tex);

    File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
}
}
