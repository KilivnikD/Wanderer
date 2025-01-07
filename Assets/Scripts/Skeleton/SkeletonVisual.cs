using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class SkeletonVisual : MonoBehaviour
{
    private const string IS_DAE = "IsDae";
    private const string TAKEHIT = "TakeHit";
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private GameObject _enemyShadow;
   

    SpriteRenderer spriteRenderer;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        if(_enemyEntity == null)
    {
            Debug.LogError("EnemyEntity не установлен в SkeletonVisual", this);
            return;
        }

        _enemyEntity.OnTakeHit += _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += _enemyEntity_OnDeath;

        if (_animator == null)
        {
            Debug.LogError("Animator не найден!", this);
        }

        if (_enemyShadow == null)
        {
            Debug.LogError("EnemyShadow не установлен!", this);
        }
    }

    private void _enemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TAKEHIT);

    }
    private void _enemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DAE,true);
        spriteRenderer.sortingOrder = -1;
        _enemyShadow.SetActive(false);

    }

}
