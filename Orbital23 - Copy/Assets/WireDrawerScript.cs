using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireDrawerScript : MonoBehaviour
{
    private List<GameObject> NodesSelected;
    private GameObject currentWire;
    [SerializeField] private GameObject WirePrefab;

    // Start is called before the first frame update
    void Start()
    {
        NodesSelected = new List<GameObject>();
    }

    // Called when Node is a clicked
    public void NodeOnSelect(GameObject Node)
    {
        List<GameObject> NodesRecolor = new List<GameObject>() {Node};

        // Keep track of node that player clicks or unclicks
        if (NodesSelected.Contains(Node))
        {
            NodesSelected.Remove(Node);
        }
        else
        {
            NodesSelected.Add(Node);

            // Draws line and reset node color once 2 nodes are selected
            if (NodesSelected.Count == 2)
            {
                DrawLine();
                NodesRecolor = new List<GameObject>(NodesSelected);
                NodesSelected.Clear();
            }
        }

        // Change node color depending on whether node is clicked or unclicked
        foreach (GameObject NodeToColor in NodesRecolor)
        {
            Debug.Log(NodeToColor.name);
            var NodeScript = NodeToColor.GetComponent<NodeScript>();
            NodeScript.changeNodeColor(NodesSelected.Contains(NodeToColor));
        }

        
    }

    // Called to draw line
    public void DrawLine()
    {
        // Spawn and draw line
        currentWire = Instantiate(WirePrefab, NodesSelected[0].transform.position, Quaternion.identity);
        currentWire.SetActive(true);
        currentWire.GetComponent<WireScript>().SpawnLine(NodesSelected);
    }
}
