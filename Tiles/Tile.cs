using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }     //a property, like a getter method, that returns a read-only value of the isPlaceable bool

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!IsPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }   
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);       //call the CreateTower method within the Tower script to instantiate it because the script is attached to the prefab
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}
