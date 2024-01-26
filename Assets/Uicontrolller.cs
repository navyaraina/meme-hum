using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class Uicontroller : MonoBehaviour
{
    public Button startButton;
    public Label messageText;
    

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("startbutton");
        messageText = root.Q<Label>("messagetext"); 
        startButton.clicked += StartButtonPressed;
    }

     void StartButtonPressed()
     {
        
        SceneManager.LoadScene("game");
        
    }
    
}


