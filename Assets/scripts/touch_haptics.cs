using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
// using UnityEngine.InputLegacy;
using UnityEngine.UIElements;

public class touch_haptics : MonoBehaviour
{   
    private float width, height;
    public GameObject circlePrefab;
    private Vector2 touchPosition;
    private Vector3 worldPosition;
    public VisualElement toucharea;
    private UnityEngine.UI.Button btn;
    private List<GameObject> generatedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        btn=GameObject.FindGameObjectWithTag("btn").GetComponent<UnityEngine.UI.Button>();
        width=(float)Screen.width/2.0f;
        height=(float)Screen.height/2.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Invoke((continueBtn.clicked) => StartButtonPressed());
        
        if(Input.touchCount>1){
            int randomIndex = Random.Range(0, Input.touchCount-1);
            for (int j = 0; j < Input.touchCount; j++)
            {
                
                touchPosition = Input.GetTouch(j).position;
                worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10f));
                GameObject instantiatedCircle = Instantiate(circlePrefab, new Vector2(worldPosition.x, worldPosition.y), Quaternion.identity);

                generatedObjects.Add(instantiatedCircle);
            }

            for (int j = 0; j < generatedObjects.Count; j++)
            {
                if (j != randomIndex)
                {
                    Destroy(generatedObjects[j]);
                    generatedObjects.RemoveAt(j);
                    j--;
                }
            }
        }
        btn.onClick=OnButtonClick();
    }
    void OnButtonClick(){
        SceneManager.LoadScene("game");
    }
}