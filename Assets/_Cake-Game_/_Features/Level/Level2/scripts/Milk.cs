using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class Milk : MonoBehaviour
{
    public GameObject directionArrow;
    public GameObject particles;
    public BoxCollider2D boxCollider;

    public Transform instPoint;
    public Transform milk;
    public UnityEvent OnCOmplete;
    public void Start()
    {

    }

    void OnMouseDown()
    {

        MilkBagMove();
    }

    IEnumerator MilkDrop()
    {
        yield return new WaitForSeconds(1f);
        particles.SetActive(true);
        milk.DOScale(new Vector3(.83f, .83f, .83f), 1);
        yield return new WaitForSeconds(2f);
        particles.SetActive(false);
        transform.DOLocalMove(new Vector3(17.6700001f, -5.57000017f, 0.117559448f), 1);
        OnCOmplete?.Invoke();
    }


    void MilkBagMove()
    {
        boxCollider.enabled = false;

        directionArrow.SetActive(false);
        transform.DOLocalMove(new Vector3(9.72f, -1.59f, .11755f), 1).OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(0, 0, 91f), 1).OnComplete(() =>
            {
                StartCoroutine(MilkDrop());
            });
        });
    }
}

