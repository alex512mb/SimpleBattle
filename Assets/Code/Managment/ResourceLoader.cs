using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ResourceLoader
{
    private static string pathToGameConfig = "data";
    private static string pathToRootIcons = "Icons";

    public static Data LoadGameConfig()
    {
        try
        {
            TextAsset textAsset = (TextAsset)Resources.Load(pathToGameConfig, typeof(TextAsset));
            Data data = JsonUtility.FromJson<Data>(textAsset.text);
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"fail to load game config at path {pathToGameConfig} with error : {e.Message}");
            return null;
        }
    }

    public static Sprite LoadSprite(string name)
    {
        string fullPath = Path.Combine(pathToRootIcons, name);
        try
        {
            var tex = Resources.Load<Texture2D>(fullPath);
            Rect rect = new Rect(0, 0, tex.width, tex.height);
            Sprite sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
            return sprite;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"fail to load icon at path {fullPath} with error : {e.Message}");
            return null;
        }
        
    }
}
