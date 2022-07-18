using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private Button buttonPlayWithConfig;
    [SerializeField]
    private Button buttonPlayWithoutConfig;


    private void OnEnable()
    {
        buttonPlayWithConfig.onClick.AddListener(ButtonPlayWithConfig_OnClick);
        buttonPlayWithoutConfig.onClick.AddListener(ButtonPlayWithoutConfig_OnClick);
    }

    private void OnDisable()
    {
        buttonPlayWithConfig.onClick.RemoveListener(ButtonPlayWithConfig_OnClick);
        buttonPlayWithoutConfig.onClick.RemoveListener(ButtonPlayWithoutConfig_OnClick);
    }

    private void ButtonPlayWithConfig_OnClick()
    {
        GlobalParametrs.gameloadMode = GameloadMode.WithBaffs;
        ReloadScene();
    }

    private void ButtonPlayWithoutConfig_OnClick()
    {
        GlobalParametrs.gameloadMode = GameloadMode.WithoutBaffs;
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
