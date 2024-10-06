using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainmenuPanel : UiPanel
{
    public Button PlayBtn;
    public Button SettingBtn;

    private void OnEnable()
    {
        PlayBtn.onClick.AddListener(OnClicked_PlayBtn);
        SettingBtn.onClick.AddListener(OnClicked_SettingBtn);
    }
    private void OnDisable()
    {
        PlayBtn.onClick.RemoveListener(OnClicked_PlayBtn);
        SettingBtn.onClick.RemoveListener(OnClicked_SettingBtn);
    }

    private void OnClicked_PlayBtn()
    {
        SoundController.Instance.PlaySound(SoundType.Click);

        EventManager.DoFireHideUiEvent(UiType.MainMenu);
        EventManager.DoFireStartGameLevel();
    }

    private void OnClicked_SettingBtn()
    {
        SoundController.Instance.PlaySound(SoundType.Click);

        EventManager.DoFireShowUiEvent(UiType.Setting);
    }

}
