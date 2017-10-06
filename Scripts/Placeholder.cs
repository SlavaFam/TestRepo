using UnityEngine;

public class Placeholder : MonoBehaviour {

    private TileControler tileControler;
	// Use this for initialization
	void Start () {
        tileControler = FindObjectOfType<TileControler>();        
    }
	

    void OnMouseEnter()
    {
        var tile = tileControler.selectedTile;
        if (tile)
            tile.transform.position = transform.position;
    }


}
