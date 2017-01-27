using UMA.PoseTools;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GuopinSpeak_LipSync : MonoBehaviour
{
    public AudioSource Audio;
    private UMAExpressionPlayer Expression;

    private float[] fData; // the collection of voice frequency data
    private float maxFrequency; // the highest voice frequency

    [Range(64, 8192)]
    public int nSample = 256;

    [Range(200, 20000)]
    public int lowFrequency = 300;

    [Range(200, 20000)]
    public int highFrequency = 800;

    public float mouthDamper = 0.7f;

    // Use this for initialization
    void Start()
    {
        fData = new float[nSample];
        Expression = GetComponent<UMAExpressionPlayer>();

        Audio = GetComponent<AudioSource>();
        Audio.Play();
        maxFrequency = AudioSettings.outputSampleRate / 2;

    }

    // Update is called once per frame
    void Update()
    {
        float value = BandVolume();
        float multiplier = 100;
        value = Mathf.Clamp01(value * multiplier);
        float normalValue = (value * 2 - 1) * mouthDamper;
        Debug.Log(normalValue);

        MouthMovement(normalValue);
    }


    /// <summary>
    /// Control the mouth movement according to the settled value
    /// </summary>
    /// <param name="value"></param>
    private void MouthMovement(float value)
    {
        Expression.jawOpen_Close = value;
        Expression.tongueUp_Down = UnityEngine.Random.Range(-0.8f, 0.8f);

    }

    /// <summary>
    /// Generate a series of voice sampling data according to our requirement
    /// </summary>
    /// <returns>Average voice frequency</returns>
    private float BandVolume()
    {
        Audio.GetSpectrumData(fData, 0, FFTWindow.BlackmanHarris);
        int n1 = Mathf.FloorToInt(lowFrequency * nSample / maxFrequency);
        int n2 = Mathf.FloorToInt(highFrequency * nSample / maxFrequency);

        float sum = 0.0f;

        for (int i = n1; i < n2; i++)
        {
            sum += fData[i];
        }

        return sum / (n2 - n1 + 1);
    }
}
