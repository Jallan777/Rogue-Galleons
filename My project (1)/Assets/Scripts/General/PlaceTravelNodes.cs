using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceTravelNodes : MonoBehaviour
{

    public Sprite nodeSprite;
    public GameObject[] nodes;
    public GameObject canvas;
    public GameObject nodePrefab;
    public float mapAreaMargin = 250f;
    public float nodeMinDistance = 50f;
    public int numIslands = 12;
    public string islandType = "Center";

    public GameObject mapObject;
    private int nodesToPlace = 0;
    private int placedNodes = 0;
    private int nodeCounter = 0;
    private float delayTime = 0.2f;
    private int oceanNodes = 4;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedCheck());
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
            PlaceOceanNodes(mapSize, (nodesToPlace+oceanNodes));
        }
        Debug.Log(nodeCounter + " total Nodes Saved!");
        Debug.Log("Total Nodes Placed:  " + placedNodes);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaceNode(Vector2 islandPosition, int index)
    {
        GameObject newNode = Instantiate(nodePrefab, canvas.transform);

        Image img = newNode.GetComponent<Image>();


        if (img != null)
        {
            img.sprite = nodeSprite;
        }
        else
        {
            Debug.LogError("Image not found on prefab!");
            return;
        }

        RectTransform rt = newNode.GetComponent<RectTransform>();

        if (rt != null)
        {
            rt.anchoredPosition = islandPosition;
            PlayerPrefs.SetFloat("NodeX_" + index, rt.anchoredPosition.x);
            PlayerPrefs.SetFloat("NodeY_" + index, rt.anchoredPosition.y);

        }
        else
        {
            Debug.LogError("RectTransform not found on image!");
            return;
        }

        placedNodes++;
        PlayerPrefs.Save();

    }

    void PlaceOceanNodes(Vector2 mapSize, int indexPos)
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


}
