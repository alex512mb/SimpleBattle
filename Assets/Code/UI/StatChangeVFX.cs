using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatChangeVFX : MonoBehaviour
{
    [SerializeField]
    private CharacterStats stats;

    [SerializeField]
    private StateChangeHandler[] states;

    [Header("Animation")]
    [SerializeField]
    private float distanceAnimMove = 100;
    [SerializeField]
    private float durationAnimMove = 0.5f;

    private string negativePrefix = "-";
    private string positivePrefix = "+";

    private void Start()
    {
        stats.OnStatChanged += Stats_OnStatChanged;
    }

    private void OnDestroy()
    {
        stats.OnStatChanged -= Stats_OnStatChanged;
    }

    private void Stats_OnStatChanged(int statID, float oldValue, float newValue)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (statID == states[i].targetStatID)
            {
                var textInstance = Instantiate(states[i].prefabText, transform);
                float valueDelta = newValue - oldValue;
                Color textColor = valueDelta >= 0 ? states[i].colorIncreaseValue : states[i].colorDecreaseValue;
                textInstance.color = textColor;
                string prefix = valueDelta >= 0 ? positivePrefix : negativePrefix;
                textInstance.text = $"{prefix} {Mathf.Abs(valueDelta)}";
                
                var tween = textInstance.transform.DOMoveY(textInstance.transform.position.y + distanceAnimMove, durationAnimMove);
                tween.onComplete += () =>
                {
                    Destroy(textInstance.gameObject);
                };
            }

        }
    }

    [System.Serializable]
    private class StateChangeHandler
    {
        public int targetStatID;
        public Text prefabText;
        public Color colorIncreaseValue;
        public Color colorDecreaseValue;
    }

}
