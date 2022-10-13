using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 5;

    [Tooltip("Adds amount to max HP when enemy dies.")]
    [SerializeField] int difficultyRamp = 1;

    int currentHP = 0;

    Enemy enemy;

    private void OnEnable()
    {
        currentHP = maxHP;
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    void ProcessHit()
    {
        currentHP--;

        if(currentHP <= 0)
        {
            gameObject.SetActive(false);
            maxHP += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
