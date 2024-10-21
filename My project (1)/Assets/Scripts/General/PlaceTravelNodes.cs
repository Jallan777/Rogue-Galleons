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
    public float mapAreaMargin = 50f;
    public int numIslands = 12;
    public string islandType = "Center";

    public GameObject mapObject;
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

        for(int i = 0; i < numIslands; i++)
        {
            if(PlayerPrefs.HasKey(islandType + "IslandX_" + i) && PlayerPrefs.HasKey(islandType + "IslandY_" + i))
            {
                availableIslands++;
            }
        }

        if(availableIslands == 0)
        {
            Debug.LogWarning("No island positions found in PlayerPrefs");
            yield break;
        }
        
        int nodesToPlace = Random.Range(5, availableIslands + 1);

        for(int i = 0; i < nodesToPlace; i++)
        {
            if(PlayerPrefs.HasKey(islandType + "IslandX_" + i) && PlayerPrefs.HasKey(islandType + "IslandY_" + i))
            {
                float islandX = PlayerPrefs.GetFloat(islandType + "IslandX_" + i);
                float islandY = PlayerPrefs.GetFloat(islandType + "IslandY_" + i);
                Vector2 islandPos = new Vector2(islandX, islandY);

                placeNode(islandPos);
            }
            else
            {
                Debug.LogWarning(islandType + "Island position not found for index: " + i);
            }
        }
        PlaceOceanNodes(mapSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void placeNode(Vector2 islandPosition)
    {
        GameObject newNode = Instantiate(nodePrefab, canvas.transform);

        Image img = newNode.GetComponent<Image>();


        if(img != null)
        {
            img.sprite = nodeSprite;
            Debug.Log("Placed node at position: " + islandPosition);
        }
        else
        {
            Debug.LogError("Image not found on prefab!");
            return;
        }

        RectTransform rt = newNode.GetComponent<RectTransform>();

        if(rt != null)
        {
            rt.anchoredPosition = islandPosition;
            Debug.Log("Placed node at position " + islandPosition);
        }
        else
        {
            Debug.LogError("RectTransform not found on image!");
            return;
        }

    }

    void PlaceOceanNodes(Vector2 mapSize)
    {
        for(int i = 0; i < oceanNodes; i++)
        {
            float usableWidth = mapSize.x - (mapAreaMargin * 2);
            float usableHeight = mapSize.y - (mapAreaMargin * 2);

            float randX = Random.Range(-usableWidth / 2, usableWidth / 2);
            float randY = Random.Range(-usableHeight / 2, usableHeight / 2);

            Vector2 oceanPos = new Vector2(randX, randY);
            
            Debug.Log("This is an Ocean Node!");
            
            placeNode(oceanPos);
        }
    }
    Vector2 GetMapSize()
    {
        RectTransform rt = mapObject.GetComponent<RectTransform>();
        return rt != null ? rt.sizeDelta : Vector2.zero;
    }


}
