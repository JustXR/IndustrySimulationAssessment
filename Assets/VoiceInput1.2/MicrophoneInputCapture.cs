using UnityEngine;

public class MicrophoneInputCapture : MonoBehaviour
{
    private AudioSource audioSource;
    private float[] spectrumData;
    private const int sampleSize = 512;

    [SerializeField] float sensitivityThreshold = 0.1f; // Adjust sensitivity threshold

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Create new AudioSource
        spectrumData = new float[sampleSize];

        audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        audioSource.loop = true;

        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
    }

    public float[] GetSpectrumData()
    {
        return spectrumData;
    }

    public bool IsBeatboxingOrWhispering()
    {
        float averageAmplitude = GetAverageAmplitude();
        return averageAmplitude < sensitivityThreshold; // Adjust threshold based on sensitivity
    }

    public float GetAverageAmplitude() // Changed to public
    {
        float sum = 0;
        foreach (var data in spectrumData)
        {
            sum += data;
        }
        return sum / sampleSize;
    }
}
