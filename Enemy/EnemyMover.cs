using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    Enemy enemy;

    private void Awake()
    {
        FindPath();
    }
    private void OnEnable()
    {
        transform.position = path[0].transform.position;        //set the enemy to spawn on the first waypoint
        StartCoroutine(FollowPath());        //use StartCoroutine to call a Coroutine function
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void FindPath()
    {
        path.Clear();       //empty the path list so it starts from a blank state

        GameObject parent = GameObject.FindGameObjectWithTag("Path");       //find the empty game object Path that contains all waypoints in the hierarchy

        foreach(Transform child in parent.transform)        //transform component supports enumerators so you can loop through all the children using foreach loop
        {
            if(child.GetComponent<Waypoint>() != null)
            {
                path.Add(child.GetComponent<Waypoint>());
            }
        }
    }

    IEnumerator FollowPath()     //return type IEnumerator signifies that this function is a Coroutine
    {
        foreach (var waypoint in path)      //loops through each element in the list
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
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
