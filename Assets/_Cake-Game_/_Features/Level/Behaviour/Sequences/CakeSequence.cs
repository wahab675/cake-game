using System.Collections;
using System;
using UnityEngine;
using DG.Tweening;
using ScratchCardAsset;
public class CakeSequence : LevelSequence
{
    [Serializable]
    public enum CakeType
    {
        ChocoCake=0,
        PlaneCake=1,
        StawberryCake=2
    }



    [SerializeField] CakeType cakeType = CakeType.ChocoCake;
    [SerializeField] CakeData[] cakeData;
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
    [SerializeField] Animator FirstToppingCanvasAnimator;
    [SerializeField] Animator ThirdToppingCanvasAnimator;
    [SerializeField] DragObjectWithinBounds SpectulaObj;
    [SerializeField] DragObjectWithinBounds IcingTool;
    [SerializeField] ScratchCardManager FinalCakeScratchCard;
    [SerializeField] SpriteRevealFromTop BorderOfCake;

    [Header("SpriteRenderer")]
    public SpriteRenderer CakeImage;
    public SpriteRenderer SpatulaImage;
    public SpriteRenderer BowlFillingImage;
    public SpriteRenderer SpreaderImage;
    public SpriteRenderer UpperLayerImage;
    public SpriteRenderer SideLayerImage;

    public Vector3 sideborderpos;
    public Vector3 sideborderpos1;

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

    void AssignCakeProperties(CakeType type)
    {
        cakeData[(int)type].CakeSprite.SetActive(true);
        SpatulaImage.sprite = cakeData[(int)type].SpatulaSpritee;
        BowlFillingImage.sprite = cakeData[(int)type].BowlFillingSprite;
       
    }

    void assignSpreaderProperties(CakeType type)
    {
        SpreaderImage.sprite = cakeData[(int)type].SpreaderSprite;
        UpperLayerImage.sprite = cakeData[(int)type].UpperLayerSprite;
        SideLayerImage.sprite = cakeData[(int)type].SideLayerSprite;
        if (type == CakeType.PlaneCake)
        {
            SideLayerImage.transform.position = sideborderpos;
        }
        else if(type == CakeType.StawberryCake)
        {
            SideLayerImage.transform.position = sideborderpos1;
        }
    }
    public void OnCakeButtonClick(int type)
    {
        AssignCakeProperties((CakeType)type);
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

    public void OnIcingButtonClick(int type)
    {
        assignSpreaderProperties((CakeType)type);
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
            CakeTopIcing.localScale.x + 0.01f,
            CakeTopIcing.localScale.y + 0.01f,
            CakeTopIcing.localScale.z + 0.01f);
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
    } public void OnClickFirstToppingButton()
    {
        ToppingCanvasAnimator.SetTrigger("out");
        FirstToppingCanvasAnimator.gameObject.SetActive(true);
        StartCoroutine(ExecuteAfterDelay(1f, () => { ToppingCanvasAnimator.gameObject.SetActive(false); })); 
    } public void OnClickThirdToppingButton()
    {
        ToppingCanvasAnimator.SetTrigger("out");
        ThirdToppingCanvasAnimator.gameObject.SetActive(true);
        StartCoroutine(ExecuteAfterDelay(1f, () => { ToppingCanvasAnimator.gameObject.SetActive(false); })); 
    }
}

[Serializable]
public class CakeData
{
    public GameObject CakeSprite;
    public Sprite SpatulaSpritee;
    public Sprite BowlFillingSprite;
    public Sprite SpreaderSprite;
    public Sprite UpperLayerSprite;
    public Sprite SideLayerSprite;
}
