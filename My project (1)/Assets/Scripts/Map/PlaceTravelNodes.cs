using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceTravelNodes : MonoBehaviour
{

    public Sprite nodeSprite;
    //public GameObject[] nodes;
    public GameObject canvas;
    public GameObject nodePrefab;
    public float mapAreaMargin = 250f;
    public float nodeMinDistance = 50f;
    public GameObject mapObject;
    public Color defaultColour = Color.white;
    public Color selectedColour = Color.blue;
    public float fadeDuration = 0.5f;
    public float scaleFactor = 1.1f;

    private int numIslands = 12;
    private int nodesToPlace = 0;
    private int placedNodes = 0;
    private int nodeCounter = 0;
    private float delayTime = 0.2f;
    private int oceanNodes = 4;

    private GameObject lastPlacedNode;
    private GameObject firstPlacedNode;
    private GameObject selectedNode;
    private Vector3 originalSize;
    private static GameObject currentlySelectedNode;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedCheck());

        SailButtonChanger(false);

    }

    IEnumerator DelayedCheck()
    {
        yield return new WaitForSeconds(delayTime);

        int availableIslands = 0;
        Vector2 mapSize = GetMapSize();

        for (int i = 0; i < numIslands; i++)
        {
            if (PlayerPrefs.HasKey("CenterIslandX_" + i) && PlayerPrefs.HasKey("CenterIslandY_" + i))
            {
                availableIslands++;
            }

            if (PlayerPrefs.HasKey("EdgeIslandX_" + i) && PlayerPrefs.HasKey("EdgeIslandY_" + i))
            {
                availableIslands++;
            }
        }

        if (availableIslands == 0)
        {
            Debug.LogWarning("No island positions found in PlayerPrefs");
            yield break;
        }

        

        if (PlayerPrefs.HasKey("NodeX_0"))
        {
            Debug.Log("Previous Node Positions Found!");

            int savedNodeCount = 0;
            int savedOceanNodeCount = 0;

            while (PlayerPrefs.HasKey("NodeX_" + savedNodeCount))
            {
                savedNodeCount++;
            }


            Debug.Log(savedNodeCount + " saved Nodes found!");
            //Debug.Log(savedOceanNodeCount + " saved Ocean Nodes found!");

            nodesToPlace = savedNodeCount + savedOceanNodeCount;
            LoadNodePositions();
            //LoadOceanNodePositions();


        }
        else
        {
            Debug.LogWarning("No previous node positions found");
            Debug.Log("Generating Travel Nodes!");

            nodesToPlace = Random.Range(5, availableIslands + 1);

            for (int i = 0; i < 2; i++)
            {
                if (PlayerPrefs.HasKey("EdgeIslandX_" + i) && PlayerPrefs.HasKey("EdgeIslandY_" + i))
                {
                    float islandX = PlayerPrefs.GetFloat("EdgeIslandX_" + i);
                    float islandY = PlayerPrefs.GetFloat("EdgeIslandY_" + i);
                    Vector2 islandPos = new Vector2(islandX, islandY);

                    PlaceNode(islandPos,(i + 4));
                    nodeCounter++;
                    Debug.Log("Placed NEW node at position " + islandPos);

                }
                else
                {
                    Debug.LogWarning("Edge Island position not found for index: " + i);
                }
            }

            //nodesToPlace -= 2;

            for (int i = 2; i < nodesToPlace; i++)
            {
                if (PlayerPrefs.HasKey("CenterIslandX_" + i) && PlayerPrefs.HasKey("CenterIslandY_" + i))
                {
                    float islandX = PlayerPrefs.GetFloat("CenterIslandX_" + i);
                    float islandY = PlayerPrefs.GetFloat("CenterIslandY_" + i);
                    Vector2 islandPos = new Vector2(islandX, islandY);

                    PlaceNode(islandPos, (i + 4));
                    nodeCounter++;
                    Debug.Log("Placed NEW node at position " + islandPos);

                }
                else
                {
                    Debug.LogWarning("Center Island position not found for index: " + i);
                }


            }
            PlaceOceanNodes(mapSize);
        }
        Debug.Log(nodeCounter + " total Nodes Saved!");
        Debug.Log("Total Nodes Placed:  " + placedNodes);

        //SelectAndChangeColour();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaceNode(Vector2 islandPosition, int index)
    {
        GameObject newNode = Instantiate(nodePrefab, canvas.transform);
        Image img = newNode.GetComponent<Image>();
        RectTransform rt = newNode.GetComponent<RectTransform>();

        if (img != null)
        {
            img.sprite = nodeSprite;
        }
        else
        {
            Debug.LogError("Image not found on prefab!");
            return;
        }


        if (rt != null)
        {
            rt.anchoredPosition = islandPosition;
            PlayerPrefs.SetFloat("NodeX_" + index, rt.anchoredPosition.x);
            PlayerPrefs.SetFloat("NodeY_" + index, rt.anchoredPosition.y);
            
            if(index == 5 || IsSavedNode(rt.anchoredPosition))
            {
                islandPosition.y += 25f;
                rt.anchoredPosition = islandPosition;

                if(islandPosition.y > (islandPosition.y + 20f))
                {
                    islandPosition.y -= 25f;
                }

               HighlightNode(newNode);
                
            }
        }
        else
        {
            Debug.LogError("RectTransform not found on image!");
            return;
        }
        if(index == 5)
        {
            firstPlacedNode = newNode;

        }
        lastPlacedNode = newNode;

        placedNodes++;
        PlayerPrefs.Save();

        Button nodeButton = newNode.GetComponent<Button>();

        if(nodeButton != null)
        {
            nodeButton.onClick.AddListener(() => OnNodeClicked(newNode));
        }

    }

    void OnNodeClicked(GameObject node)
    {
        if (currentlySelectedNode != null && currentlySelectedNode != node)
        {
            ResetNode(currentlySelectedNode);
            currentlySelectedNode = null;

        }

        if(currentlySelectedNode != node)
        {
            HighlightNode(node);
            currentlySelectedNode = node;

            SaveSelectedNode(node.GetComponent<RectTransform>().anchoredPosition);
            SailButtonChanger(true);

            
        }
        else
        {
            ResetNode(node);
            currentlySelectedNode = null;

            SailButtonChanger(false);
        }
    }

    void HighlightNode(GameObject node)
    {
        Image img = node.GetComponent<Image>();
        RectTransform rt = node.GetComponent<RectTransform>();

        if(img != null)
        {
            img.color = selectedColour;
        }

        if(rt != null)
        {
            rt.localScale = rt.localScale * scaleFactor;
        }
    }

    void ResetNode(GameObject node)
    {
        Image img = node.GetComponent<Image>();
        RectTransform rt = node.GetComponent<RectTransform>();

        if(img != null)
        {
            img.color = defaultColour;
        }

        if(rt != null)
        {
            rt.localScale = rt.localScale / scaleFactor;
        }
    }

    bool IsSavedNode(Vector2 position)
    {
        float savedX = PlayerPrefs.GetFloat("SavedNodeX");
        float savedY = PlayerPrefs.GetFloat("SavedNodeY");
        Vector2 savedPos = new Vector2(savedX, savedY);

        return position == savedPos;
    }

    void SaveSelectedNode(Vector2 position)
    {
        PlayerPrefs.SetFloat("SavedNodeX", position.x);
        PlayerPrefs.SetFloat("SavedNodeY", position.y);
        PlayerPrefs.Save();
    }

    void PlaceOceanNodes(Vector2 mapSize)
    {
        int oceanNodeCounter = 0;
        List<Vector2> placedNodePositions = new List<Vector2>();

        for (int i = 0; i < oceanNodes; i++)
        {
            Vector2 oceanPos = new Vector2(0, 0);

            bool posFound = false;
            while (!posFound)
            {
                float usableWidth = mapSize.x - (mapAreaMargin * 2);
                float usableHeight = mapSize.y - (mapAreaMargin * 2);

                float randX = Random.Range(-usableWidth / 2, usableWidth / 2);
                float randY = Random.Range(-usableHeight / 2, usableHeight / 2);

                oceanPos = new Vector2(randX, randY);
                posFound = true;

                foreach (Vector2 placedNodePos in placedNodePositions)
                {
                    if (Vector2.Distance(oceanPos, placedNodePos) < nodeMinDistance)
                    {
                        Debug.LogWarning("Node too close to another Node! Trying placement again...");
                        posFound = false;
                        break;
                    }
                }
            }

            PlaceNode(oceanPos, i);
            oceanNodeCounter++;

            Debug.Log("Placed NEW Ocean Node at position: " + oceanPos);

            placedNodePositions.Add(oceanPos);

            //PlayerPrefs.SetFloat("NodeX_" + i, oceanPos.x);
            //PlayerPrefs.SetFloat("NodeY_" + i, oceanPos.y);

        }
        Debug.Log(oceanNodeCounter + " total Ocean Nodes Saved!");
        PlayerPrefs.Save();
    }

    Vector2 GetMapSize()
    {
        RectTransform rt = mapObject.GetComponent<RectTransform>();
        return rt != null ? rt.sizeDelta : Vector2.zero;
    }

    void LoadNodePositions()
    {
        int index = 0;
        
        
        while (PlayerPrefs.HasKey("NodeX_" + index) && index < 4)
        {
            float nodeX = PlayerPrefs.GetFloat("NodeX_" + index);
            float nodeY = PlayerPrefs.GetFloat("NodeY_" + index);
            Vector2 savedNodePos = new Vector2(nodeX, nodeY);

            PlaceNode(savedNodePos, index);

            Debug.Log("Saved Ocean node placed at location: " + savedNodePos);

            index++;
        }
        
        //index = 4;
        Debug.Log("AFTER OCEAN NODES INDEX: " + index);

        int indexStart = index;
        while (PlayerPrefs.HasKey("NodeX_" + indexStart))
        {
            float nodeX = PlayerPrefs.GetFloat("NodeX_" + indexStart);
            float nodeY = PlayerPrefs.GetFloat("NodeY_" + indexStart);
            Vector2 savedNodePos = new Vector2(nodeX, nodeY);

            PlaceNode(savedNodePos, indexStart);

            Debug.Log("Saved node placed at location: " + savedNodePos);

            indexStart++;

        }
        Debug.Log("FINAL INDEX: " + indexStart);

    }

    void SailButtonChanger(bool isClickable)
    {
        GameObject sailButton = GameObject.Find("MapPopup/MapButtons/SailButton");

        if(sailButton != null)
        {
            Button sButton = sailButton.GetComponent<Button>();
            Image buttonImage = sailButton.GetComponent<Image>();

            if(sButton != null && buttonImage != null)
            {
                if(isClickable)
                {
                    sButton.interactable = true;
                    buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1f);

                }
                else
                {
                    sButton.interactable = false;
                    buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.5f);

                }
            } 
            else
            {
                Debug.LogError("SailButton has no image or button!");
            }
        }
        else
        {
            Debug.LogError("SailButton not found!");
        }
    }
    void SelectAndChangeColour()
    {
        if(firstPlacedNode != null)
        {
            Image img = firstPlacedNode.GetComponent<Image>();
            RectTransform rt = firstPlacedNode.GetComponent<RectTransform>();

            if(img != null)
            {
                img.color = Color.blue;
            }

            if(rt != null)
            {
                Vector3 newSize = rt.localScale * 1.2f;
                rt.localScale = newSize;
            }
        }
    }
}
