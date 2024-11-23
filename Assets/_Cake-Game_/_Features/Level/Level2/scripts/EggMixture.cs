using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EggMixture : MonoBehaviour
{
    public Animation HandAnination;
    public Animator mixture;
    private bool Completed=false;
    public UnityEvent Complete;
    public void Update()
    {
        if (Input.GetMouseButton(0)&& !Completed) 
            {
         
            mixture.SetBool("mix", true);
            HandAnination.Play();
            mixture.speed = 1;
            }
        else
        {
            HandAnination.Stop();
            mixture.speed = 0;
        }
    }

    public void mixtureComplete()
    {
        mixture.SetBool("mix", false);
        Completed=true;
        Complete?.Invoke();
    }
}
