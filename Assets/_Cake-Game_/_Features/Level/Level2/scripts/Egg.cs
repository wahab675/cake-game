using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Egg : MonoBehaviour
{
    public GameObject directionArrow;

    public BoxCollider2D boxCollider;
    public SpriteRenderer eggSprite;
    public Transform instPoint;
    public GameObject brokenEgg;
    private int clickCOunt;


    [Header("Broken Egg again Tap")]
    public Transform brokenEggOneCutPiece;
    public Transform eggFromBrokenEgg;
    public GameObject eggOnBowl;




    public UnityEvent OnCOmplete;
    public void Start()
    {

    }

    void OnMouseDown()
    {
        clickCOunt++;
        if (clickCOunt == 1)
        {
            MilkBagMove();
        }else
        {
            eggBrok();
        }

    }
    void eggBrok()
    {
        brokenEggOneCutPiece.DOLocalRotate(new Vector3(0, 0, 51.9999962f), .5f);
        eggFromBrokenEgg.DOScale(new Vector3(.8f, .8f, .8f), 1).OnComplete(() => {
            eggOnBowl.gameObject.SetActive(true);
            
            OnCOmplete?.Invoke();
            gameObject.SetActive(false);
        });
    }
   


    void MilkBagMove()
    {
        boxCollider.enabled = false;

        directionArrow.SetActive(false);
        transform.DOLocalMove(new Vector3(9.52999973f, -3.33999991f, 0.117559448f), 1).OnUpdate(() =>
        {

            transform.DOLocalRotate(new Vector3(0, 0, 91f), 1).OnComplete(() =>
            {
                eggSprite.enabled = false;
                brokenEgg.SetActive(true);
                boxCollider.enabled = true;
               transform.DOLocalMove(new Vector3(5.96999979f, -0.850000024f, 0.117559448f),.5f);
            });
        });
    }
}
