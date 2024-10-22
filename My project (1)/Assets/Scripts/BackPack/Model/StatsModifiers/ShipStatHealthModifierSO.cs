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

        if (playerHealth != null)
        {
            // Increase the player's health by healAmount
            float newHealth = playerHealth.CurrentHealth + healAmount;

            // Clamp health to not exceed the maximum (assuming 500 is max health)
            playerHealth.CurrentHealth = Mathf.Clamp(newHealth, 400, playerHealth.maxHealth);

            // Update the health bar display
            playerHealth.UpdateHealthBar();
        }
    }
}

