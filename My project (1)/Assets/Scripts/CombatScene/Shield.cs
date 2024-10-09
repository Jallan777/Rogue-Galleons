using UnityEngine;
using UnityEngine.UI; // Optional, if you want to show shield status on the UI

public class ShieldSystem : MonoBehaviour
{
    public int maxShieldPoints = 200;    // Maximum shield capacity
    private int currentShieldPoints;     // Current shield strength
    public float rechargeTime = 20f;     // Time in seconds to fully recharge
    private float rechargeRate;          // How much to recharge per second
    private bool isRecharging = false;   // Flag to check if the shield is recharging

    void Start()
    {
        currentShieldPoints = maxShieldPoints;
        rechargeRate = maxShieldPoints / rechargeTime;
    }

    void Update()
    {
        // If the shield is not full, start recharging
        if (currentShieldPoints < maxShieldPoints && !isRecharging)
        {
            StartCoroutine(RechargeShield());
        }
    }

    public void TakeDamage(int damage)
    {
        // Subtract damage from the shield
        currentShieldPoints -= damage;
        if (currentShieldPoints < 0)
        {
            currentShieldPoints = 0; // Shield is depleted
        }

        // Stop recharging if taking damage
        StopAllCoroutines();
        isRecharging = false;
    }

    private IEnumerator RechargeShield()
    {
        isRecharging = true;

        // Gradually recharge the shield over time
        while (currentShieldPoints < maxShieldPoints)
        {
            currentShieldPoints += Mathf.CeilToInt(rechargeRate * Time.deltaTime);
            if (currentShieldPoints > maxShieldPoints)
            {
                currentShieldPoints = maxShieldPoints;
            }
            yield return null; // Wait for the next frame
        }

        isRecharging = false;
    }

    public int GetCurrentShieldPoints()
    {
        return currentShieldPoints;
    }
}
