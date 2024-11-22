using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class JellyBar : MonoBehaviour
{
    public GameObject directionArrow;
    public GameObject topCap;
    public BoxCollider2D boxCollider;
    public GameObject[] beasn;
    public Transform instPoint;
    public void Start()
    {
        
    }

    void OnMouseDown()
    {
       // if (Input.GetMouseButtonDown(0))
       // {
          
            ThrowJelly();
       // } 
    }
    void ThrowJelly()
    {
        boxCollider.enabled = false;
        topCap.SetActive(false);
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
        for (int i = 0; i < beasn.Length; i++)
        {
            Instantiate(beasn[i], instPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(.3f);
        }

        transform.DOLocalMove(new Vector3(17.6700001f, -5.57000017f, 0.117559448f), 1);
    }
   
}
