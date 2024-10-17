using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateMap : MonoBehaviour
{

    public GameObject[] centerIslands;
    public GameObject[] edgeIslands;

    public GameObject[] otherIslands;
    public Vector2 boundingAreaSize;
    public Vector2 boundingAreaCenter;
    public Image treasureIslandImage;
    public Sprite[] tiImages;
    public Color boxColour = Color.red;
    private List<Vector2> placedPositions = new List<Vector2>();


    public RectTransform image1;
    public RectTransform image2; 



    // Start is called before the first frame update
    void Start()
    {
        if (tiImages.Length != 3)
        {
            Debug.LogError("Assign 3 images please!");
            return;
        }

        AssignTIImg();

        foreach (GameObject island in centerIslands)
        {
            PlaceRandomly(island);
        }

        foreach (GameObject island in edgeIslands)
        {
            PlaceRandomX(island);
        }

        AddIslandPositions();
    }

    void AddIslandPositions()
    {
        
        
        foreach(GameObject island in otherIslands)
        {
            RectTransform rt = island.GetComponent<RectTransform>();
            if(rt != null)
            {
                placedPositions.Add(rt.anchoredPosition);
            }
            else
            {
                Debug.LogWarning("No RectTransform found for island: " + island.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get the anchored positions
        Vector2 pos1 = image1.anchoredPosition;
        Vector2 pos2 = image2.anchoredPosition;

        // Calculate distance
        float distance = Vector2.Distance(pos1, pos2);

        // Print distance to console
        Debug.Log("Distance between images: " + distance);
    }

    void PlaceRandomly(GameObject island)
    {
        RectTransform rectTransform = island.GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("No RectTransform found on island: " + island.name);
            return;
        }

        Vector2 islandSize = new Vector2(rectTransform.rect.width + 15, rectTransform.rect.height + 15);

        Vector2 randPosition;
        int counter = 0;
        int counterMax = 25;

        do
        {
            randPosition = GetRandPosition(islandSize);

            float angle = Random.Range(0f, 360f);
            float distance = counter * 2f;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

            randPosition += offset;

            counter++;

            if (counter > counterMax)
            {
                Debug.LogError("Max attempts reached for island: " + island.name);
                return;
            }
        }
        while (IsOverlapping(randPosition, islandSize, 15));

        rectTransform.anchoredPosition = randPosition;
        placedPositions.Add(randPosition);

        Debug.Log("Placed island: " + island.name + " at position: " + randPosition);
    }

    void PlaceRandomX(GameObject island)
    {
        RectTransform rectTransform = island.GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("No RectTransform found on island: " + island.name);
            return;
        }

        Vector2 islandSize = new Vector2(rectTransform.rect.width + 15, rectTransform.rect.height + 15);
        Vector2 randPosition;

        float setYPosition = rectTransform.anchoredPosition.y;

        do
        {
            float xMin = (boundingAreaCenter.x - boundingAreaSize.x / 2 + islandSize.x / 2) + 300;
            float xMax = boundingAreaCenter.x + boundingAreaSize.x / 2 - islandSize.x / 2;

            float randX = Random.Range(xMin, xMax);

            randPosition = new Vector2(randX, setYPosition);
        }
        while (IsOverlapping(randPosition, islandSize, 10));

        rectTransform.anchoredPosition = randPosition;
        placedPositions.Add(randPosition);

        Debug.Log("Placed Edge island: " + island.name + " at position: " + randPosition);

    }

    void AssignTIImg()
    {
        int randIndex = Random.Range(0, tiImages.Length);
        treasureIslandImage.sprite = tiImages[randIndex];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = boxColour;

        foreach (GameObject island in centerIslands)
        {
            DrawBoundingBox(island);
        }

        foreach (GameObject island in edgeIslands)
        {
            DrawBoundingBox(island);
        }
    }

    void DrawBoundingBox(GameObject island)
    {
        RectTransform rt = island.GetComponent<RectTransform>();

        if (rt == null)
        {
            return;
        }

        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        Gizmos.DrawLine(corners[0], corners[1]);
        Gizmos.DrawLine(corners[1], corners[2]);
        Gizmos.DrawLine(corners[2], corners[3]);
        Gizmos.DrawLine(corners[3], corners[0]);

    }

    Vector2 GetRandPosition(Vector2 islandSize)
    {

        float xMin = boundingAreaCenter.x - boundingAreaSize.x / 2 + islandSize.x / 2;
        float xMax = boundingAreaCenter.x + boundingAreaSize.x / 2 - islandSize.x / 2;
        float yMin = boundingAreaCenter.y - boundingAreaSize.y / 2 + islandSize.y / 2;
        float yMax = boundingAreaCenter.y + boundingAreaSize.y / 2 - islandSize.y / 2;

        float randX = Random.Range(xMin, xMax);
        float randY = Random.Range(yMin, yMax);

        return new Vector2(randX, randY);
    }

    /*
        bool IsOverlapping(Vector2 newPosition, Vector2 islandSize)
        {
            Rect newIslandRect = new Rect (
                newPosition - islandSize / 2,
                islandSize
            );

            foreach(Vector2 placedPosition in placedPositions)
            {
                Rect placedIslandRect = new Rect (
                placedPosition - islandSize / 2,
                islandSize
                );

                if(newIslandRect.Overlaps(placedIslandRect))
                {
                    return true;
                }
            }

            return false;
        }
    */

    bool IsOverlapping(Vector2 newPosition, Vector2 islandSize, float minDistance)
    {
        Rect newIslandBounds = new Rect(newPosition - (islandSize / 2), islandSize);

        foreach (Vector2 placedPos in placedPositions)
        {
            Rect placedIslandBounds = new Rect(placedPos - (islandSize / 2), islandSize);

            if (newIslandBounds.xMax + minDistance > placedIslandBounds.xMin &&
                newIslandBounds.xMin - minDistance < placedIslandBounds.xMax &&
                newIslandBounds.yMax + minDistance > placedIslandBounds.yMin &&
                newIslandBounds.yMin - minDistance < placedIslandBounds.yMax)
            {
                return true;
            }
        }
        return false;
    }

}
