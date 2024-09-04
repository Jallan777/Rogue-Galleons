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



    private void Awake() {
        this.currentHealth = this.maxHealth;
    }

    public void takeDamage(Attack attack){
        this.currentHealth -= attack.attackDamage;
        OnTakingDamage.Invoke(this);
    }

    private void Update() {
        
    }

}
