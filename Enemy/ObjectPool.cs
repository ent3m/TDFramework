using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefab, this.transform);
            pool[i].SetActive(false);
        }
    }
    IEnumerator SpawnEnemy()        //return type IEnumerator signifies that this function is a Coroutine
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);        //yield return is needed to complete setting up the Coroutine
                                                                //the function exits after yield return, continues with other codes, and comes back to this function after 1 second
        }
    }
    void EnableObjectInPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                break;
            }
        }
    }
}
