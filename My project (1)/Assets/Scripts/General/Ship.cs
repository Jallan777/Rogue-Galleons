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
        this.currentShield = maxShield;
        }

        this.launcher = this.GetComponentInChildren<CannonLauncher>();
    }

    public void takeDamage(Attack attack)
    {
        float damage = attack.attackDamage;

      
        if (isRecharging == false)
        {
            float shieldAbsorbed = Mathf.Min(currentShield, damage); 
            currentShield -= shieldAbsorbed;
            damage -= shieldAbsorbed; 

      
            if (shieldIcon != null)
            {
                shieldIcon.fillAmount = currentShield / maxShield;

               
                if (currentShield <= 0)
                {
                    shieldIcon.enabled = false; 
                    StartCoroutine(RechargeShield()); 
                }
            }
        }


        if (damage > 0)
        {
            currentHealth -= damage;
            OnTakingDamage.Invoke(this);
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

        
        {
            shieldIcon.enabled = true;
        }


        yield return new WaitForSeconds(RechargeTime);

        float rechargeRate = maxShield / RechargeTime;  

        while (currentShield < maxShield)
        {
            currentShield += rechargeRate * Time.deltaTime;
            currentShield = Mathf.Min(currentShield, maxShield);  

            
            if (shieldIcon != null)
            {
                shieldIcon.fillAmount = currentShield / maxShield;  
            }

            yield return null; 
        }

      
        if (audioSource != null && shieldSound != null)
        {
            audioSource.PlayOneShot(shieldSound);
        }

        isRecharging = false;  
    }

}
