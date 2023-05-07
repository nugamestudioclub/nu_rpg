using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnityTileDataReader : MonoBehaviour
{
    private Tile tile;
    [SerializeField]
    private Sprite tileSprite;
    [SerializeField]
    Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        tile.sprite = tileSprite;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), tile);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
