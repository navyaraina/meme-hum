using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class polling: MonoBehaviour
{
    public Button funnyButton;
    public Button notfunnyButton;
    public Label MessageText;
    

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        funnyButton = root.Q<Button>("funnybutton");
        notfunnyButton = root.Q<Button>("notfunnybutton");
        MessageText = root.Q<Label>("messagetext"); 
        funnyButton.clicked += FunnyButtonPressed;
        notfunnyButton.clicked+= NotfunnyButtonPressed;
    }

     void FunnyButtonPressed()
     {
        
        SceneManager.LoadScene("game");
        
    }
    void NotfunnyButtonPressed()
     {
        
        SceneManager.LoadScene("scenename");
        
    }
    
}


