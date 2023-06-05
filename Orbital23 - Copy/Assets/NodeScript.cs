using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public string NodeValue;
    public bool NodeConnected;
    [SerializeField] private Collider2D NodeCollider;

    // Initialise default state of NodeConnected
    private void Start()
    {
        NodeConnected = false;
    }

    // Check and change NodeConnected state every frame
    private void Update()
    {
        int CollidersOverNode = NodeCollider.OverlapCollider(new ContactFilter2D().NoFilter(), new List<Collider2D>());
        NodeConnected = (CollidersOverNode > 0);
    }

    // Node on click
    private void OnMouseDown()
    {
        GameObject.Find("WireDrawer").GetComponent<WireDrawerScript>().NodeOnSelect(gameObject);
    }

    // Change colour depending on selection state
    public void changeNodeColor(bool NodeSelected)
    {
        SpriteRenderer Sprite = GetComponent<SpriteRenderer>();
        if (NodeSelected)
        {
            Sprite.color = new Color32(255, 219, 18, 170);
        }
        else
        {
            Sprite.color = new Color32(255, 219, 18, 255);
        }
    }
}
