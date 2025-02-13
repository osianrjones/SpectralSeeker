using System;
using UnityEngine;

public class ItemPickupComponent : MonoBehaviour
{
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemName;

    public event Action<IItem> ItemPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager inventory = collision.GetComponent<InventoryManager>();

            if (inventory != null)
            {
               
                inventory.AddItemToInventory(itemIcon, itemName);

                Destroy(gameObject);
            }
        }
    }
}
