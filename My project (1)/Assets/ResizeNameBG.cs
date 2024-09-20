using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResizeNameBG : MonoBehaviour
{

    public Text textComponent;
    public RectTransform nameBGTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float preferredWidth = textComponent.preferredWidth;
        float preferredHeight = textComponent.preferredHeight;

        float padding = 30f;

        nameBGTrans.sizeDelta = new Vector2(preferredWidth + padding, preferredHeight + padding);
    }
}
