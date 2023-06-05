using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : MonoBehaviour
{
    public bool deleteMode;

    // Start is called before the first frame update
    void Start()
    {
        deleteMode = false;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseDown()
    {
        deleteMode = !deleteMode;
        if(deleteMode)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}
