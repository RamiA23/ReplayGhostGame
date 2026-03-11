using UnityEngine;

/// <summary>
/// Attaches to a Light component and makes it flicker, simulating damaged/unstable
/// facility lighting. Used for environmental storytelling (Step 2: Light & Atmosphere).
/// </summary>
public class FlickerLight : MonoBehaviour
{
    public Light targetLight;

    [Header("Intensity Range")]
    public float minIntensity = 0f;
    public float maxIntensity = 1.5f;

    [Header("Timing")]
    public float minInterval = 0.02f;
    public float maxInterval = 0.15f;

    [Header("Occasional Full Blackout")]
    public bool enableBlackout = true;
    public float blackoutChance = 0.08f;   // probability per flicker tick
    public float blackoutDuration = 0.4f;

    private float timer;
    private bool inBlackout;
    private float blackoutTimer;

    void Start()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        timer = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        if (inBlackout)
        {
            blackoutTimer -= Time.deltaTime;
            if (blackoutTimer <= 0f)
                inBlackout = false;
            return;
        }

        timer -= Time.deltaTime;
        if (timer > 0f) return;

        if (enableBlackout && Random.value < blackoutChance)
        {
            targetLight.intensity = 0f;
            inBlackout = true;
            blackoutTimer = blackoutDuration;
        }
        else
        {
            targetLight.intensity = Random.Range(minIntensity, maxIntensity);
        }

        timer = Random.Range(minInterval, maxInterval);
    }
}
