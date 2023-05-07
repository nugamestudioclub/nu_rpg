using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTilemapData2D {
    private readonly int[][] data;

    private Vector2Int size;
    public Vector2Int Size
    {
        get => size;
        set
        {
            //ThrowIfArgumentDimensionNegative(value, nameof(value));
            size = value;
        }
    }
    public RectTilemapData2D(Vector2Int size) { 
        Size = size;
    }
    public void SetTile(int tileID, int x, int y)
    {
        data[x][y] = tileID;
    }
    public int GetTile(int x, int y)
    {
        return data[x][y];
    }

    public IEnumerable<int> GetRow(int x)
    {
        for (int i = 0; i< data[x].Length; i++)
        {
            yield return data[x][i];
        }
    }

    public IEnumerable<int> GetCol(int y)
    {
        for (int i = 0; i < data.Length; i++)
        {
            yield return data[i][y];
        }
    }
}
