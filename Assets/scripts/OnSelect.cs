using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnSelect : MonoBehaviour
{

    public Button captureButton;

    // Start is called before the first frame update
    void Start()
    {
        Button button = captureButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }

    }

    public void OnClick(){
        SceneManager.LoadScene("Drawing_prompt");
    }
}
