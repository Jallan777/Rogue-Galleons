using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TakeDamageEventSO takeDamageEvent;
    public StatusBar playerBar;
    public StatusBar enemyBar;

    private void OnEnable() {
      takeDamageEvent.shipTakingDamage  += ChangeHealth;
    }


    private void OnDisable() {
      takeDamageEvent.shipTakingDamage -= ChangeHealth;
    }
 
 
    private void ChangeHealth(Ship ship)
    {
      if(ship.shipType == ShipType.PLAYER)
        playerBar.onHealthChange(ship.currentHealth / ship.maxHealth);
      else{
        enemyBar.onHealthChange(ship.currentHealth / ship.maxHealth);
      }
    }
}
