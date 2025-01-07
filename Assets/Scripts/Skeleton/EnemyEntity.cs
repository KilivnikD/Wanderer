using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    private int _currentHealth;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    private CapsuleCollider2D _capsuleCollider2D;
    private EnemyAI _enemyAI;
    private void Awake()
    {
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();

        if (_enemyAI == null)
        {
            Debug.LogError("EnemyAI не найден на объекте!", this);
        }
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if(_currentHealth <= 0)
        {
            _capsuleCollider2D.enabled = false;

            _enemyAI.SetDeathState();
            OnDeath?.Invoke(this, EventArgs.Empty);

            //Destroy(gameObject);
        }
            
    }
}
