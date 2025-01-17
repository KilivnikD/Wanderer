
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    [SerializeField] private Sword sword;
    private const string ATTACK= "Attack";

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
        
    }

    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);

    }
    public void TriggerEndAttackAnimation()
    {
        sword.AttackColliderTurnOff();
    }
}
