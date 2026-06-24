using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Health _health;


    private void Start()
    {
        _health.OnHealthDepleted += OnEnemyKilled;
    }

    private void OnEnemyKilled(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
