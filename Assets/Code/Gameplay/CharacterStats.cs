using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CharacterStats : MonoBehaviour
{
    public event StateChangeDelegate OnStatChanged;

    public float Health
    {
        get { return GetStat(StatsId.LIFE_ID).Value; }
        set { SetStat(StatsId.LIFE_ID, value); }
    }
    public float MaxHealth => GetStat(StatsId.LIFE_ID).MaxValue;
    public float Armor
    {
        get { return GetStat(StatsId.ARMOR_ID).Value; }
        set { SetStat(StatsId.ARMOR_ID, value); }
    }
    public float Damage
    {
        get { return GetStat(StatsId.DAMAGE_ID).Value; }
        set { SetStat(StatsId.DAMAGE_ID, value); }
    }
    public float LifeSteal
    {
        get { return GetStat(StatsId.LIFE_STEAL_ID).Value; }
        set { SetStat(StatsId.LIFE_STEAL_ID, value); }
    }
    public IReadOnlyDictionary<int, CharacterStat> allStats => dictStats;

    [SerializeField]
    private CharacterStat health = new CharacterStat(100);
    [SerializeField]
    private CharacterStat armor = new CharacterStat(0);
    [SerializeField]
    private CharacterStat damage = new CharacterStat(10);
    [SerializeField]
    private CharacterStat lifeSteal = new CharacterStat(0);
    
    private Dictionary<int, CharacterStat> dictStats = new Dictionary<int, CharacterStat>();

    private void Awake()
    {
        dictStats.Add(StatsId.LIFE_ID, health);
        dictStats.Add(StatsId.ARMOR_ID, armor);
        dictStats.Add(StatsId.DAMAGE_ID, damage);
        dictStats.Add(StatsId.LIFE_STEAL_ID, lifeSteal);

        Initialize();

        foreach (var item in dictStats)
        {
            int statID = item.Key;
            item.Value.OnValueChanged += (oldValue, newValue) =>
            {
                OnStatChanged?.Invoke(statID, oldValue, newValue);
            };
        }
    }

    [ContextMenu("PrintValue")]
    private void PrintValue()
    {
        Debug.Log(dictStats[0]);
    }

    public void Initialize()
    {
        foreach (var stat in dictStats.Values)
        {
            stat.Initialize();
        }
    }

    public void SetStats(CharacterStats otherStats)
    {
        foreach (var statID in dictStats.Keys)
        {
            SetStat(statID, otherStats.GetStat(statID).Value);
        }
    }

    public void AddStat(int stateID, float value)
    {
        SetStat(stateID, GetStat(stateID).Value + value);
    }

    public void SetStat(int statID, float value)
    {
        dictStats[statID].Value = value;
    }

    public CharacterStat GetStat(int statID)
    {
        return dictStats[statID];
    }

    public delegate void StateChangeDelegate(int statID , float oldValue, float newValue);
    public delegate void ValueChangeDelegate(float oldValue, float newValue);

    [System.Serializable]
    public class CharacterStat
    {
        public event ValueChangeDelegate OnValueChanged;

        public float Value
        {
            get => value;
            set
            {
                float oldValue = this.value;

                this.value = value;

                if (value > maxValue)
                    maxValue = value;

                if (oldValue != value)
                {
                    OnValueChanged?.Invoke(oldValue, value);
                }
            }
        }

        public float MaxValue
        {
            get => maxValue;
            private set
            {
                maxValue = value;
            }
        }

        [SerializeField]
        private float value = 1;
        
        private float maxValue;
        private float defaultValue;

        public CharacterStat(int v)
        {
            this.value = v;
        }

        public void Initialize()
        {
            defaultValue = value;
            maxValue = value;
        }

        public void Reset()
        {
            Value = defaultValue;
            maxValue = defaultValue;
        }
    }

}
