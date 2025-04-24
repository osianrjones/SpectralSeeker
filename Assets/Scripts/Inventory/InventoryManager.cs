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

    private MonoBehaviour createBehaviourFromName(string name)
    {
        switch (name)
        {
            case "Flashlight":
                return GetComponentInChildren<FlashlightItemComponent>();
            case "Sword":
                return GetComponentInChildren<SwordItemComponent>();
            case "Heart":
                return GetComponentInChildren<HeartItemComponent>();
            default:
                return null;              
        }
    }

    private string createNameFromBehaviour(MonoBehaviour behaviour)
    {
        switch (behaviour)
        {
            case FlashlightItemComponent component:
                return "Flashlight";
            case SwordItemComponent component:
                return "Sword";
            case HeartItemComponent component:
                return "Heart";
            default:
                return null;
        }
    }

    public void inventoryPressed(int keyNum)
    {
        int index = --keyNum;
        MonoBehaviour component = inventoryItems[index];
        if (component == null) { return; }

        if (component is IItem item)
        {
            bool used = item.ToggleUse();
            if (used && component is HeartItemComponent)
            {
                RemoveItemFromSlot(index);       
            }
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
                MonoBehaviour component = createBehaviourFromName(itemName);
                inventoryItems[i] = component;
                SetImageOpacity(inventoryImageSlots[i]);
                break;
            }
        }
    }

    public bool RemoveItemFromSlot(int index)
    {
        if (index > 0 && index < inventoryImageSlots.Length)
        {
            SetImageClear(inventoryImageSlots[index]);
            inventoryImageSlots[index].sprite = null;
            inventoryItems[index] = null;
            return true;
        }

        return false;
    }

    public void throwItem()
    {
        if (lastPressedItem > 1)
        {
            MonoBehaviour component = inventoryItems[lastPressedItem];
            inventoryPressed(++lastPressedItem);

            string item = createNameFromBehaviour(component);
            if (item == null) { return; }

            if (RemoveItemFromSlot(lastPressedItem))
            {
                GameObject throwableObject = ObjectTracker.Instance.FindInactiveObjectByTag(item);

                float facingDirection = GetComponent<SpriteRenderer>().flipX ? -1 : 1;
                Vector2 spawnOffset = new Vector2(facingDirection * 1f, 0);
                Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;

                GameObject throwableItem = Instantiate(throwableObject, spawnPosition, Quaternion.identity);

                throwableItem.SetActive(true);

                Rigidbody2D rigidbody2D = throwableItem.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    float horizontalForce = facingDirection * 2f;

                    float verticalForce = 2f;

                    Vector2 throwForce = new Vector2(horizontalForce, verticalForce);

                    rigidbody2D.AddForce(throwForce, ForceMode2D.Impulse);
                }
            }
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

    private void SetImageClear(Image slot)
    {
        if (slot != null)
        {
            Color color = slot.color;
            color.a = 0f;
            slot.color = color;
        }
    }
}
