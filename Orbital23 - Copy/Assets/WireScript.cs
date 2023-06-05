using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    private LineRenderer lr;
    private List<GameObject> NodesConnected;

    // Wire drawing and collider generation
    public void SpawnLine(List<GameObject> NodesSelected)
    {
        // Create a copy of NodesSelected to NodesConnected
        NodesConnected = new List<GameObject>(NodesSelected);
        
        // Initialise node positions of wire
        List<Vector3> NodePositions = NodesConnected.ConvertAll(p => p.transform.position);

        // Drawing the wire
        lr = GetComponent<LineRenderer>();
        for (int i = 0; i < NodePositions.Count; i++)
        {
            lr.SetPosition(i, NodePositions[i]);
        }

        // Finding positions for polygon collider corners and generating polygon collider around wire
        List<Vector2>  ColliderPoints = CalculateColliderPoints(NodePositions);
        GetComponent<PolygonCollider2D>().SetPath(0, ColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

    // Returning positions for polygon collider corners
    private List<Vector2> CalculateColliderPoints(List<Vector3> NodePositions)
    {
        // Get width of line
        float width = lr.startWidth;

        // Gradient of line
        float m = (NodePositions[1].y - NodePositions[0].y) / (NodePositions[1].x - NodePositions[0].x);

        // X and Y offset of corners of polygon collider from centre of the line
        float deltaX = (width / 2) * Mathf.Sin(Mathf.Atan(m));
        float deltaY = (width / 2) * Mathf.Cos(Mathf.Atan(m));

        // 2 possible offsets(X,Y) of corners of polygon collider from centre of line
        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        // Generate polygon collider corners
        List<Vector2> ColliderPositions = new List<Vector2>
        {
            NodePositions[0]+offsets[0],
            NodePositions[0]+offsets[1],
            NodePositions[1]+offsets[1],
            NodePositions[1]+offsets[0],
        };
        return ColliderPositions;

    }

    // Destroy lines in destroy mode
    private void OnMouseDown()
    {
        if (GameObject.Find("DeleteModeButton").GetComponent<DeleteScript>().deleteMode)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
        ColorChange(new Color32(0, 0, 0, 180));
    }

    private void OnMouseExit()
    {
        ColorChange(new Color32(0,0,0,255));
    }

    private void ColorChange(Color32 color)
    {
        lr.startColor = color;
        lr.endColor = color;
    }
}
