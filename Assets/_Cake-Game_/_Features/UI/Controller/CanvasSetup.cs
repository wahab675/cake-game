using UnityEngine;

public class CanvasSetup : MonoBehaviour
{
    [SerializeField] Camera IngameUiCam;
    [SerializeField] Canvas[] IngameCanvases;
    [SerializeField] Canvas[] EventBasedCanvases;

    private void OnEnable()
    {
        EventManager.OnSwitchUiCanvasType += SwitchCanvasType;
    }
    private void OnDisable()
    {
        EventManager.OnSwitchUiCanvasType -= SwitchCanvasType;
    }

    private void Start()
    {
        for(int i = 0; i < IngameCanvases.Length; i++)
        {
            IngameCanvases[i].renderMode = RenderMode.ScreenSpaceCamera;
            IngameCanvases[i].worldCamera = IngameUiCam;
        }
    }

    void SwitchCanvasType(RenderMode type)
    {
        foreach(Canvas canvas in EventBasedCanvases)
        {
            canvas.renderMode = type;
        }
    }
}
