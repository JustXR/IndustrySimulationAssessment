using UnityEngine;

public class VoiceControlledShape : MonoBehaviour
{
    public VoiceInputCapture voiceInputCapture;
    private MeshFilter meshFilter;
    private Renderer objectRenderer;
    private Vector3[] originalVertices;
    private MaterialPropertyBlock propertyBlock;

    private Color[] colors;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        objectRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();

        if (meshFilter != null)
        {
            originalVertices = meshFilter.mesh.vertices;
        }
        else
        {
            Debug.LogWarning("No MeshFilter found on this GameObject.");
        }

        if (objectRenderer == null)
        {
            Debug.LogWarning("No Renderer found on this GameObject.");
        }

        // Define the colors to transition through
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
        if (voiceInputCapture != null && meshFilter != null && objectRenderer != null)
        {
            float volume = voiceInputCapture.GetMicrophoneVolume();
            Debug.Log("Volume: " + volume);  // Log volume for debugging
            ModifyShapeAndColor(volume);
        }
    }

    void ModifyShapeAndColor(float volume)
    {
        float amplificationFactor = 100.0f; // Increase to make changes more visible

        Debug.Log("Modifying shape and color with volume: " + volume);

        // Modify shape
        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;

        // Calculate deformation based on volume
        float deformationFactor = 1.0f + volume * 0.5f; // Adjust the 0.5f factor as needed

        // Apply deformation to vertices to change from sphere to oval shape
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].x *= deformationFactor; // Deform the x-axis
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Modify color
        float scaledVolume = Mathf.Clamp01(volume * 10); // Scale volume for better visibility
        Color newColor = GetColorFromVolume(scaledVolume);
        Debug.Log("Changing color to: " + newColor);

        objectRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_Color", newColor);
        objectRenderer.SetPropertyBlock(propertyBlock);
    }


    Color GetColorFromVolume(float volume)
    {
        if (colors.Length == 0)
            return Color.white;

        float scaledVolume = volume * (colors.Length - 1);
        int index = Mathf.FloorToInt(scaledVolume);
        float t = scaledVolume - index;

        if (index >= colors.Length - 1)
            return colors[colors.Length - 1];

        return Color.Lerp(colors[index], colors[index + 1], t);
    }
}
