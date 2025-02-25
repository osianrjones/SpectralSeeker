using System;
using UnityEngine;

public class ItemPickupComponent : MonoBehaviour
{
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemName;
    [SerializeField] private float pickupCooldown;
    private bool canBePickedUp = false;

    public event Action<IItem> ItemPickup;

    private void Start()
    {
        ObjectTracker.Instance.RegisterObject(gameObject, itemName);
        Invoke(nameof(EnablePickup), pickupCooldown);
    }

    private void EnablePickup()
    {
        canBePickedUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager inventory = collision.GetComponent<InventoryManager>();

            if (inventory != null && canBePickedUp)
            {               
                inventory.AddItemToInventory(itemIcon, itemName);

                gameObject.gameObject.SetActive(false);
            }
        }
    }
}
