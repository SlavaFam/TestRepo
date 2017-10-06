using UnityEngine;


public class TileControler : MonoBehaviour
{

    public Tile selectedTile;
    public Placeholder placaholderPrefab;
    private Vector3 tileScale = new Vector3(1, 0, 1);
    public Tile []allTiles;

    public void SelectTile(Tile tile)
    {
        if (tile == selectedTile)
        {
            return;
        }
        if (selectedTile)
            DeselectTile();
        selectedTile = tile;
        selectedTile.transform.localScale += tileScale;
        Debug.Log(string.Format("{0} tile has been selected", selectedTile.name));
        selectedTile.isSelected = true;
    }

    public void DeselectTile()
    {
        if (!selectedTile.isPlanted)
        {
            selectedTile.transform.localScale -= tileScale;
            selectedTile.transform.position = selectedTile.startPosition;
        }
        selectedTile.isSelected = false;
        selectedTile = null; 
    }

    public void PlaceTile()
    {
        selectedTile.isPlanted = true;
        SpawnNewPlaceholders(selectedTile.transform.position);
        DeselectTile();
        

    }

    public void SpawnNewPlaceholders(Vector3 initialPosition)
    {
        var x = 10f;
        Vector3 []positions = { initialPosition + new Vector3(x, 0, 0),
                initialPosition - new Vector3(x, 0, 0),
                initialPosition -new Vector3(0, 0, x),
                initialPosition + new Vector3(0, 0, x)};
        foreach(var pos in positions)
        {
            var canPlace = true;
            foreach(var tile in allTiles)
            {
                if(tile.transform.position == pos)
                {
                    canPlace = false;
                    break;
                }
            }
            if (canPlace)
            {
                Instantiate(placaholderPrefab, pos, Quaternion.identity);
            }
        }
    }

}
