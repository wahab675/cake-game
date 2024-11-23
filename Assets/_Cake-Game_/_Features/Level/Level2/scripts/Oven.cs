using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public GameObject Hand;
    public Transform bowl;
    int clickCount;

    public void OnMouseDown()
    {
        clickCount++;
        if (clickCount == 1)
        {
            CallBowl();
        }
    }

    public void CallBowl()
    {
        boxCollider.enabled = false;    
        transform.DOMove(new Vector3(5.57999992f, -6.48000002f, 0.117559448f), 1);

    }
    IEnumerator BowlToBake()
    {
        yield return new WaitForSeconds(2f);
        Hand.transform.DOMove(new Vector3(7.23002625f, -8.25f, 0.0998853445f), 0);
        Hand.SetActive(true);
    }
}
