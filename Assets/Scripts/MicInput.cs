using UnityEngine;
using System.Collections;

[System.Serializable]
public class MicInput
{
    AudioClip m_audioClip;
    const int m_sampleRate = 8000;
    const int m_sampleRange = 800;
    [SerializeField] int m_audioDevice = 0;
    [SerializeField][Range(1f,50f)] float m_amplifier = 1.0f;
    [SerializeField][Range(0f,1f)] float m_noiseGate = 0.1f;
    [SerializeField][Range(0f, 1f)] float m_output = 0f;

    public void Start()
    {
        m_audioClip = Microphone.Start(Microphone.devices[m_audioDevice], true, 1, m_sampleRate);
    }

    public float OutputVolume()
    {
        // get sample array
        float[] samples = new float[m_sampleRate];
        m_audioClip.GetData(samples, 0);

        // get range of samples
        int index_end = Microphone.GetPosition(Microphone.devices[m_audioDevice]);
        int index_start = index_end - m_sampleRange;
        if (index_start < 0) index_start += samples.Length;

        // calculate output
        int i = index_start;
        //
        while (i != index_end)
        {
            m_output += Mathf.Abs(samples[i]);
            i++;
            if (i >= samples.Length) i = 0;
        }
        m_output /= m_sampleRange;
        m_output *= m_amplifier;

        // noise gate
        if (m_output < m_noiseGate) m_output = 0.0f;

        // limiter
        if (m_output > 1f) m_output = 1f;

        return m_output;
    }


    public void SetAmp(float value) { m_amplifier = value; }
    public void SetGate(float value) { m_noiseGate = value; }
    public void SetDevice(int deviceNumber) { m_audioDevice = deviceNumber; }
    public int GetDeviceIndex() { return m_audioDevice; }


    public void ShowDevicesOnDebug()
    {
        Debug.Log("Available audio devices:\n");
        int nr = 0;
        foreach (string deviceName in Microphone.devices)
        {
            Debug.Log(deviceName + " > [" + nr + "]\n");
        }
    }
}
