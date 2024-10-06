using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuController : MonoBehaviour
{
    public GameObject[] MainmenuItems;

    private void OnEnable()
    {
        EventManager.OnStartGameLevel += HideMainmenuItems;
        EventManager.OnShowUi += ShowMainmenuItems;
    }

    private void OnDisable()
    {
        EventManager.OnStartGameLevel -= HideMainmenuItems;
        EventManager.OnShowUi -= ShowMainmenuItems;
    }

    private void ShowMainmenuItems(UiType ui, object data)
    {
        if(ui != UiType.MainMenu)
            return;

        foreach(var item in MainmenuItems)
            item.SetActive(true);
    }

    private void HideMainmenuItems()
    {
        foreach(var item in MainmenuItems)
            item.SetActive(false);
    }
}
