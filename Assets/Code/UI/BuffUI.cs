using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    public int BuffID => buffID;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text title;

    private int buffID;

    public void Setup(string iconPath, string title, int buffID)
    {
        this.title.text = title;
        icon.sprite = ResourceLoader.LoadSprite(iconPath);
        this.buffID = buffID;
    }
}
