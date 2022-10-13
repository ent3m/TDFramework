using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]     //enable code to run in both editor and play modes
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);     //pass in RGB values to construct color Orange

    TextMeshPro label;
    Vector2Int coordinates;
    GridManager gridManager;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        gridManager = FindObjectOfType<GridManager>();

        DisplayCoordinates();       //make the coordinates shown during play mode. no need to update since tile objects aren't moved around in play mode
    }
    void Update()
    {
        if (!Application.isPlaying)     //ensure the coordinates only update during editor mode
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }
    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void SetLabelColor()
    {
        if (gridManager == null)
        { 
            return;
        }

        Node node = gridManager.GetNode(coordinates);

        if (node == null)
        {
            return;
        }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);      //use the position of the parent object (tile) and divide it by grid snap value
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
