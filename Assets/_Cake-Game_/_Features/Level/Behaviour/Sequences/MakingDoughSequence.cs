using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingDoughSequence : MonoBehaviour
{

    [SerializeField] Animator SugarPot;
    [SerializeField] GameObject SugarSpreadInBowl;
    [SerializeField] GameObject LeanFingerHandler;
    [SerializeField] GameObject SugarParticles;

    Action _fingerDownCurrentAction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void StartSugar()
    {
        SugarPot.gameObject.SetActive(true);
        StartCoroutine(TurnOnFingerHandler(1.5f,LeanFingerHandler));
        DOVirtual.DelayedCall(0.89f, delegate
        {
            _fingerDownCurrentAction = delegate
            {
                StartCoroutine(TurnOnFingerHandler(1.5f, SugarParticles)); 
                SugarPot.SetTrigger("Pouring");
                _fingerDownCurrentAction = null;

                // these are animation clip lengths
                DOVirtual.DelayedCall(4.163f , delegate { OnDonePouringSugar(); });
            };
        });
    }

    IEnumerator TurnOnFingerHandler(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

    void OnDonePouringSugar()
    {
        SugarParticles.SetActive(false);
        SugarSpreadInBowl.SetActive(true);
      
    }

    public void HandleFingerDown()
    {
        _fingerDownCurrentAction?.Invoke();
    }
}
