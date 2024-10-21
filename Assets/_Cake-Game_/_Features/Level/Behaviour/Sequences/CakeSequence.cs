using System.Collections;
using System;
using UnityEngine;
using DG.Tweening;
using ScratchCardAsset;
public class CakeSequence : LevelSequence
{
    [SerializeField] DOTweenController CakeTray;
    [SerializeField] DOTweenController CakeBowl;
    [SerializeField] Transform Cake;
    [SerializeField] Transform CakeTopIcing;
    [SerializeField] GameObject FinalCake;
    [SerializeField] GameObject BowlFront;
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject[] LineFInger;
    [SerializeField] Animator CakeCanvasAnimator;
    [SerializeField] Animator IcingCanvasAnimator;
    [SerializeField] Animator ToppingCanvasAnimator;
    [SerializeField] Animator BowlWithChoco;
    [SerializeField] Animator TutHand;
    [SerializeField] Animator Toppings;
    [SerializeField] DragObjectWithinBounds SpectulaObj;
    [SerializeField] DragObjectWithinBounds IcingTool;
    [SerializeField] ScratchCardManager FinalCakeScratchCard;
    [SerializeField] SpriteRevealFromTop BorderOfCake;
    void Start()
    {
        StartSequence();
        SpectulaObj.OnDraggingAction += OnSpectulaDrag;
    }

    public void StartSequence()
    {
        CakeTray.gameObject.SetActive(true);
    }

    public void OnCakeTrayTweenComplete()
    {
        CakeBowl.gameObject.SetActive(true);

    }
    public void OnCakeBowlTweenComplete()
    {
        Arrow.gameObject.SetActive(true);
        TurnOnLeanFinger(0, true);
    }

    public void OnCLickLeanFingerOne()
    {
        Arrow.gameObject.SetActive(false);
        CakeMovementToTray();
        TurnOnLeanFinger(0, false);
    }
    public void TurnOnLeanFinger(int index, bool TurnOn)
    {
        LineFInger[index].SetActive(TurnOn);
    }
    // Update is called once per frame
    public void CakeMovementToTray()
    {
        Cake.parent = CakeTray.transform;
        Cake.DOMoveY(Cake.position.y + 2.5f, 1.2f).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                BowlFront.SetActive(false);
                Cake.DOLocalMove(new Vector3(-0.02f, 2.56f, 0), 2f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    Cake.DOScale(new Vector3(0.908f, 0.876f, 0.75f),1f);
                    CakeBowl.TriggerNextTween();
                    CakeCanvasAnimator.gameObject.SetActive(true);
                    CakeCanvasAnimator.SetTrigger("in");
                });
            });
    }

    public void OnCakeButtonClick()
    {
        CakeCanvasAnimator.SetTrigger("out");
        BowlWithChoco.gameObject.SetActive(true);
        BowlWithChoco.SetTrigger("moveinspread");
        StartCoroutine(ExecuteAfterDelay(1f, () => { CakeCanvasAnimator.gameObject.SetActive(false); }));
        StartCoroutine(ExecuteAfterDelay(1f, () =>
        {
            BowlWithChoco.SetTrigger("getchoco");
            StartCoroutine(ExecuteAfterDelay(3.1f, () =>
            {
                BowlWithChoco.enabled = false;
                SpectulaObj.enabled = true;
                TutHand.gameObject.SetActive(true);
                TutHand.SetTrigger("spectulatocake");
                SpectulaObj.GetComponent<SpriteRenderer>().sortingOrder = 5;
                FinalCake.SetActive(true);
                Cake.gameObject.SetActive(false);
                FinalCakeScratchCard.gameObject.SetActive(true);
            }));
        }));
    }

    public IEnumerator ExecuteAfterDelay(float delays, Action action)
    {
        // Wait until the condition is true
        yield return new WaitForSeconds(delays);

        // Execute the passed method
        action?.Invoke();

        yield return null;
    }

    public void OnSpectulaDrag()
    {
        TutHand.gameObject.SetActive(false);
        if (SpectulaObj.isDragging)
        {
            FinalCakeScratchCard.Card.InputEnabled = true;
        }
        else
        {
            FinalCakeScratchCard.Card.InputEnabled = false;
        }

        if (FinalCakeScratchCard.Progress.currentProgress == 1)
        {
            FinalCakeScratchCard.gameObject.SetActive(false);
            SpectulaObj.gameObject.SetActive(false);
            IcingCanvasAnimator.gameObject.SetActive(true);
            IcingCanvasAnimator.SetTrigger("in");
        }
    }

    public void OnIcingButtonClick()
    {
        IcingCanvasAnimator.SetTrigger("out");
        IcingTool.gameObject.SetActive(true);
        CakeTopIcing.gameObject.SetActive(true);
        StartCoroutine(ExecuteAfterDelay(1f, () => { IcingCanvasAnimator.gameObject.SetActive(false); }));
        IcingTool.OnDraggingAction += OnDraggingIcingTool;
    }
    float epsilon = 0.001f;
    public void OnDraggingIcingTool()
    {
        // Define an epsilon to account for floating-point precision issues


        // Check if the scale is approximately 1 on all axes
        if (Mathf.Abs(CakeTopIcing.localScale.x - 1f) < epsilon &&
            Mathf.Abs(CakeTopIcing.localScale.y - 1f) < epsilon &&
            Mathf.Abs(CakeTopIcing.localScale.z - 1f) < epsilon)
        {
            BorderOfCake.gameObject.SetActive(true);
            BorderOfCake.enabled = true;
            StartCoroutine(ExecuteAfterDelay(2f, () =>
            {

                IcingTool.GetComponent<DOTweenController>().TriggerNextTween();
            }));
            IcingTool.enabled = false;
            return;
        }

        // Incrementally increase the scale if it's not yet 1
        CakeTopIcing.localScale = new Vector3(
            CakeTopIcing.localScale.x + 0.001f,
            CakeTopIcing.localScale.y + 0.001f,
            CakeTopIcing.localScale.z + 0.001f);
    }

    public void Topping_Canvas()
    {

        ToppingCanvasAnimator.gameObject.SetActive(true);
        ToppingCanvasAnimator.SetTrigger("in");
    }

    public void OnClickStawberryButton()
    {
        ToppingCanvasAnimator.SetTrigger("out");
        Toppings.gameObject.SetActive(true);
        StartCoroutine(ExecuteAfterDelay(1f, () => { ToppingCanvasAnimator.gameObject.SetActive(false); })); 
    }
}
