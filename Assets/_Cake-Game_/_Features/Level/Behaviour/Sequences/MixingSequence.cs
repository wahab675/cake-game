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
    [SerializeField] DragObjectWithinBounds DragStirrer;
    [SerializeField] DOTweenController  Parent;

    
    int imageCounter=0;
    // Start is called before the first frame update
    void Start()
    {
        Buttons.SetActive(true);
        //Leanfinger.SetActive(true);
        DragStirrer.enabled = false;
    }

    // Update is called once per frame
    
    public void OnStirrerButtonClick()
    {
        Parent.gameObject.SetActive(true);
        Buttons.GetComponent<DOTweenController>().TriggerNextTween();
    }
    public void PlayStirrerAnimation(string trigger)
    {
        Stirrer.SetTrigger(trigger);
    }
    private int previousIndex = -1;
    public void ChangeImages()
    {
        StartingImage.SetActive(false);
        if (imageCounter < 15 && DragStirrer.enabled==false)
        {
            // Randomly select an image index
            int randomIndex = Random.Range(0, FlourStirringImages.Length-1);

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
                DragStirrer.OnDragBeginAction += ChangeImages;
            }
        }
        else if (imageCounter < 10 && DragStirrer.enabled == true)
        {
            int randomIndex = Random.Range(0, FlourStirringImages.Length - 1);

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

            // Increment the counter
            imageCounter++;
            if(imageCounter == 10)
            {
                Parent.TriggerNextTween();
                DragStirrer.OnDragBeginAction -= ChangeImages;
                DragStirrer.enabled = false;
                FlourStirringImages[^1].gameObject.SetActive(true);
                FlourStirringImages[previousIndex].gameObject.SetActive(false);
            }
        }

    }
}

