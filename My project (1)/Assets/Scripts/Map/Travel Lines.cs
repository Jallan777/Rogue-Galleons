using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelLines : MonoBehaviour
{

    public GameObject linePrefab;
    public float maxDistance = 350.0f;

    private List<Vector2> nodePositions = new List<Vector2>();
    private List<GameObject> lines = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadNodePositions()
    {
        int index = 0;

        while(PlayerPrefs.HasKey("NodeX_" + index))
        {
            float nodeX = PlayerPrefs.GetFloat("NodeX_" + index);
            float nodeY = PlayerPrefs.GetFloat("NodeY_" + index);
            Vector2 savedNodePos = new Vector2(nodeX, nodeY);

            nodePositions.Add(savedNodePos);
            index++;
        }
    }

    void DrawLine(Vector2 start, Vector2 end)
    {
        GameObject lineObject = Instantiate(linePrefab);
        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0));
        lineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0));

        lines.Add(lineObject);

    }

    void LinesBetweenNodes()
    {
        foreach(GameObject line in lines)
        {
            Destroy(line);
        }

        lines.Clear();

        for(int i = 0; i < nodePositions.Count; i++)
        {
            for(int j = i + 1; j < nodePositions.Count; j++)
            {
                Vector2 pos1 = nodePositions[i];
                Vector2 pos2 = nodePositions[j];

                float distance = Vector2.Distance(pos1, pos2);

                if(distance <= maxDistance)
                {
                    DrawLine(pos1, pos2);
                }
            }
        }
    }
}
