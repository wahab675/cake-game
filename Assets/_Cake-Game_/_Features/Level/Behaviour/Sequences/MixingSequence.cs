using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingSequence : LevelSequence
{
    [SerializeField] Animator Stirrer;
    [SerializeField] GameObject[] FlourStirringImages;
    [SerializeField] GameObject StartingImage;
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject Leanfinger;
    [SerializeField] GameObject LeanfingerPouring;
    [SerializeField] DragObjectWithinBounds DragStirrer;
    [SerializeField] DOTweenController  Parent;
    [SerializeField] DOTweenController  smallbowlFlour;
    [SerializeField] DOTweenController  BigBowlFlour;
    [SerializeField] Animator BlueBallAnimator,SmallBlueBall;
    [SerializeField] GameObject BigBowl;
    [SerializeField] GameObject BowlArrow;
    [SerializeField] GameObject PouringParticle;
    [SerializeField] SpriteRenderer StirrerSpriteRend;
    [SerializeField] Sprite Stirrer1, Stirrer2;

    
    int imageCounter=0;
    // Start is called before the first frame update
    void Start()
    {
        Buttons.SetActive(true);
        //Leanfinger.SetActive(true);
        DragStirrer.enabled = false;
    }

    // Update is called once per frame
    
    public void OnStirrerButtonClick1()
    {
        StirrerSpriteRend.sprite = Stirrer1;
        Parent.gameObject.SetActive(true);
        Buttons.GetComponent<DOTweenController>().TriggerNextTween();
    }
    public void OnStirrerButtonClick2()
    {
        StirrerSpriteRend.sprite = Stirrer2;
        Parent.gameObject.SetActive(true);
        Buttons.GetComponent<DOTweenController>().TriggerNextTween();
    }
    public void PlayStirrerAnimation(string trigger)
    {
        Stirrer.SetTrigger(trigger);
    }
    private int previousIndex = -1;
    private int tempcounter =0;
    public void ChangeImages()
    {
        StartingImage.SetActive(false);
        if (imageCounter < 15 && DragStirrer.enabled==false)
        {
            // Randomly select an image index
            int randomIndex = UnityEngine.Random.Range(0, FlourStirringImages.Length-1);

            // If there was a previous active image, set it to inactive
            if (previousIndex != -1)
            {
                FlourStirringImages[previousIndex].gameObject.SetActive(false);
            }

            // Activate the randomly selected image
            FlourStirringImages[randomIndex].gameObject.SetActive(true);
            PlayStirrerAnimation("still");
            // Store the current index as previous for the next iteration
            previousIndex = randomIndex;

            // Increment the counter
            imageCounter++;
            if(imageCounter == 15)
            {
                imageCounter = 0;
                DragStirrer.enabled = true;
                Leanfinger.SetActive(false);
                DragStirrer.OnDraggingAction += ChangeImages;
            }
        }
        else if (imageCounter < 10 && DragStirrer.enabled == true)
        {
            if (imageCounter >=9)
            {
                Parent.TriggerNextTween();
            }
            StartCoroutine(ExecuteAfterDelay(0.3f, () => {
                int randomIndex = UnityEngine.Random.Range(0, FlourStirringImages.Length - 1);

                // If there was a previous active image, set it to inactive
                if (previousIndex != -1)
                {
                    FlourStirringImages[previousIndex].gameObject.SetActive(false);
                }

                // Activate the randomly selected image
                FlourStirringImages[randomIndex].gameObject.SetActive(true);
                PlayStirrerAnimation("move");
                // Store the current index sas previous for the next iteration
                previousIndex = randomIndex;

                if (tempcounter >= 20)
                {
                    tempcounter = 0;
                    imageCounter++;
                }
                tempcounter++;
                if (imageCounter == 10)
                {
                    Parent.gameObject.SetActive(false);
                    DragStirrer.OnDraggingAction -= ChangeImages;
                    DragStirrer.enabled = false;
                    FlourStirringImages[^1].gameObject.SetActive(true);
                    FlourStirringImages[previousIndex].gameObject.SetActive(false);
                    StartCoroutine(PlayAnimWithDelay(1f));
                }
            }));
           
          
           
        }

    }

    IEnumerator PlayAnimWithDelay(float delay)
    {
        yield return null;
     
        yield return new WaitForSeconds(delay);
       
        BlueBallAnimator.SetTrigger("moveout");
        yield return new WaitForSeconds(delay);
        BigBowl.SetActive(true);
        SmallBlueBall.gameObject.SetActive(true);
        SmallBlueBall.SetTrigger("movein");

        yield return new WaitForSeconds(1f);
        BowlArrow.SetActive(true);
        LeanfingerPouring.SetActive(true);
    }
    public IEnumerator ExecuteAfterDelay(float delays, Action action)
    {
        // Wait until the condition is true
        yield return new WaitForSeconds(delays);

        // Execute the passed method
        action?.Invoke();

        yield return null;
    }
    public void PourFlourInBowl()
    {
        BowlArrow.SetActive(false);
        SmallBlueBall.SetTrigger("rotate");
        StartCoroutine(ExecuteAfterDelay(0.3f, () => { PouringParticle.SetActive(true);
            smallbowlFlour.enabled = true;
            BigBowlFlour.enabled = true;

        }));
       
       
    }

    public void EndSequence()
    {
        SmallBlueBall.transform.rotation = new Quaternion(0, 0, 0, SmallBlueBall.transform.rotation.w);
        SmallBlueBall.SetTrigger("moveout");
        BigBowl.GetComponent<DOTweenController>().TriggerNextTween();
    }
}

