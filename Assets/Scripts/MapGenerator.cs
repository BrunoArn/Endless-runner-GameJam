using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MapGenerator : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TileBase tile;
    [SerializeField] float spawnDistance;

    [SerializeField] int yLimitPositive, yLimitNegative;

    private Tilemap myTileMap;
    private BoundsInt bounds;

    private int lastGeneratedX;

    void Start()
    {
        myTileMap = GetComponent<Tilemap>();
        myTileMap.CompressBounds();

        bounds = myTileMap.cellBounds;
        
        lastGeneratedX = bounds.xMax;
        Debug.Log(lastGeneratedX);

        //StartCoroutine(SpawnTiles());

    }

    private void Update()
    {
        SpawnTiles();
    }

    private void SpawnTiles()
    {
        float playerX = player.position.x;
        int targetTileX = Mathf.FloorToInt(playerX + spawnDistance);

        while (lastGeneratedX <= targetTileX)
        {
            Vector3Int tilePosFloor = new(lastGeneratedX, yLimitNegative + 1, 0);
            Vector3Int tilePosRoof = new(lastGeneratedX, yLimitPositive - 1, 0);
            myTileMap.SetTile(tilePosFloor, tile);
            myTileMap.SetTile(new Vector3Int(lastGeneratedX, yLimitPositive), tile);

            myTileMap.SetTile(tilePosRoof, tile);
            myTileMap.SetTile(new Vector3Int(lastGeneratedX, yLimitNegative), tile);
            lastGeneratedX++;
        }
    }

    private void GetTilesPosition()
    {
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int Pos = new(x, y, 0);
                TileBase tile = myTileMap.GetTile(Pos);

                if (tile != null)
                {
                    Debug.Log($"Tile at [{Pos}] is [{tile.name}]");
                }
            }
        }
    }
}
