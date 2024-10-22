using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NodeController : MonoBehaviour
{
    private bool isSelected = false;
    public Color defaultColour = Color.white;
    public Color selectedColour = Color.blue;
    public float fadeDuration = 0.5f;
    public float scaleFactor = 1.1f;

    //private Color originalColour;
    private Button button;
    private Image image;
    private Vector3 originalSize;
    private float colourProgress = 0f;
    private Color endColour;
    private RectTransform rt;
    private static NodeController currentlySelectedNode;

    public NodeManager nodeManager;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        //originalColour = image.color;
        originalSize = transform.localScale;

        if (button != null)
        {
            button.onClick.AddListener(OnNodeClicked);
        }

        SetSelected(false);

        if (nodeManager != null)
        {
            nodeManager.RegisterNode(this);
        }

        Vector2 currentNodePos = rt.anchoredPosition;

        Debug.Log("Found current node position: " + currentNodePos);

        float nodeX = PlayerPrefs.GetFloat("NodeX_5");
        float nodeY = PlayerPrefs.GetFloat("NodeY_5");
        Vector2 firstNodePos = new Vector2(nodeX, nodeY);

        Debug.Log("Found first node position: " + firstNodePos);

        if(currentNodePos == firstNodePos)
        {
            SetSelected(true);
        }

        Debug.Log("Node Position: " + rt);
    }

    // Update is called once per frame
    void Update()
    {
        if (colourProgress < 1f)
        {
            colourProgress += Time.deltaTime / fadeDuration;
            image.color = Color.Lerp(image.color, endColour, colourProgress);
        }
    }

    public void SetSelected(bool selected, bool instant = false)
    {
        isSelected = selected;
        endColour = isSelected ? selectedColour : defaultColour;



        /*
        */

        if (instant)
        {
            image.color = endColour;
            colourProgress = 1f;

            if (rt != null)
            {
                rt.localScale = originalSize * scaleFactor;
            }
        }
        else
        {
            colourProgress = 0f;

            if (rt != null)
            {
                if (isSelected)
                {
                    StartCoroutine(ScaleNode(rt, originalSize * scaleFactor));
                }
                else
                {
                    StartCoroutine(ScaleNode(rt, originalSize));
                }
            }
        }
    }

    private IEnumerator ScaleNode(RectTransform rt, Vector3 targetScale)
    {
        Vector3 startScale = rt.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            rt.localScale = Vector3.Lerp(startScale, targetScale, (elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        rt.localScale = targetScale;
    }

    void OnNodeClicked()
    {
        if (currentlySelectedNode != null && currentlySelectedNode != this)
        {
            currentlySelectedNode.SetSelected(false);
        }

        //isSelected = !isSelected;
        SetSelected(!isSelected);
        currentlySelectedNode = isSelected ? this : null;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
