using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public Image energyFill;

    public float maxEnergy = 100f;
    public float currentEnergy;

    public float replayCost = 20f;
    public float capsuleGain = 15f;

    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseReplay();
        }
    }

    public void UseReplay()
    {
        if (currentEnergy >= replayCost)
        {
            currentEnergy -= replayCost;
            UpdateUI();
        }
    }

    public void AddEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        UpdateUI();
    }

    void UpdateUI()
    {
        energyFill.fillAmount = currentEnergy / maxEnergy;
    }
}