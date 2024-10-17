using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceTravelNodes : MonoBehaviour
{

    public GameObject[] islands;
    public GameObject node;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject island in islands)
        {
            PolygonCollider2D polyCollider = island.GetComponent<PolygonCollider2D>();

            if(polyCollider != null)
            {
                Vector2 randomEdgePoint = GetRandomPointOnPolygonEdge(polyCollider);

                PlaceAtPosition(randomEdgePoint, island.transform);
            }
            else
            {
                Debug.LogWarning("PolygonCollider2D not found on island: " + island.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 GetRandomPointOnPolygonEdge(PolygonCollider2D polyCollider)
    {
        // Get all the points of the polygon
        Vector2[] points = polyCollider.points;
        Vector2[] worldPoints = new Vector2[points.Length];

        // Convert local points to world space
        for (int i = 0; i < points.Length; i++)
        {
            worldPoints[i] = polyCollider.transform.TransformPoint(points[i]);
        }

        // Randomly select an edge
        int randomEdgeIndex = Random.Range(0, worldPoints.Length);
        Vector2 startPoint = worldPoints[randomEdgeIndex];
        Vector2 endPoint = worldPoints[(randomEdgeIndex + 1) % worldPoints.Length]; // Loop back to first point

        // Get a random point along this edge
        float t = Random.Range(0f, 1f);
        Vector2 randomEdgePoint = Vector2.Lerp(startPoint, endPoint, t);

        return randomEdgePoint;
    }

    void PlaceAtPosition(Vector2 position, Transform parent)
    {
        GameObject newObject = Instantiate(node, position, Quaternion.identity);
        newObject.transform.SetParent(parent, false); // Set the parent to the island
        Debug.Log("Placed object at: " + position);
    }
}
