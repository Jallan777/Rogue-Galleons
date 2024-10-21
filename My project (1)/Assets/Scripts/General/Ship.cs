using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Audio;

public class Ship : MonoBehaviour
{

    public UnityEvent<Ship> OnTakingDamage;
    [Header("Shield UI and Audio")]
    public Image shieldIcon;
    public AudioClip shieldSound;
    private AudioSource audioSource;


    [Header("Basic Parameters")]
    public float maxHealth;
    public float currentHealth;
    public ShipType shipType;

    [Header("Status")]
    public bool isAttacking;
    public CannonLauncher launcher;

    [Header("Shield Parameters")]
    public float maxShield = 75f;
    public float currentShield;
    public float RechargeTime = 10f;
    private bool isRecharging = false;


    public GameObject success;

    public GameObject failure;
    private void Awake()
    {

        audioSource = gameObject.AddComponent<AudioSource>();

        if (shipType == ShipType.ENEMY)
        {
            isAttacking = true;
            this.currentHealth = this.maxHealth;
        }
        else
        {
          this.currentHealth = PlayerPrefs.GetFloat("PlayerHealth");
            ///this.currentHealth = 10;
        this.currentShield = maxShield;
        }

        this.launcher = this.GetComponentInChildren<CannonLauncher>();
    }

    public void takeDamage(Attack attack)
    {
        float damage = attack.attackDamage;

        // If there is shield available, absorb damage first
        if (isRecharging == false)
        {
            float shieldAbsorbed = Mathf.Min(currentShield, damage); // Absorb as much damage as the shield can
            currentShield -= shieldAbsorbed; // Reduce the shield amount
            damage -= shieldAbsorbed; // Subtract absorbed damage from the remaining damage

            // Update shield icon fill amount
            if (shieldIcon != null)
            {
                shieldIcon.fillAmount = currentShield / maxShield;

                // Hide the shield icon if the shield is completely depleted
                if (currentShield <= 0)
                {
                    shieldIcon.enabled = false;  // Hide the shield icon
                    StartCoroutine(RechargeShield());  // Start the recharge process
                }
            }
        }

        // Apply remaining damage to health if the shield is depleted
        if (damage > 0)
        {
            currentHealth -= damage;
            OnTakingDamage.Invoke(this);

            if (currentHealth <= 0)
            {if (shipType == ShipType.ENEMY)
                {
                    success.SetActive(true);
                }
                else if (shipType == ShipType.PLAYER)
                {
                    failure.SetActive(true);
                }
               

            }
        }
        else
        {
            
        }
    }



    private void Update()
    {
        if (isAttacking)
        {
            launcher.isActive = true;
        }
        else
        {
            launcher.isActive = false;
        }
    }

    public void setAttackStat()
    {
        if (isAttacking)
            isAttacking = false;
        else
            isAttacking = true;
    }


    private IEnumerator RechargeShield()
    {
        isRecharging = true;

        // Make sure the shield icon is visible when the recharge starts
        if (shieldIcon != null)
        {
            shieldIcon.enabled = true;
        }

        // Wait for the recharge time before starting to restore the shield
        yield return new WaitForSeconds(RechargeTime);

        float rechargeRate = maxShield / RechargeTime;  // Amount of shield to restore per second

        while (currentShield < maxShield)
        {
            currentShield += rechargeRate * Time.deltaTime;
            currentShield = Mathf.Min(currentShield, maxShield);  // Cap shield at its maximum value

            // Update shield icon fill amount based on current shield percentage
            if (shieldIcon != null)
            {
                shieldIcon.fillAmount = currentShield / maxShield;  // Set the fill amount to match shield percentage
            }

            yield return null;  // Wait for the next frame
        }

        // Play sound effect when shield is fully charged
        if (audioSource != null && shieldSound != null)
        {
            audioSource.PlayOneShot(shieldSound);
        }

        isRecharging = false;  // Recharge complete
    }

}
