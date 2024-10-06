using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] Camera UiCam;
    [SerializeField] Transform CanvasT;
    public List<UiPanelModel> UiPanels = new List<UiPanelModel>();

    Dictionary<UiType, UiPanel> _usedUiPanels = new Dictionary<UiType, UiPanel>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventManager.OnShowUi += ShowUi;
        EventManager.OnHideUi += HideUi;
        EventManager.OnHideAllUi += HideAllUi;
        EventManager.OnCheckUiStatus += CheckUiStatus;
    }

    private void OnDisable()
    {
        EventManager.OnShowUi -= ShowUi;
        EventManager.OnHideUi -= HideUi;
        EventManager.OnHideAllUi -= HideAllUi;
        EventManager.OnCheckUiStatus -= CheckUiStatus;
    }

    private void ShowUi(UiType ui, object data)
    {
        if(_usedUiPanels.ContainsKey(ui))
        {
            _usedUiPanels[ui].gameObject.SetActive(true);
            _usedUiPanels[ui].transform.SetAsLastSibling();
            _usedUiPanels[ui].Show(data);
        }
        else
        {
            var uiPanel = UiPanels.Find(uip => uip.Type == ui);
            if(uiPanel == null)
                return;

            var panel = Instantiate(uiPanel.UiPrefab, CanvasT);
            panel.Show(data);
            _usedUiPanels.Add(ui, panel);
            panel.gameObject.SetActive(true);
            _usedUiPanels[ui].transform.SetAsLastSibling();
        }
    }

    private void HideUi(UiType ui)
    {
        if(_usedUiPanels.ContainsKey(ui) == false)
        {
            //Debug.LogError(String.Format($"Trying to hide Ui panel ({0}) before it's opened. This is not allowed!", ui.ToString()));
            return;
        }

        _usedUiPanels[ui].gameObject.SetActive(false);
    }

    private void HideAllUi()
    {
        foreach(UiPanel panel in _usedUiPanels.Values)
        {
            panel.gameObject.SetActive(false);
        }
    }

    private bool CheckUiStatus(UiType ui)
    {
        if(_usedUiPanels.ContainsKey(ui))
            return _usedUiPanels[ui].gameObject.activeSelf;
        else
            return false;
    }
}

[Serializable]
public class UiPanelModel
{
    public UiType  Type;
    public UiPanel UiPrefab;
}