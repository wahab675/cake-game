using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SandBag : MonoBehaviour
{
    public GameObject directionArrow;
   
    public BoxCollider2D boxCollider;
   
    public Transform instPoint;
    public UnityEvent OnCOmplete;
    public void Start()
    {

    }

    void OnMouseDown()
    {

        ThrowJelly();

    }
    void ThrowJelly()
    {
        boxCollider.enabled = false;
      
        directionArrow.SetActive(false);

        transform.DOLocalMove(new Vector3(9.72f, -1.59f, .11755f), 1).OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(0, 0, 91f), 1).OnComplete(() =>
            {
                StartCoroutine(BeansDrop());
            });
        });


    }
    IEnumerator BeansDrop()
    {
     
        yield return new WaitForSeconds(1);
        transform.DOLocalMove(new Vector3(17.6700001f, -5.57000017f, 0.117559448f), 1);
        OnCOmplete?.Invoke();
    }


    void SandBagMove()
    {
        transform.DOLocalMove(new Vector3(9.72f, -1.59f, .11755f), 1).OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(0, 0, 91f), 1).OnComplete(() =>
            {
                StartCoroutine(BeansDrop());
            });
        });
    }
}
