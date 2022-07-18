using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[DefaultExecutionOrder(-100)]
public class GameplayComposition : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characters;

    private void Start()
    {
        Data data = ResourceLoader.LoadGameConfig();

        ApplyDefaultState(data);
        
        if (GlobalParametrs.gameloadMode == GameloadMode.WithBaffs)
        {
            ApplyBuffs(data);
        }
    }

    private void ApplyBuffs(Data data)
    {
        List<Buff> listNotUsedBuffs = new List<Buff>();

        for (int charIndex = 0; charIndex < characters.Length; charIndex++)
        {
            CharacterBuffs charBuffs = characters[charIndex].GetComponent<CharacterBuffs>();
            int buffCount = Random.Range(data.settings.buffCountMin, data.settings.buffCountMax + 1);
            if (data.settings.allowDuplicateBuffs)
            {
                for (int buffIndex = 0; buffIndex < buffCount; buffIndex++)
                {
                    int randomBuffIndex = Random.Range(0, data.buffs.Length);
                    Buff randomBuff = data.buffs[randomBuffIndex];
                    charBuffs.ApplyBuff(randomBuff);
                }
            }
            else
            {
                buffCount = Mathf.Min(buffCount, data.buffs.Length);
                listNotUsedBuffs.Clear();
                listNotUsedBuffs.AddRange(data.buffs);
                for (int buffIndex = 0; buffIndex < buffCount; buffIndex++)
                {
                    int randomBuffIndex = Random.Range(0, listNotUsedBuffs.Count);
                    Buff randomBuff = listNotUsedBuffs[randomBuffIndex];
                    charBuffs.ApplyBuff(randomBuff);
                    listNotUsedBuffs.RemoveAt(randomBuffIndex);
                }
            }


        }
    }

    private void ApplyDefaultState(Data data)
    {
        for (int charIndex = 0; charIndex < characters.Length; charIndex++)
        {
            CharacterStats charStats = characters[charIndex].GetComponent<CharacterStats>();
            for (int i = 0; i < data.stats.Length; i++)
            {
                charStats.SetStat(data.stats[i].id, data.stats[i].value);
            }
        }
    }
}
