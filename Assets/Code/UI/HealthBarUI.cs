using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image bar;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private CharacterStats stats;

    private Camera cam;


    private void Awake()
    {
        cam = Camera.main;
        UpdatePos();
    }

    private void Start()
    {
        var healthStat = stats.GetStat(StatsId.LIFE_ID);
        ApplyHealthValue(healthStat.Value);
        stats.GetStat(StatsId.LIFE_ID).OnValueChanged += CharStats_Health_OnValueChanged;
    }

    private void OnDestroy()
    {
        stats.GetStat(StatsId.LIFE_ID).OnValueChanged -= CharStats_Health_OnValueChanged;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdatePos();
        UpdateFill();
    }

    private void UpdateFill()
    {
        float healthFactor = stats.Health / stats.MaxHealth;
        bar.fillAmount = healthFactor;
    }

    private void UpdatePos()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(pivot.position);
        bar.rectTransform.anchoredPosition = screenPos;
    }

    private void CharStats_Health_OnValueChanged(float oldValue, float newValue)
    {
        ApplyHealthValue(newValue);
    }

    private void ApplyHealthValue(float value)
    {
        text.text = value.ToString();
    }
}
