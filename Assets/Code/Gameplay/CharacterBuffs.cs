using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterBuffs : MonoBehaviour
{
    [HideInInspector]
    public ReactiveCollection<Buff> activeBuffs = new ReactiveCollection<Buff>();

    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void ApplyBuff(Buff buff)
    {
        for (int i = 0; i < buff.stats.Length; i++)
        {
            ApplyStatMod(buff.stats[i]);
        }
        activeBuffs.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        for (int i = 0; i < buff.stats.Length; i++)
        {
            RemoveStatMod(buff.stats[i]);
        }
        activeBuffs.Remove(buff);
    }

    private void ApplyStatMod(BuffStat buffStat)
    {
        stats.AddStat(buffStat.statId, buffStat.value);
    }

    private void RemoveStatMod(BuffStat buffStat)
    {
        stats.AddStat(buffStat.statId, -buffStat.value);
    }


}
