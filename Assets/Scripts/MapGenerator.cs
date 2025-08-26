using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    private Tilemap tileMap;

    void Start()
    {
        tileMap = GetComponent<Tilemap>();
        BoundsInt bounds = tileMap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int Pos = new(x, y, 0);
                TileBase tile = tileMap.GetTile(Pos);

                if (tile != null)
                {
                    Debug.Log($"Tile at [{Pos}] is [{tile.name}]");
                }
            }
        }
    }
}
