using UnityEngine;

public class VoiceInputHandler : MonoBehaviour
{
    private string _microphoneDevice;
    private AudioClip _microphoneClip;

    public ParticleSystem particleSystem;

    void Start()
    {
        _microphoneDevice = Microphone.devices[0];
        _microphoneClip = Microphone.Start(_microphoneDevice, true, 999, 44100);
    }

    void Update()
    {
        ProcessVoiceInput();
    }

    void ProcessVoiceInput()
    {
        int sampleSize = 128;
        float[] samples = new float[sampleSize];
        int position = Microphone.GetPosition(_microphoneDevice) - sampleSize + 1;
        if (position < 0) return;

        _microphoneClip.GetData(samples, position);

        float volume = 0f;
        foreach (var sample in samples)
        {
            volume += Mathf.Abs(sample);
        }

        volume /= sampleSize;

        if (volume > 0.01f) // Adjust the threshold as needed
        {
            Debug.Log("Voice input detected! Volume: " + volume);
            TriggerVFX(volume); // Call the method to trigger the VFX with the volume parameter
        }
    }

    void TriggerVFX(float volume)
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play(); // Start playing the Particle System VFX
        }

        // Define the initial and final scale values for sphere and ellipsoid
        Vector3 sphereScale = new Vector3(1f, 1f, 1f);
        Vector3 ellipsoidScale = new Vector3(10f, 10f, 20f); // Example ellipsoid scale (adjust as needed)

        // Interpolate between sphere and ellipsoid scale based on volume
        Vector3 newScale = Vector3.Lerp(sphereScale, ellipsoidScale, volume);

        // Set the new scale to the Particle System's shape module
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.scale = newScale;

        // Adjust the emission rate based on volume
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = volume * 100f; 

        Debug.Log("New scale: " + newScale + ", Emission rate: " + emissionModule.rateOverTime);
    }
}
