using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousefollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas; // Reference to the canvas in which the object resides

    [SerializeField]
    private UIInventoryItem item; // Reference to the UIInventoryItem (representing the item to follow the mouse)

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        // Get the canvas component from the root of the transform hierarchy
        canvas = transform.root.GetComponent<Canvas>();
        // Get the UIInventoryItem component from the children of this GameObject
        item = GetComponentInChildren<UIInventoryItem>();
    }

    // Set the item's sprite and quantity to display as it follows the mouse
    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity); // Pass sprite and quantity to UIInventoryItem for display
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        // Convert the mouse position to local coordinates relative to the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
        );
        // Update the position of the object to follow the mouse
        transform.position = canvas.transform.TransformPoint(position);
    }

    // Toggles the visibility of the mouse follower object
    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}"); // Log the toggle action
        gameObject.SetActive(val); // Set the active state of the GameObject
    }




}
