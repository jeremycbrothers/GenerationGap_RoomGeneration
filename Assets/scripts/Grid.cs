using System;
using System.Collections.Generic;
using UnityEngine;
using RG.Utils;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] grid;
    private TextMesh[,] debugTextArray;
    private Vector3 originPosition;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        grid = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x,y] = createGridObject();
            }
        }

        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                Debug.Log(grid[x,y].ToString());
                //debugTextArray[x,y] = Utils.CreateWorldText(grid[x,y]?.ToString(), null, GetWorldPosition(x,y) + new Vector3(cellSize, cellSize) *0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x, y+1), Color.white, 100.0f);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1, y), Color.white, 100.0f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100.0f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width,height), Color.white, 100.0f);
    }

    public TGridObject[,] GetGrid()
    {
        return grid;
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if(x >= 0 && y >=0 && x < width && y < height)
        {
            grid[x, y] = value;
            debugTextArray[x, y].text = grid[x,y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x,y,value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if(x >= 0 && y >=0 && x < width && y < height)
        {
            return grid[x,y];
        }
        return default(TGridObject);
    }

    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x,y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x,y);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition-originPosition).y / cellSize);
    }
}
