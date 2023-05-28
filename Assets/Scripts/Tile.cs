using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable => isPlaceable;

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinate = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinate);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinate).isWalkable && !pathFinder.WillBlockPath(coordinate))
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            
            if (isPlaced)
            {
                gridManager.BlockNode(coordinate);
                pathFinder.NotifyReceivers();
            }    
        }
    }
}
