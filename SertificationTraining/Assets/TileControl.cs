using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour {

    public Rotator tile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        Debug.Log(tile.isSelected);
        if (tile.isSelected)
        {
            tile.transform.position = transform.position;
        }
    }
}
