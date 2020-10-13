using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RG.Utils;
using Room; // may remove to preserve strong typing

public class RoomGenerator : MonoBehaviour
{
    private Grid<Cell> grid;
    private const int width = 8;
    private const int height = 10;
    private float cellSize;
    public Vector3 originPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Room template 1
        Room.CellType[,] CT = new Room.CellType[width, height]
        {   //Bottom left                                                                                        //Top Left
            {CellType.tg,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e},
            {CellType.tg,CellType.e,CellType.e,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.e},
            {CellType.tg,CellType.e,CellType.e,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.e},
            {CellType.tg,CellType.e,CellType.e,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.e},
            {CellType.tg,CellType.e,CellType.e,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.e},
            {CellType.tg,CellType.e,CellType.e,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.e},
            {CellType.tg,CellType.e,CellType.e,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.rg,CellType.e},
            {CellType.tg,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e,CellType.e},
            //Bottom right                                                                                       //Top right
        };

        cellSize = 5.0f;
        grid = new Grid<Cell>(width, height, cellSize, originPosition, () => new Cell());
        Cell[,] cells = grid.GetGrid();
        // Loop throught the grid of cells and determine which cells get ground tiles.
        for(int i = 0; i < cells.GetLength(0); i++)
        {
            for(int j = 0; j < cells.GetLength(1); j++)
            {
                // Current cell
                Cell cell = (Cell)cells.GetValue(i,j);
                cell.cellType = CT[i,j]; // Set room types to the template
                if(cell.cellType == CellType.tg) // Place template ground tiles
                {
                    cell.PlaceTile(grid.GetWorldPosition(i,j), cellSize);
                }
                // If the cell is a random cell. Generate a random number between 0-2 (actual values would be 0 and 1 bc of how the range function works)
                else if(cell.cellType == CellType.rg)
                {
                    int number = Random.Range(0,2);
                    Debug.Log(number);
                    if(number==1)
                    {
                        cell.PlaceTile(grid.GetWorldPosition(i,j), cellSize);
                    }
                }

                // Debug draw room types
                // grid.SetDebugTextValue(i,j, RG.Utils.Utils.CreateWorldText(
                //         cells[i,j].GetCellType().ToString(), 
                //         null, 
                //         grid.GetWorldPosition(i,j) + new Vector3(cellSize, cellSize) *0.5f, 
                //         20, 
                //         Color.white,
                //         TextAnchor.MiddleCenter
                //     )
                // );   
            }
        }
    }
}
