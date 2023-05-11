using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTransactionHandler : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private TilemapReader tilemapReader;
    [SerializeField]
    private TilemapWriter tilemapWriter;
    // Start is called before the first frame update
    [SerializeField]
    private Vector3Int offset;

    private Sprite[,] sprites;
    void Awake()
    {
        sprites = tilemapReader.Read();
        tilemap.ClearAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tilemapWriter.Write(offset, sprites);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            tilemap.ClearAllTiles();
        }
    }
}
