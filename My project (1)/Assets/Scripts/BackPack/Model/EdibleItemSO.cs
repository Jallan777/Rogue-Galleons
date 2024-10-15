using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    // ScriptableObject representing an edible item that can be consumed by the character
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        // List of modifiers that affect the character when the item is consumed
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();

        // Action name representing the item's action in the inventory (e.g., "Consume")
        public string ActionName => "Consume";

        // Sound effect to play when the item is consumed
        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        // Method that performs the item's action on the character
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            // Apply each modifier to the character
            foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true; // Return true to indicate the action was successfully performed
        }
    }

    // Interface for items that can be destroyed (e.g., consumed, dropped)
    public interface IDestroyableItem
    {

    }

    // Interface for items that can perform actions (e.g., use, consume)
    public interface IItemAction
    {
        // The name of the action to display in the UI (e.g., "Consume")
        public string ActionName { get; }

        // The sound effect to play when the action is performed
        public AudioClip actionSFX { get; }

        // Method to perform the item's action on the character
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }

    // Class to represent a modifier applied to a character's stats (e.g., health, stamina)
    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier; // The stat modifier to apply
        public float value; // The value by which to modify the stat
    
    
}
}
