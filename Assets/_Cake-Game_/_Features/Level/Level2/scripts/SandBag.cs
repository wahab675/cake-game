using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class SandBag : MonoBehaviour
{
    public GameObject directionArrow;
    public BoxCollider2D boxCollider;
    public GameObject particles;
    public Transform instPoint;
    public Transform flour;
    public UnityEvent OnCOmplete;
    public void Start()
    {

    }

    void OnMouseDown()
    {

        SandBagMove();

    }
   
    IEnumerator SandDrop()
    {
        particles.SetActive(true);
        yield return new WaitForSeconds(1f);
        flour.DOScale(new Vector3(.73f, .73f, .73f), 1);
        yield return new WaitForSeconds(2f);
        particles.SetActive(false);
        transform.DOLocalMove(new Vector3(17.6700001f, -5.57000017f, 0.117559448f), 1);
        OnCOmplete?.Invoke();
    }


    void SandBagMove()
    {
        boxCollider.enabled = false;

        directionArrow.SetActive(false);
        transform.DOLocalMove(new Vector3(9.72f, -1.59f, .11755f), 1).OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(0, 0, 91f), 1).OnComplete(() =>
            {
                StartCoroutine(SandDrop());
            });
        });
    }
}
