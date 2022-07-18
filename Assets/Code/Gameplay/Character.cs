using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

[RequireComponent(typeof(CharacterStats))]
public class Character : MonoBehaviour
{
    public event Action OnAttack;

    private float Health
    {
        get { return stats.Health; }
        set { stats.Health = value; }
    }
    private float MaxHealth => stats.MaxHealth;
    private float Armor
    {
        get { return stats.Armor; }
        set { stats.Armor = value; }
    }
    private float Damage
    {
        get { return stats.Damage; }
        set { stats.Damage = value; }
    }
    private float LifeSteal
    {
        get { return stats.LifeSteal; }
        set { stats.LifeSteal = value; }
    }

    [SerializeField]
    private Character target;
    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void Attack()
    {
        if (Health <= 0)
            return;

        OnAttack?.Invoke();
        target.ApplyDamage(Damage, LifeSteal, this);   
    }

    public void ApplyDamage(float damage, float lifeSteal, Character attacker)
    {
        if (Health <= 0)
            return;

        Armor = Mathf.Clamp(Armor, 0, 100f);
        float armorCoeff = 1 - (Armor / 100f);
        float actualDamage = damage * armorCoeff;
        actualDamage = Mathf.Min(Health, actualDamage); 

        float vampiricCoeff = lifeSteal / 100f;
        float actualVampiricDamage = actualDamage * vampiricCoeff;

        Health -= actualDamage;

        attacker.ApplyHeal(actualVampiricDamage);
    }

    public void ApplyHeal(float heal)
    {
        Health = Mathf.Clamp(Health + heal, 0 , MaxHealth);
    }
}
