using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdjuster : MonoBehaviour
{
    public float sizeInMeters;
    public float MinOrthoSize = 11.3f;

    void Start()
    {
        float orthoSize = sizeInMeters * Screen.height / Screen.width * 0.5f;

        if(orthoSize < MinOrthoSize)
            orthoSize = MinOrthoSize;

        GetComponent<Camera>().orthographicSize = orthoSize;
    }
}
