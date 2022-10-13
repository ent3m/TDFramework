using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathFinder;

    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinder>();
    }
    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        
        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();       //empty the path list so it starts from a blank state
        path = pathFinder.GetNewPath();
        StartCoroutine(FollowPath());        //use StartCoroutine to call a Coroutine function
    }
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    IEnumerator FollowPath()     //return type IEnumerator signifies that this function is a Coroutine
    {
        for (int i = 1; i < path.Count; i++)      //loops through each element in the list
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPos);       //make enemy faces the position it's heading to

            while(travelPercent < 1f)       //move the enemy slowly to the target position using a while loop in a Coroutine
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();       //continues the while loop after the end of each frame. yield return is needed to complete setting up the Coroutine
            } 
        }
        FinishPath();
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);        //deactivate the gameobject after reaching the final waypoint
    }
}
