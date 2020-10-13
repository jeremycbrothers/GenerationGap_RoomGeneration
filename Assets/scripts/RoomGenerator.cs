using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RG.Utils;

public class RoomGenerator : MonoBehaviour
{
    private Grid<Cell> grid;

    // Start is called before the first frame update
    void Start()
    {
       grid = new Grid<Cell>(8, 10, 5.0f, new Vector3(-20,-25,0), () => new Cell());
       Cell[,] cells = grid.GetGrid();
       for(int i = 0; i < cells.GetLength(0); i++)
       {
           for(int j = 0; j < cells.GetLength(1); j+=2)
           {
                Cell cell = (Cell)cells.GetValue(i,j);   
                cell.PlaceTile(grid.GetWorldPosition(i,j), 5.0f);    
           }
       }
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 position = Utils.GetMouseWorldPosition();
//            grid.SetValue(position, true);
        }
    }
}

public class Cell
{
    private bool hasTile;
    private Sprite[] groundSprites;
    private GameObject ground;

    public Cell()
    {
        groundSprites = Resources.LoadAll<Sprite>("tileset-sliced");
        ground = new GameObject();
        ground.AddComponent<SpriteRenderer>();
    }

    public void PlaceTile(Vector3 position, float cellSize)
    {
        ground.GetComponent<SpriteRenderer>().sprite = groundSprites[0];
        
        BoxCollider2D box = ground.AddComponent<BoxCollider2D>();
        box.size = new Vector2(cellSize,cellSize);
        ground.gameObject.transform.position = position + new Vector3(cellSize, cellSize) *0.5f;
    }
}
