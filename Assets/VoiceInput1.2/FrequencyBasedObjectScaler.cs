using System.Collections;
using UnityEngine;

public class FrequencyBasedObjectScaler : MonoBehaviour
{
    public MicrophoneInputCapture microphoneInputCapture;
    public Transform objectToScale;

    [SerializeField] float minScale = 0.5f;
    [SerializeField] float maxScale = 2f;
    [SerializeField] float scalingFactor = 1f; // Adjust scaling factor for more sensitivity
    [SerializeField] float changeProbability = 0.5f; // Probability of changing scale
    [SerializeField] float delayBeforeStart = 5f; // Delay in seconds before starting the scaling

    private Vector3 baseScale; // Store the initial scale of the object

    void Start()
    {
        // Initialize the base scale with the initial scale of the object
        baseScale = objectToScale.localScale;

        // Deactivate the object initially
        objectToScale.gameObject.SetActive(false);

        // Start the coroutine to delay activation and scaling
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeStart);

        // Activate the object after the delay
        objectToScale.gameObject.SetActive(true);
    }

    void Update()
    {
        if (microphoneInputCapture != null && objectToScale.gameObject.activeSelf)
        {
            ScaleObjectBasedOnMicrophoneInput();
        }
    }

    void ScaleObjectBasedOnMicrophoneInput()
    {
        float averageAmplitude = microphoneInputCapture.GetAverageAmplitude();
        float scaleChange = averageAmplitude * scalingFactor; // Calculate scale change

        // Randomly decide to increase or decrease the scale
        if (Random.value < changeProbability)
        {
            scaleChange = -scaleChange;
        }

        // Update the scale based on the current scale
        Vector3 newScale = objectToScale.localScale + new Vector3(scaleChange, scaleChange, scaleChange);

        // Ensure the scale stays within the defined min and max scales
        newScale.x = Mathf.Clamp(newScale.x, baseScale.x * minScale, baseScale.x * maxScale);
        newScale.y = Mathf.Clamp(newScale.y, baseScale.y * minScale, baseScale.y * maxScale);
        newScale.z = Mathf.Clamp(newScale.z, baseScale.z * minScale, baseScale.z * maxScale);

        // Apply the new scale to the object
        objectToScale.localScale = newScale;
    }
}
