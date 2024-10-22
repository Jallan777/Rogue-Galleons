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

    public bool isBoss;

    public GameObject success;
    public GameObject failure;
    private string randDefeatDesc;
    private string randWinDesc;

    private List<string> defeatTexts = new List<string>()
    {
        "Yer ship has been lost to the cruel sea..",
        "The dream o' Treasure Island slips away...",
        "Ye couldn't best this foe, matey..."
    };

    private List<string> winTexts = new List<string>()
    {
        "Ye made it to the fabled Treasure Island!",
        "That mighty beast was no match for ye!",
        "The crew shout and cheer! Ye've finally made it!"
    };

    private void Awake()
    {
        Debug.Log("Is Boss: " + isBoss);
        
        string playerName = PlayerPrefs.GetString("PlayerName");

        randDefeatDesc = defeatTexts[Random.Range(0, defeatTexts.Count)];
        randDefeatDesc = "You Failed <b>Captain " + playerName.ToUpper() + "</b>\n\n" + randWinDesc + "\nReturn to Main Menu";
        
        randWinDesc = winTexts[Random.Range(0, winTexts.Count)];
        randWinDesc = "Well Done <b>Captain " + playerName.ToUpper() + "</b>!\n\n" + randWinDesc + "\nReturn to Main Menu";


        audioSource = gameObject.AddComponent<AudioSource>();

        if (OnTakingDamage == null)
        {
            OnTakingDamage = new UnityEvent<Ship>();
        }

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

            if (currentHealth <= 0)
            {
                if (shipType == ShipType.ENEMY)
                {
                    if(isBoss)
                    {
                        success.SetActive(true);
                        UpdateSuccessDescription();
                    }
                    
                }
                else if (shipType == ShipType.PLAYER)
                {

                    failure.SetActive(true);
                    UpdateFailureDescription();
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

    private void UpdateFailureDescription()
    {
        if (failure != null)
        {
            Transform defeatDescTrans = failure.transform.Find("DefeatDescription");

            if (defeatDescTrans != null)
            {
                Text defeatDescText = defeatDescTrans.GetComponent<Text>();

                if (defeatDescText != null)
                {
                    //randDefeatDesc = defeatTexts[Random.Range(0, defeatTexts.Count)];


                    defeatDescText.text = randDefeatDesc;
                }
                else
                {
                    Debug.LogError("DefeatDescription has no text component!");
                }
            }
            else
            {
                Debug.LogError("DefeatDescription GameObject not found!");

            }
        }
    }

    private void UpdateSuccessDescription()
    {
         if (success != null)
        {
            Transform successDescTrans = success.transform.Find("WinDescription");

            if (successDescTrans != null)
            {
                Text winDescText = successDescTrans.GetComponent<Text>();

                if (winDescText != null)
                {
                    //randDefeatDesc = defeatTexts[Random.Range(0, defeatTexts.Count)];


                    winDescText.text = randWinDesc;
                }
                else
                {
                    Debug.LogError("WinDescription has no text component!");
                }
            }
            else
            {
                Debug.LogError("WinDescription GameObject not found!");

            }
        }
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
