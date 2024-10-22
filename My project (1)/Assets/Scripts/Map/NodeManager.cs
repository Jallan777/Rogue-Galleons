using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    private List<NodeController> nodes = new List<NodeController>();
    private const string selectedNodeKey = "SelectedNodeIndex";

    // Start is called before the first frame update
    void Start()
    {
        nodes.AddRange(FindObjectsOfType<NodeController>());   
        //SelectRandNode();
        LoadChosenNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNodeSelected(NodeController selectedNode)
    {

        Vector2 selectedPos = selectedNode.GetPosition();
        Debug.Log("Selected Node Position");

        SaveChosenNode(selectedNode);

        foreach(NodeController node in nodes)
        {
            if(node == selectedNode) continue;

            float distance = Vector2.Distance(selectedPos, node.GetPosition());
            

        }
    }

    public void RegisterNode(NodeController node)
    {
        nodes.Add(node);
    }

    public void SelectRandNode()
    {
        if(nodes.Count > 0)
        {
            int randIndex = Random.Range(0, nodes.Count);
            nodes[randIndex].SetSelected(true, true);
        }
    }

    private void SaveChosenNode(NodeController selectedNode)
    {
        int selectedIndex = nodes.IndexOf(selectedNode);
        PlayerPrefs.SetInt(selectedNodeKey, selectedIndex);
        PlayerPrefs.Save();
    }

    private void LoadChosenNode()
    {
        if(PlayerPrefs.HasKey(selectedNodeKey))
        {
            int savedIndex = PlayerPrefs.GetInt(selectedNodeKey);

            if(savedIndex >= 0 && savedIndex < nodes.Count)
            {
                nodes[savedIndex].SetSelected(true, true);
            }
            else
            {
                SelectRandNode();
            }
        }
        else
        {
            SelectRandNode();
        }
    }
}
