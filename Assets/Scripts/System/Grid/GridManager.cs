using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.CellGeneration;
using ACG.Resources;

public class GridManager : MonoBehaviour
{
    public Vector2Int dim = new Vector2Int(1, 1);
    public List<Cell> cell;
    [SerializeField] MountainCell _mountainCell;
    [SerializeField] ForestCell _forestCell;

    Cell[,] cellArray;

    private void Start()
    {
        StartCoroutine(GenerateGrid());
    }

    private IEnumerator GenerateGrid()
    {
        int[,] gen = CellGenerator.GenerateLand(dim*2,cell);
        for (int i = -dim.y; i < dim.y; i++)
        {
            for (int j = -dim.x; j < dim.x; j++)
            {
                var spawnedCell = Instantiate(cell[gen[j + dim.x, i + dim.y]], new Vector3(j+0.5f , 0.05f, i+0.5f), Quaternion.Euler(90, 0, 0),transform) ;
                spawnedCell.name = $"Cell {j + dim.x} {i + dim.y}";

                if (i == 0 && j == 0) CameraManager.Instance.MoveCamera(spawnedCell.transform);
            }
        }

        // Generate Mountains - See Summary
        yield return StartCoroutine(GenerateMountains());

        // Generate Forests
        yield return StartCoroutine(CO_GenerateForests());

        // After all cells generated, generate resources
        yield return StartCoroutine(GenerateResources());

       
    }
    /// <summary>
    /// Generate Moutains: First pass creates random mountain tiles on field tiles. Second pass increases width of mountains.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateMountains()
    {               
        // First Pass
        for (int i = 0; i < transform.childCount; i++)
        {
            
            float first = Random.value;
            if (first < 0.05f && transform.GetChild(i).TryGetComponent(out FieldCell fc))
            {               
                Vector3 pos = fc.transform.position;
                Quaternion rot = fc.transform.rotation;
                Destroy(transform.GetChild(i).gameObject);                
                Instantiate(_mountainCell, pos,rot, transform);                
            }
        }
        yield return new WaitForEndOfFrame(); // allow for map to update


        // Second Pass
        MountainCell[] mcs = transform.GetComponentsInChildren<MountainCell>();
        foreach (MountainCell mCell in mcs)
        {
            Collider[] cellsAroundMountain = Physics.OverlapBox(mCell.transform.position, Vector3.one, Quaternion.identity, SelectorManager.Instance.CellMask);
            foreach (Collider item in cellsAroundMountain)
            {
                if (item.TryGetComponent(out MountainCell _)) continue;
                if (Random.value < 0.15f && mCell.gameObject != item.gameObject)
                {
                    Instantiate(_mountainCell, item.transform.position, item.transform.rotation, transform);
                    Destroy(item.gameObject);
                    yield return new WaitForEndOfFrame();

                }
            }
        }
        yield return new WaitForEndOfFrame(); // allow for map to update
    }

    private IEnumerator CO_GenerateForests()
    {
        // First Pass
        for (int i = 0; i < transform.childCount; i++)
        {
            float first = Random.value;
            if(first < 0.1f && transform.GetChild(i).TryGetComponent(out FieldCell fc))
            {
                ReplaceCellWithNewCell(fc, _forestCell);                
            }
        }

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator GenerateResources()
    {
        foreach (Transform child in transform)
        {           
            if (child.TryGetComponent(out ICellResources fc)) fc.InitializeResources();
        }

        yield return new WaitForEndOfFrame(); // allow for map to update
    } 

    public Vector3 GetCenterOfCell()
    {
        return Vector3.one/2.0f;
    }

    private void ReplaceCellWithNewCell(Cell oldCell, Cell newCell)
    {
        Vector3 pos = oldCell.transform.position;
        Quaternion rot = oldCell.transform.rotation;
        Destroy(oldCell.gameObject);
        Instantiate(newCell, pos, rot, transform);
    }

    private void Update()
    {

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
    }
}