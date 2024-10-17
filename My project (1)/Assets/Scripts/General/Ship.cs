using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private void Awake() {

        if(shipType == ShipType.ENEMY)
        {
            isAttacking = true;
            this.currentHealth = this.maxHealth;
        }
        else
        {
            this.currentHealth = PlayerPrefs.GetFloat("PlayerHealth");

        }

        //this.currentHealth = this.maxHealth;
        this.launcher = this.GetComponentInChildren<CannonLauncher>();

    }

    public void takeDamage(Attack attack){
        this.currentHealth -= attack.attackDamage;
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
