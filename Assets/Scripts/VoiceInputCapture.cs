using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceInputCapture : MonoBehaviour
{
    private AudioClip microphoneClip;
    private bool isRecording = false;
    public int sampleRate = 44100;
    public int lengthSec = 1;

    void Start()
    {
        StartMicrophone();
    }

    void StartMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneClip = Microphone.Start(null, true, lengthSec, sampleRate);
            isRecording = true;
        }
        else
        {
            Debug.LogWarning("No microphone found!");
        }
    }

    void OnDisable()
    {
        if (isRecording)
        {
            Microphone.End(null);
            isRecording = false;
        }
    }

    void Update()
    {
        if (isRecording)
        {
            float volume = GetMicrophoneVolume();
            Debug.Log("Volume: " + volume);
            // Use the volume to control your object shape here
        }
    }

    public float GetMicrophoneVolume()
    {
        int sampleCount = 256;
        float[] samples = new float[sampleCount];
        microphoneClip.GetData(samples, Microphone.GetPosition(null) - sampleCount);

        float sum = 0;
        foreach (float sample in samples)
        {
            sum += sample * sample;
        }

        return Mathf.Sqrt(sum / sampleCount);
    }
}
