using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MapGenerator : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TileBase tile;
    [SerializeField] float spawnDistance;

    [SerializeField] int yMax, yMin;

    private Tilemap myTileMap;
    private BoundsInt bounds;
    private int lastGeneratedYFloor;
    private int lastGeneratedYRoof;

    private int lastGeneratedX;

    void Start()
    {
        myTileMap = GetComponent<Tilemap>();
        myTileMap.CompressBounds();

        bounds = myTileMap.cellBounds;
        lastGeneratedX = bounds.xMax;

        lastGeneratedYFloor = yMin;
        lastGeneratedYRoof = yMax;
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
            //floor
            //generate base Floor
            myTileMap.SetTile(new Vector3Int(lastGeneratedX, yMin), tile);
            myTileMap.SetTile(new Vector3Int(lastGeneratedX, yMin - 1), tile); // generate below

            //generate variation Y
            int floorY = lastGeneratedYFloor + Random.Range(-1, 2); // -1, 0 or 1
            floorY = Mathf.Clamp(floorY, yMin, -1); // avoids 0 and its minimun
            for (int y = yMin; y < floorY; y++)
            {

                myTileMap.SetTile(new Vector3Int(lastGeneratedX, y), tile);
            }
            lastGeneratedYFloor = floorY;

            //Roof
            //generate base Roof
            myTileMap.SetTile(new Vector3Int(lastGeneratedX, yMax), tile);
            myTileMap.SetTile(new Vector3Int(lastGeneratedX, yMax + 1), tile); // generate above

            int roofY = lastGeneratedYRoof + Random.Range(-1, 2); // -1, 0 or 1
            floorY = Mathf.Clamp(roofY, 1, yMax); // avoids 0 and its minimun
            for (int y = roofY; y < yMax; y++)
            {

                myTileMap.SetTile(new Vector3Int(lastGeneratedX, y), tile);
            }
            lastGeneratedYRoof = roofY;

            lastGeneratedX++;
        }
    }
}
