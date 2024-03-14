using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ImageUpdater : MonoBehaviour
{
    public Image imageToUpdate; // Reference to the Image component or SpriteRenderer

    void Start()
    {
        // Call the method to update the image (you can call this whenever you need to update)
        UpdateImage();
    }

    void UpdateImage()
    {
        // Path to your PNG file (change it according to your project structure)
        string filePath = "C:/programs/unity/GGJ24/Assets/DrawnScreen.png";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the bytes from the PNG file
            byte[] fileData = File.ReadAllBytes(filePath);

            // Create a new Texture2D and load the PNG file data
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            // Apply the texture to the Image component or SpriteRenderer
            if (imageToUpdate != null)
            {
                imageToUpdate.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
            else
            {
                Debug.LogError("Image component not assigned to imageToUpdate.");
            }
        }
        else
        {
            Debug.LogError("PNG file not found at path: " + filePath);
        }
    }

    void Update(){
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
               SceneManager.LoadScene("Polling");
            }
    }
}