using UnityEngine;

public class SineWaveAnimation : MonoBehaviour
{
    public enum OvershootDirection { None, Top, Bottom }

    public float frequency = 1f;        // Frequency of the sine wave
    public float amplitude = 1f;        // Amplitude of the sine wave
    public float overshootAmount = 0f;  // Amount of overshoot
    public OvershootDirection overshootDirection = OvershootDirection.None;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the sine value based on time
        float sineValue = Mathf.Sin(Time.time * frequency);

        // Apply overshoot if specified
        switch(overshootDirection)
        {
            case OvershootDirection.Top:
                sineValue += overshootAmount;
                break;
            case OvershootDirection.Bottom:
                sineValue -= overshootAmount;
                break;
            default:
                break;
        }

        // Apply the sine wave to the Y position of the transform
        Vector3 newPosition = startPosition + Vector3.up * sineValue * amplitude;
        transform.position = newPosition;
    }
}
