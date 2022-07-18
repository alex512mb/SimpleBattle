using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(CharacterStats))]
public class CharacterAnimations : MonoBehaviour
{
    private string triggerAttack = "Attack";
    private string healthParametr = "Health";
    private int triggerAttackHash;
    private int healthParametrHash;

    private Animator animator;
    private Character character;
    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        character = GetComponent<Character>();
        Assert.IsNotNull(character);
        animator = GetComponentInChildren<Animator>();
        Assert.IsNotNull(animator);
        
        triggerAttackHash = Animator.StringToHash(triggerAttack);
        healthParametrHash = Animator.StringToHash(healthParametr);
    }

    private void OnEnable()
    {
        character.OnAttack += Character_OnAttack;
    }

    private void OnDisable()
    {
        character.OnAttack -= Character_OnAttack;
    }

    private void Update()
    {
        animator.SetInteger(healthParametrHash, (int)stats.Health);
    }

    private void Character_OnAttack()
    {
        animator.SetTrigger(triggerAttackHash);
    }
}
