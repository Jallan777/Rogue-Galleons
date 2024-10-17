using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }
<<<<<<< HEAD
        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
=======
        public int AddItem(ItemSO item, int quantity)
>>>>>>> testMain
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                   

                    while (quantity > 0 && IsInventoryFull() == false) 
                    {
<<<<<<< HEAD
                       quantity -=  AddItemToFirstFreeSlot(item, 1,itemState);
=======
                       quantity -=  AddItemToFirstFreeSlot(item, 1);
>>>>>>> testMain
                      

                    }
                    InformAboutChange();    
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;

    }

<<<<<<< HEAD
        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
=======
        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
>>>>>>> testMain
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
<<<<<<< HEAD
                quantity = quantity,itemState = new List<ItemParameter> (itemState == null ? item.DefaultParametersList : itemState)
=======
                quantity = quantity
>>>>>>> testMain
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) 
                { 
                inventoryItems[i] = newItem;
                    return quantity;
                
                }
            
            }
            return 0;
        }

        private bool IsInventoryFull()
         => inventoryItems.Where(item => item.IsEmpty).Any() == false;


        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake =
                        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;

        }


            public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }


        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

<<<<<<< HEAD
  public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;

                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex]
                        .ChangeQuantity(reminder);

                InformAboutChange();
            }
        }
=======

>>>>>>> testMain

        public void SwapItem(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
<<<<<<< HEAD

      
=======
>>>>>>> testMain
    }





    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
<<<<<<< HEAD
         public List<ItemParameter> itemState;
=======
        // public List<ItemParameter> itemState;
>>>>>>> testMain
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
<<<<<<< HEAD
                 itemState = new List<ItemParameter>(this.itemState)
=======
                //  itemState = new List<ItemParameter>(this.itemState)
>>>>>>> testMain
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
<<<<<<< HEAD
                itemState = new List<ItemParameter>()
=======
                // itemState = new List<ItemParameter>()
>>>>>>> testMain
            };
    }

}