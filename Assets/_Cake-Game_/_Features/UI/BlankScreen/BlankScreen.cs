using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlankScreen : MonoBehaviour
{
    [SerializeField] Image _screenImage;
    [SerializeField] float _fadeInDuration = 1.0f;
    [SerializeField] float _fadeOutDuration = 1.0f;

    public static float FadeInDuration { get { return instance._fadeInDuration; } }
    public static float FadeOutDuration { get { return instance._fadeOutDuration; } }

    static BlankScreen instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(instance != null && instance._screenImage != null)
            instance._screenImage.raycastTarget = false;
    }

    public static void FadeIn()
    {
        if(instance != null && instance._screenImage != null)
        {
            //Debug.Log("FADING IN...");
            instance._screenImage.transform.SetAsLastSibling();
            instance._screenImage.raycastTarget = true;
            instance._screenImage.DOFade(1.0f, instance._fadeInDuration);
        }
    }

    public static void FadeOut()
    {
        if(instance != null && instance._screenImage != null)
        {
            instance._screenImage.transform.SetAsLastSibling();
            instance._screenImage.raycastTarget = false;
            instance._screenImage.DOFade(0.0f, instance._fadeOutDuration);
        }
    }

    public static void FadeIn(float duration)
    {
        if(instance != null && instance._screenImage != null)
        {
            instance._screenImage.transform.SetAsLastSibling();
            instance._screenImage.raycastTarget = true;
            instance._screenImage.DOFade(1.0f, duration);
        }
    }

    public static void FadeOut(float duration)
    {
        if(instance != null && instance._screenImage != null)
        {
            //Debug.Log("FADING OUT...");
            instance._screenImage.transform.SetAsLastSibling();
            instance._screenImage.raycastTarget = false;
            instance._screenImage.DOFade(0.0f, duration);
        }
    }
}
