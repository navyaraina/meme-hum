using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class TIMER : MonoBehaviour
{
    public TMP_Text startText;
    public float timeLeft = 3.0f;


    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
        }
        else
        {
            SceneManager.LoadScene("displayimage");
        }

    }
}