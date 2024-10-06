using UnityEngine;

public static partial class EventManager
{
    public delegate void ShowUiEvent(UiType ui, object data = null);
    public static ShowUiEvent OnShowUi;
    public static void DoFireShowUiEvent(UiType ui, object data = null) => OnShowUi?.Invoke(ui, data);

    public delegate void HideUiEvent(UiType ui);
    public static HideUiEvent OnHideUi;
    public static void DoFireHideUiEvent(UiType ui) => OnHideUi?.Invoke(ui);

    public delegate void HideAllUi();
    public static HideAllUi OnHideAllUi;
    public static void DoFireHideAllUi() => OnHideAllUi?.Invoke();

    public delegate bool CheckUiStatusEvent(UiType ui);
    public static CheckUiStatusEvent OnCheckUiStatus;
    public static bool DoFireCheckUiStatus(UiType ui) => OnCheckUiStatus.Invoke(ui);

    public delegate void SwtichUiCanvasType(RenderMode mode);
    public static SwtichUiCanvasType OnSwitchUiCanvasType;
    public static void DoFireSwitchUiCanvasType(RenderMode mode) => OnSwitchUiCanvasType.Invoke(mode);

    public delegate void ShowHideMenuChar(bool status);
    public static ShowHideMenuChar OnShowHideMenuCharacter;
    public static void DoFireShowHideMenuCharacter(bool status) => OnShowHideMenuCharacter?.Invoke(status);

}
