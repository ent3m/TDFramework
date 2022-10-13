using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }     //a property, like a getter method, that returns a read-only value of the isPlaceable bool

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);       //call the CreateTower method within the Tower script to instantiate it because the script is attached to the prefab
            isPlaceable = !isPlaced;        //make the tile not placeable if the CreateTower method returns true (success)
        }
    }
}
