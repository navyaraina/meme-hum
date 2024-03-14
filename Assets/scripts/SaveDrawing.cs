using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour
{
    public Button captureButton;
    public Image imageToUpdate; // Reference to the Image component you want to update

    private void Start()
    {
        Run();
    }

    private void Run()
    {
        // Attach the button click event listener
        Button button = captureButton.GetComponent<Button>();
        if (button != null)
        {
            Debug.Log("Button found. Setting up click event.");
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button not found on ButtonClickHandler GameObject.");
        }
    }

    private void OnButtonClick()
    {
        StartCoroutine(SaveScreenshot());
    }

    IEnumerator SaveScreenshot()
    {
        // Wait for the end of the frame to capture the screenshot
        yield return new WaitForEndOfFrame();

        Debug.Log("Capturing screenshot...");

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
        string fileName = "DrawnScreen.png";
        string filePath = Path.Combine(directoryPath, fileName);

        // Check if the file already exists
        if (File.Exists(filePath))
        {
            Debug.LogWarning("File already exists. Generating a new file name...");
            // You can generate a new file name or take appropriate action here

            string newPath = "Assets/" + "DrawnScreen.png";
            File.Delete(filePath);

            File.Copy(newPath, filePath);
        }

        // Ensure the directory exists before attempting to save
        if (!Directory.Exists(directoryPath))
        {
            Debug.Log("Creating directory...");
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Photo saved at: " + filePath);

        SceneManager.LoadScene("showdrawing");

        // Load the saved image into the Image component
        // ImageToUI(filePath);
    }
}