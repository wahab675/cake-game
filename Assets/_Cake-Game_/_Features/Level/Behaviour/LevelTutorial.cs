using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTutorial : MonoBehaviour
{
    public GameObject Container;

    private void OnEnable()
    {
        EventManager.OnShowLevelTutorial += ShowTutorial;
        EventManager.OnSketchingStarted += StopTutorial;
    }

    private void OnDisable()
    {
        EventManager.OnShowLevelTutorial -= ShowTutorial;
        EventManager.OnSketchingStarted -= StopTutorial;
    }

    private void Start()
    {
        if(Profile.Level == 0)
            ShowTutorial();
        else
            StopTutorial();
    }

    private void ShowTutorial()
    {
        Container.SetActive(true);
    }

    private void StopTutorial()
    {
        Container.SetActive(false);
    }
}
