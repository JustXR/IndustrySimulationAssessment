using UnityEngine;

public class VoiceControlledParticleSystem : MonoBehaviour
{
    public VoiceInputCapture voiceInputCapture;
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.ShapeModule shapeModule;

    private Color[] colors;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        mainModule = particleSystem.main;
        shapeModule = particleSystem.shape;

        // Define the colors to choose from
        colors = new Color[]
        {
            Color.blue,
            Color.cyan,
            Color.green,
            Color.yellow,
            Color.red
        };
    }

    void Update()
    {
        if (voiceInputCapture != null && particleSystem != null)
        {
            float volume = voiceInputCapture.GetMicrophoneVolume();
            Debug.Log("Volume: " + volume);  // Log volume for debugging
            ModifyParticleSystem(volume);
        }
    }

    void ModifyParticleSystem(float volume)
    {
        float scaledVolume = Mathf.Clamp01(volume * 10); // Scale volume for better visibility

        // Randomly select a color from the array
        Color randomColor = GetRandomColor();

        // Interpolate between the random color and white based on volume
        Color newColor = Color.Lerp(Color.white, randomColor, scaledVolume);
        Debug.Log("Changing color to: " + newColor);

        mainModule.startColor = newColor;

        // Modify shape based on volume
        shapeModule.radius = Mathf.Lerp(1f, 5f, scaledVolume); // Example: Change radius from 1 to 5 based on volume
    }

    Color GetRandomColor()
    {
        if (colors.Length == 0)
            return Color.white;

        int randomIndex = Random.Range(0, colors.Length);
        return colors[randomIndex];
    }
}
