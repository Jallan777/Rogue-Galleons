using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{

    public Image targetImg;
    public Vector2 buttonPressOffset = new Vector2(0, -5);
    private Vector2 origPos;

    // Start is called before the first frame update
    void Start()
    {

        if(targetImg != null) {
            origPos = targetImg.rectTransform.anchoredPosition;
        }
    }

    public void OnButtonDown() {
        if(targetImg != null) {
            targetImg.rectTransform.anchoredPosition = origPos + buttonPressOffset;
        }
    }

    public void OnButtonUp() {
        if(targetImg != null) {
            targetImg.rectTransform.anchoredPosition = origPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
