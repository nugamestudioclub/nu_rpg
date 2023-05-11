using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapReader : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
     
    public Sprite[,] Read()
    {
        BoundsInt bounds = tilemap.cellBounds;
        int maxX = bounds.max.x - bounds.min.x;
        int maxY = bounds.max.y - bounds.min.y;
        Sprite[,] sprites = new Sprite[maxX, maxY];
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                var pos = new Vector3Int(x + bounds.min.x, y+ bounds.min.y, 0);
                if (tilemap.HasTile(pos))
                {
                    Tile tile = tilemap.GetTile<Tile>(pos);
                    sprites[x, y] = tile.sprite;
                }
            }
        }
        return sprites;
    }
}
