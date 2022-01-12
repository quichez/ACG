using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int dim = new Vector2Int(1, 1);
    public Cell cell;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (float i = -dim.y; i < dim.y; i++)
        {
            for (float j = -dim.x; j < dim.x; j++)
            {
                var spawnedCell = Instantiate(cell, new Vector3(i+0.5f , 0.05f, j+0.5f), Quaternion.Euler(90, 0, 0)) ;
                spawnedCell.name = $"Cell {i} {j}";
            }
        }
    }

    public Vector3 GetCenterOfCell()
    {
        return Vector3.one/2.0f;
    }
}

public interface IGridObject
{
    
}

public static class GridExtensions
{
    public static void AlignToGrid(this IGridObject _, Transform tsf)
    {
        tsf.position = new Vector3(Mathf.Floor(tsf.position.x) + 0.5f, 0.0f, Mathf.Floor(tsf.position.z) + 0.5f);
        Debug.Log("should have been called");
    }
}