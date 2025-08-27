using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MapGenerator : MonoBehaviour
{
    [Header("Tile generation")]
    [SerializeField] Transform player;
    [SerializeField] TileBase tile;
    [Space]
    [SerializeField] float spawnDistance;
    [SerializeField] int yMax, yMin;

    [Header("Biscuit")]
    [SerializeField] GameObject biscuitPrefab;
    [SerializeField] Score score;


    private Tilemap myTileMap;
    private BoundsInt bounds;
    private int lastGeneratedYFloor;
    private int lastGeneratedYRoof;

    private int lastGeneratedX;

    private void Awake()
    {
        lastGeneratedYFloor = yMin;
        lastGeneratedYRoof = yMax;
    }

    void Start()
    {
        myTileMap = GetComponent<Tilemap>();
        myTileMap.CompressBounds();

        bounds = myTileMap.cellBounds;
        lastGeneratedX = bounds.xMax;
    }

    private void Update()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        float playerX = player.position.x;
        int targetTileX = Mathf.FloorToInt(playerX + spawnDistance);

        while (lastGeneratedX <= targetTileX)
        {
            //floor
            GenerateTiles(lastGeneratedX, yMin, -1, yMin, -1, ref lastGeneratedYFloor);
            //roof
            GenerateTiles(lastGeneratedX, yMax, 1, 1, yMax, ref lastGeneratedYRoof);

            GenerateBiscuit(lastGeneratedX, lastGeneratedYFloor + 1);
            lastGeneratedX++;
        }
    }

    private void GenerateTiles(int x, int baseY, int extraBasePosition, int clampMin, int clampMax, ref int lastY)
    {
        myTileMap.SetTile(new Vector3Int(x, baseY), tile);
        myTileMap.SetTile(new Vector3Int(x, baseY + extraBasePosition), tile); // generate below

        //generate variation Y
        int newY = lastY + Random.Range(-1, 2); // -1, 0 or 1
        newY = Mathf.Clamp(newY, clampMin, clampMax); // avoids 0 and its minimun

        //generate fillers an last
        int from = Mathf.Min(baseY, newY);
        int to = Mathf.Max(baseY, newY);
        for (int y = from; y <= to; y++)
        {
            Vector3Int cellPosition = new(x, y, 0);
            myTileMap.SetTile(cellPosition, tile);
        }
        lastY = newY;
    }

    private void GenerateBiscuit(int x, int y)
    {
        int chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn >= 80)
        {
            Vector3 worldPos = myTileMap.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0.5f, 0.5f, 0);
            GameObject biscuit = Instantiate(biscuitPrefab, worldPos, Quaternion.identity);
            
            if (biscuit.TryGetComponent<BiscuitAdd>(out BiscuitAdd biscuitScoreReference))
            {
                biscuitScoreReference.scoreInfo = score;
            }
        }

    }
}
