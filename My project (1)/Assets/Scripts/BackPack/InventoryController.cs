using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Text;
=======
>>>>>>> testMain
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
<<<<<<< HEAD
        private UIInventoryPage inventoryUI; // Reference to the UI that manages inventory display

        [SerializeField]
        private InventorySO inventoryData; // Reference to the ScriptableObject holding inventory data

        //public int inventorySize = 10;

        public List<InventoryItem> InitialItems = new List<InventoryItem>(); // List to hold the initial items to populate the inventory

        [SerializeField]
        private AudioClip dropClip; // Audio clip to play when an item is dropped

        [SerializeField]
        private AudioSource audioSource; // Audio source to play sound effects

        // Start is called before the first frame update
        private void Start()
        {
            PrepareUI(); // Initialize the inventory UI
            PrepareInventoryData(); // Initialize and populate the inventory with initial data
        }

        // Initializes the inventory data and populates it with initial items
        private void PrepareInventoryData()
        {
            inventoryData.Initialize(); // Initialize the inventory data
            inventoryData.OnInventoryUpdated += UpdateInventoryUI; // Subscribe to the inventory update event
            foreach (InventoryItem item in InitialItems) // Add initial items to the inventory
            {
                if (item.IsEmpty)
                    continue; // Skip empty items
                inventoryData.AddItem(item); // Add item to the inventory
            }
        }

        // Updates the UI based on the current state of the inventory
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems(); // Clear the current UI items
            foreach (var item in inventoryState) // Update the UI with new inventory data
=======
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        //public int inventorySize = 10;

        public List<InventoryItem > InitialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in InitialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);

            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
>>>>>>> testMain
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

<<<<<<< HEAD
        // Prepares the UI, setting up event handlers for user interactions
        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size); // Initialize the inventory UI with the size of the inventory
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest; // Subscribe to description request event
            inventoryUI.OnSwapItems += HandleSwapItems; // Subscribe to item swap event
            inventoryUI.OnStartDragging += HandleDragging; // Subscribe to item dragging event
            inventoryUI.OnItemActionRequested += HandleItemActionRequest; // Subscribe to item action request event
        }

        // Handles the action request for a specific item in the inventory
        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex); // Get the item at the specified index
            if (inventoryItem.IsEmpty)
                return; // If the item is empty, return without performing any action

            // Check if the item has a custom action (implements IItemAction interface)
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex); // Show available actions for the item
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex)); // Add the custom action to the UI
            }

            // Check if the item is destroyable (implements IDestroyableItem interface)
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity)); // Add the 'Drop' action to the UI
            }
        }

        // Handles the action of dropping an item from the inventory
        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity); // Remove the specified quantity of the item from the inventory
            inventoryUI.ResetSelection(); // Reset the UI selection after the drop
            audioSource.PlayOneShot(dropClip); // Play the drop sound effect
        }

        // Performs the specified action on the item at the given index
        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex); // Get the item at the specified index
            if (inventoryItem.IsEmpty)
                return; // If the item is empty, return without performing any action

            // Check if the item is destroyable and remove it if so
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1); // Remove one instance of the destroyable item
            }

            // Check if the item has a custom action and perform it
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState); // Perform the item's custom action
                audioSource.PlayOneShot(itemAction.actionSFX); // Play the item's action sound effect
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection(); // Reset the UI selection if the item is now empty
            }
        }

        // Handles the dragging event for an item in the inventory
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex); // Get the item at the specified index
            if (inventoryItem.IsEmpty)
                return; // If the item is empty, return without performing any action
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity); // Create the dragged item in the UI
        }

        // Handles the swapping of two items in the inventory
        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItem(itemIndex_1, itemIndex_2); // Swap the two items in the inventory
        }

        // Handles the request for item description and updates the UI
        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex); // Get the item at the specified index
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection(); // Reset the selection if the item is empty
                return;
            }

            // Retrieve item information and update the description UI
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem); // Prepare the item's description
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description); // Update the description in the UI
        }

        // Prepares the description text for a given inventory item
        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder(); // Use StringBuilder to efficiently build the description string
            sb.Append(inventoryItem.item.Description); // Append the item's main description
            sb.AppendLine(); // Add a new line for readability
            for (int i = 0; i < inventoryItem.itemState.Count; i++) // Loop through the item parameters and append them
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                  $": {inventoryItem.itemState[i].value} / " +
                  $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine(); // Add a new line after each parameter
            }
            return sb.ToString(); // Return the complete description string
        }

        // Update is called once per frame to handle input
        public void Update()
        {
            // Check if the "I" key is pressed to toggle inventory visibility
=======
        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }


        private void HandleItemActionRequest(int itemIndex)
        {

        }



        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }



        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItem(itemIndex_1, itemIndex_2);
        }





        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);

        }





        public void Update()
        {
>>>>>>> testMain
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
<<<<<<< HEAD
                    inventoryUI.Show(); // Show the inventory UI if it is currently hidden
                    foreach (var item in inventoryData.GetCurrentInventoryState()) // Update the UI with the current inventory state
=======
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
>>>>>>> testMain
                    {
                        inventoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                }
                else
                {
<<<<<<< HEAD
                    inventoryUI.Hide(); // Hide the inventory UI if it is currently visible
                }
            }
        }
    }
}
=======
                    inventoryUI.Hide();
                }

            }
        }
    }
}
>>>>>>> testMain
