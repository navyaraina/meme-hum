using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
// using UnityEngine.InputLegacy;
using UnityEngine.UIElements;

public class InputManager: MonoBehaviour
{

    public GameObject linePoint;
    public RectTransform canvas;
    public GameObject circlePrefab;
    private Vector2 touchPosition;
    private Vector3 worldPosition;
    private List<GameObject> generatedObjects = new List<GameObject>();

    private void Update(){
        if(Input.touchCount>0){
            for (int j = 0; j < Input.touchCount; j++)
            {
                
                touchPosition = Input.GetTouch(j).position;
                worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10f));
                GameObject instantiatedCircle = Instantiate(circlePrefab, new Vector2(worldPosition.x, worldPosition.y), Quaternion.identity);
                generatedObjects.Add(instantiatedCircle);
            }
        }
        // if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
        //        SceneManager.LoadScene("showdrawing");
        //     }
    }
}