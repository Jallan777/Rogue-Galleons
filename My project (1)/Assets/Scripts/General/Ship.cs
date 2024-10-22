using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{

    public UnityEvent<Ship> OnTakingDamage;

    [Header("Basic Parameters")]
    public float maxHealth;
    public float currentHealth;
    public ShipType shipType;

    [Header("Status")]
    public bool isAttacking;
    public CannonLauncher launcher;


    public GameObject success;

    public Text EnmHp;
    private void Awake() {
        this.currentHealth = this.maxHealth;
        this.launcher = this.GetComponentInChildren<CannonLauncher>();

    }

    public void takeDamage(Attack attack){
        this.currentHealth -= attack.attackDamage;
        EnmHp.text = this.currentHealth.ToString();
        print(currentHealth);
        if (currentHealth<=0) { success.SetActive(true); }
        OnTakingDamage.Invoke(this);
    }    

    private void Update() {
        if(isAttacking){
            launcher.isActive = true;
        }else{
            launcher.isActive = false;
        }
    }

    public void setAttackStat(){
        if(isAttacking)
            isAttacking = false;
        else
            isAttacking=true;    
    }

}
