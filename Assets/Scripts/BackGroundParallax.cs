using UnityEngine;

public class BackGroundParallax : MonoBehaviour
{
    public Transform[] backgroundTiles; // Assign BG_1, BG_2, BG_3 here
    public Transform player;
    public float parallaxFactor = 0.3f;

    private float tileWidth;

    private void Start()
    {
        tileWidth = backgroundTiles[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the background based on player's X position * parallaxFactor
        Vector3 newPos = new Vector3(player.position.x * parallaxFactor, transform.position.y, transform.position.z);
        transform.position = newPos;

        // Reposition tiles that are off-screen
        foreach (Transform tile in backgroundTiles)
        {
            if (player.position.x - tile.position.x > tileWidth)
            {
                // Move tile to the rightmost position
                float rightMostX = GetRightMostTileX();
                tile.position = new Vector3(rightMostX + tileWidth, tile.position.y, tile.position.z);
            }
        }
    }

    float GetRightMostTileX()
    {
        float maxX = backgroundTiles[0].position.x;
        foreach (Transform tile in backgroundTiles)
        {
            if (tile.position.x > maxX)
                maxX = tile.position.x;
        }
        return maxX;
    }
}
