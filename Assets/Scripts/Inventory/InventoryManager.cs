using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Image[] inventoryImageSlots;
    private MonoBehaviour[] inventoryItems;
    private int lastPressedItem = 0;

    private void Start()
    {
        inventoryImageSlots = new Image[5];
        inventoryItems = new MonoBehaviour[5];
        for (int i = 0; i < inventoryImageSlots.Length; i++)
        {
            inventoryImageSlots[i] = GameObject.Find($"itemSlot{i + 1}")?.GetComponent<Image>();
            if (inventoryImageSlots[i] == null)
            {
                Debug.LogError($"itemSlot{i + 1} not found or missing Image component!");
            }
           
        }
        //Add the player's journal.
        inventoryItems[0] = GetComponent<JournalManagerComponent>();
    }

    private MonoBehaviour createItemObject(string name)
    {
        switch (name)
        {
            case "Flashlight":
                return GetComponentInChildren<FlashlightItemComponent>();
            case "Sword":
                return GetComponentInChildren<SwordItemComponent>();
            default:
                return null;              
        }
    }

    public void inventoryPressed(int keyNum)
    {
        int index = --keyNum;
        MonoBehaviour component = inventoryItems[index];
        if (component == null) { return; }

        if (component is  FlashlightItemComponent flashlight)
        {
            flashlight.ToggleUse();
        } else if (component is JournalManagerComponent journal)
        {
            journal.ToggleUse();
        } else if (component is SwordItemComponent sword)
        {
            sword.ToggleUse();
        }
        
        lastPressedItem = index;
    }

    public void AddItemToInventory(Sprite sprite, String itemName)
    {
        Debug.Log(inventoryImageSlots);
        for (int i = 0;i < inventoryImageSlots.Length;i++)
        {
            if (inventoryImageSlots[i].sprite == null)
            {
                inventoryImageSlots[i].sprite = sprite;
                MonoBehaviour component = createItemObject(itemName);
                inventoryItems[i] = component;
                SetImageOpacity(inventoryImageSlots[i]);
                break;
            }
        }
    }

    public void RemoveItemFromSlot(int index)
    {
        if (index > 1 && index < inventoryImageSlots.Length)
        {
            inventoryImageSlots[index].sprite = null;
            inventoryItems[index] = null;
        }
    }

    private void SetImageOpacity(Image slot)
    {
        if (slot != null)
        {
            Color color = slot.color;
            color.a = 255f;
            slot.color = color;
        }
    }
}
