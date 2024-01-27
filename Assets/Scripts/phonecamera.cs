using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CameraController : MonoBehaviour
{
    private WebCamTexture frontCameraTexture;
    public RawImage displayImage;
    public Button button;

    void Start()
    {
        InitializeWebCam();
        //InitializeUI();
    }

    void InitializeWebCam()
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
}

