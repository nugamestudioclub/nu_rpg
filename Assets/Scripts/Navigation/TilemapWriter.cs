using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapWriter : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    // Start is called before the first frame update

    [SerializeField]
    private Sprite[] tileSprites;
    void Start()
    {
        /*
        Sprite[,] sprites = new Sprite[
            tileSprites.GetLength(0),
            tileSprites.GetLength(0)];

        for (int x = 0; x < tileSprites.GetLength(0); x++)
        {
            for (int y = 0; y < tileSprites.GetLength(0); y++)
            {

                if (Random.Range(0,5) ==0)
                {
                    sprites[x,y] = tileSprites[x];
                }
            }
        }
        var offset = new Vector3Int(-5,-5,0);   
        Write(offset, sprites);
        */
    }

    public void Write(Vector3Int offset, Sprite[,] write)
    {
        for(int x = 0; x < write.GetLength(0); x++)
        {
            for (int y = 0; y < write.GetLength(1); y++)
            {
                Tile tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
                tile.sprite = write[x,y];
                tilemap.SetTile(new Vector3Int(x,y) + offset, tile);
            }
        }
    }
}
