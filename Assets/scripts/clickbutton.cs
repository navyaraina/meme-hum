using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    public RawImage displayImage;
    public Button captureButton;

    private void Start()
    {
        // Attach the button click event listener
        Button button = captureButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnCaptureButtonClick);
        }
        else
        {
            Debug.LogError("Button not found on ButtonClickHandler GameObject.");
        }
    }

    private void OnCaptureButtonClick()
    {
        StartCoroutine(CaptureAndSaveScreenshot());
    }

    IEnumerator CaptureAndSaveScreenshot()
    {
        // Wait for the end of the frame to capture the screenshot
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Capture the screenshot
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);

        // Save the screenshot
        string directoryPath = "C:/programs/unity/GGJ24/Assets";
        string fileName = "SavedScreen.png";
        string filePath = Path.Combine(directoryPath, fileName);

        // Ensure the directory exists before attempting to save
        if (File.Exists(filePath))
        {
            Debug.LogWarning("File already exists. Generating a new file name...");
            // You can generate a new file name or take appropriate action here

            string newPath = "Assets/" + "SavedScreen.png";
            File.Delete(filePath);

            File.Copy(newPath, filePath);
        }

        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Photo saved at: " + filePath);
        SceneManager.LoadScene("displayimage");
    }
    // void Update(){
    //     if(Input.touchCount==1){
    //         SceneManager.LoadScene("draw_or_make");
    //     }
    // }
}
