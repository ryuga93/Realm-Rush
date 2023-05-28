using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("World Grid Size should match the UnityEditor Snap Settings.")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize => unityGridSize;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid => grid;

    void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            grid[coordinate].isWalkable = false;
        }
    }

    public void ResetNode()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int blockCoordinate = new Vector2Int();
        blockCoordinate.x = Mathf.RoundToInt(position.x / unityGridSize);
        blockCoordinate.y = Mathf.RoundToInt(position.z / unityGridSize);

        return blockCoordinate;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinate)
    {
        Vector3 position = new Vector3();
        position.x = coordinate.x * unityGridSize;
        position.z = coordinate.y * unityGridSize;

        return position;
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
