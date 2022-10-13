using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node       //classes not inheriting from Monobehavior is a pure C# class and cannot be attached to any GameObject. they are used to contain data
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)       //a constructor is a method that has the same name as the class. call a constructor by using "new Node(coordinates, isWalkable)"
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
