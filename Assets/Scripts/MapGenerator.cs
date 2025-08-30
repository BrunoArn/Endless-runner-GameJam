using System.Collections.Generic;
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
    [SerializeField] float despawnDistance = 30f;
    [SerializeField] int yMax, yMin;

    [Header("Biscuit")]
    [SerializeField] GameObject biscuitPrefab;
    [SerializeField] Score score;
    [SerializeField] int biscuitGaps;

    //tilemap
    private Tilemap myTileMap;
    private BoundsInt bounds;
    //crears
    private int clearUpToX;
    private readonly List<GameObject> biscuits = new();
    //generation axys
    private int lastGeneratedYFloor;
    private int lastGeneratedYRoof;
    private int lastGeneratedX;

    private int lastGeneratedXBiscuit;

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
        lastGeneratedXBiscuit = lastGeneratedX;
        clearUpToX = bounds.xMin;
    }

    private void Update()
    {
        CreateMap();
        CleanUp();
    }

    private void CreateMap()
    {
        float playerX = player.position.x;
        int targetTileX = Mathf.FloorToInt(playerX + spawnDistance);

        while (lastGeneratedX <= targetTileX)
        {
            //floor
            GenerateTiles(lastGeneratedX, yMin, -1, yMin, -2, ref lastGeneratedYFloor, true);
            //roof
            GenerateTiles(lastGeneratedX, yMax, 1, 1, yMax, ref lastGeneratedYRoof, false);

            GenerateBiscuit(lastGeneratedX, lastGeneratedYRoof, lastGeneratedYFloor);
            lastGeneratedX++;
        }
    }

    private void GenerateTiles(int x, int baseY, int extraBasePosition, int clampMin, int clampMax, ref int lastY, bool isFloor)
    {
        myTileMap.SetTile(new Vector3Int(x, baseY), tile);
        myTileMap.SetTile(new Vector3Int(x, baseY + extraBasePosition), tile); // generate below

        //wdifficulty wheights
        float movedDistance;
        if (score == null)
        {
            movedDistance = 500f;
        }
        else
        {
            movedDistance = score.GetDistanceMoved();
        }
        float progress = Mathf.InverseLerp(0f, 500f, movedDistance);
        float weightNeg, weightZero, weightPos;
        if (isFloor)
        {
            weightNeg = Mathf.Lerp(8, 1f, progress);
            weightZero = Mathf.Lerp(5, 1, progress);
            weightPos = Mathf.Lerp(2, 1f, progress);
        }
        else
        {
            weightNeg = Mathf.Lerp(2, 1f, progress);
            weightZero = Mathf.Lerp(5, 1, progress);
            weightPos = Mathf.Lerp(8, 1f, progress);
        }

        float totalWeight = weightNeg + weightPos + weightZero;
        float roll = Random.value * totalWeight;

        int deltaY;

        if (roll < weightNeg)
        {
            deltaY = -1;
        }
        else if (roll < weightNeg + weightZero)
        {
            deltaY = 0;
        }
        else
        {
            deltaY = 1;
        }

        //generate variation Y
        //int newY = lastY + Random.Range(-1, 2); // -1, 0 or 1
        int newY = Mathf.Clamp(lastY + deltaY, clampMin, clampMax); // avoids 0 and its minimun

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

    private void GenerateBiscuit(int x, int yMax, int yMin)
    {
        int chanceToSpawn = Random.Range(0, 100);
        if (chanceToSpawn >= 90 && lastGeneratedX >= lastGeneratedXBiscuit + biscuitGaps)
        {
            int randomSpawnY = Random.Range(yMin + 1, yMax - 1);
            Vector3 worldPos = myTileMap.CellToWorld(new Vector3Int(x, randomSpawnY, 0)) + new Vector3(0.5f, 0.5f, 0);
            GameObject biscuit = Instantiate(biscuitPrefab, worldPos, Quaternion.identity);
            biscuits.Add(biscuit);
            lastGeneratedXBiscuit = lastGeneratedX;
            if (biscuit.TryGetComponent<BiscuitAdd>(out var biscuitScoreReference))
            {
                biscuitScoreReference.scoreInfo = score;
            }
        }
    }

    private void CleanUp()
    {
        int keepFromX = Mathf.FloorToInt(player.position.x - despawnDistance);

        while (clearUpToX < keepFromX)
        {
            for (int y = yMin - 2; y <= yMax + 2; y++)
            {
                myTileMap.SetTile(new Vector3Int(clearUpToX, y, 0), null);
            }
            clearUpToX++;
        }

        for (int i = biscuits.Count - 1; i >= 0; i--)
        {
            var b = biscuits[i];
            if (b == null) { biscuits.RemoveAt(i); continue; }

            // convert biscuit position to cell X (same grid as tiles)
            int bx = myTileMap.WorldToCell(b.transform.position).x;
            if (bx < keepFromX)
            {
                Destroy(b);
                biscuits.RemoveAt(i);
            }
        }
        myTileMap.CompressBounds();
    }
}
