using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShipStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float healAmount)
    {
        // DisplayPlayerHealth
        DisplayPlayerHealth playerHealth = character.GetComponent<DisplayPlayerHealth>();
        float cHealth = playerHealth.GetHealth();

            Debug.Log("Original Health Amount: " + cHealth);


        if (playerHealth != null)
        {
            float prechangeHealth = PlayerPrefs.GetFloat("PlayerHealth");
            // Increase the player's health by healAmount
            //float newHealth = playerHealth.CurrentHealth + healAmount;
            float newHealth = prechangeHealth + healAmount;

            Debug.Log("New Health Amount: " + newHealth);
            //Debug.Log("Healed by Amount: " + healAmount);
            
            
            //playerHealth.UpdateHealth(newHealth);
            //playerHealth.Start();
            playerHealth.ApplyHeal(healAmount);

            newHealth = PlayerPrefs.GetFloat("PlayerHealth");
            Debug.Log("New Health Amount: " + newHealth);

            Debug.Log("TEST CURRENT HEALTH: " + playerHealth.CurrentHealth);


            // Clamp health to not exceed the maximum (assuming 500 is max health)
            //playerHealth.CurrentHealth = Mathf.Clamp(newHealth, playerHealth.CurrentHealth, playerHealth.maxHealth);

            
            // Update the health bar display
            //playerHealth.UpdateHealthBar();

            
        }
    }
}

