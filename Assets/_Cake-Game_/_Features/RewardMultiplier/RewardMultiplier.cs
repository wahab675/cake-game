using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardMultiplier : MonoBehaviour
{
    public Button ClaimWithAdButton;

    [SerializeField] RectTransform Arrow;
    [SerializeField] float MoveDur = 1f;
    [SerializeField] Ease MoveEase = Ease.Linear;
    [SerializeField] TextMeshProUGUI MultiplierText;

    [SerializeField] Vector2[] ArrowMultipliersAndPoss;

    public float CurrentMultiplier { get; private set; } = 2f;

    public void AnimateMultiplierView(float startingCoins)
    {
        Arrow.anchoredPosition = new Vector2(ArrowMultipliersAndPoss[0].y, Arrow.anchoredPosition.y);
        Arrow.DOAnchorPosX(ArrowMultipliersAndPoss[ArrowMultipliersAndPoss.Length - 1].y, MoveDur)
             .SetEase(MoveEase)
             .SetLoops(-1, LoopType.Yoyo)
             .OnUpdate(() =>
             {
                 UpdateCurrentMultiplierForPos(Arrow.anchoredPosition.x, startingCoins);
             });
    }

    void UpdateCurrentMultiplierForPos(float pos, float startingCoins)
    {
        for(int i = 0; i < ArrowMultipliersAndPoss.Length - 1; i++)
        {
            if(pos >= ArrowMultipliersAndPoss[i].y && pos < ArrowMultipliersAndPoss[i + 1].y)
            {
                CurrentMultiplier = ArrowMultipliersAndPoss[i].x;
            }
        }

        float multipliedCoins = CurrentMultiplier * startingCoins;
        uint c = (uint) multipliedCoins;

        MultiplierText.text = Helper.FormatNumber(c);
    }

    public void Stop()
    {
        Arrow.DOKill();
    }
}
