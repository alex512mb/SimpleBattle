using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UniRx;
using System.Collections;
using System.Collections.Generic;

public class PlayerPanelHierarchy : MonoBehaviour
{
    [SerializeField]
    private BuffUI prefabBaffUI;
    [SerializeField]
    private Button attackButton;
    [SerializeField]
    private Transform statsPanel;
    [SerializeField]
    private CharacterBuffs characterBuffs;
    [SerializeField]
    private Character character;
    [SerializeField]
    private CharacterStats stats;

    private List<BuffUI> activeBaffs = new List<BuffUI>();


    private void Awake()
    {
        Assert.IsNotNull(characterBuffs);
    }

    private void OnEnable()
    {
        attackButton.onClick.AddListener(AttackButton_OnClick);
    }

    private void OnDisable()
    {
        attackButton.onClick.RemoveListener(AttackButton_OnClick);
    }

    private void Start()
    {
        SubscribeOnBuffChanges();
        CreateStatsUI();
        CreateBuffsUI();
    }

    private void CreateBuffsUI()
    {
        foreach (var buff in characterBuffs.activeBuffs)
        {
            CreateEntityUI(buff.icon, buff.title, buff.id);
        }
    }

    private void CreateStatsUI()
    {
        Data data = ResourceLoader.LoadGameConfig();
        for (int i = 0; i < data.stats.Length; i++)
        {
            var stat = data.stats[i];
            CreateEntityUI(stat.icon, stats.GetStat(stat.id).Value.ToString(), stat.id);
        }
    }

    private void AttackButton_OnClick()
    {
        character.Attack();
    }

    private void Character_OnBuffAdded(CollectionAddEvent<Buff> addEvent)
    {
        Buff buff = addEvent.Value;
        BuffUI baffUI = CreateEntityUI(buff.icon, buff.title, buff.id, true);
        activeBaffs.Add(baffUI);
    }

    private BuffUI CreateEntityUI(string icon, string title, int id, bool addToEnd = false)
    {
        var baffUI = Instantiate(prefabBaffUI, statsPanel);
        baffUI.Setup(icon, title, id);
        return baffUI;
    }

    private void Character_OnBuffRemoved(CollectionRemoveEvent<Buff> removeEvent)
    {
        Buff buff = removeEvent.Value;
        var buffUI = activeBaffs.Find(b => b.BuffID == buff.id);
        activeBaffs.Remove(buffUI);
        Destroy(buffUI.gameObject);
    }

    private void SubscribeOnBuffChanges()
    {
        var observerAdd = characterBuffs.activeBuffs.ObserveAdd();
        var observerRemove = characterBuffs.activeBuffs.ObserveRemove();
        observerAdd.Subscribe(Character_OnBuffAdded).AddTo(this);
        observerRemove.Subscribe(Character_OnBuffRemoved).AddTo(this);
    }
}
