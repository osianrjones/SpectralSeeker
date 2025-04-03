using System;
using UnityEngine;

public class ItemPickupComponent : MonoBehaviour
{
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemName;
    [SerializeField] private float pickupCooldown;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip coinPickupSound;

    private bool canBePickedUp = false;

    public event Action ScrollPickedUp;
    public event Action<int> CoinPickedUp;

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
                if (itemName.Contains("Scroll"))
                {
                    ScrollPickedUp?.Invoke();

                } else if (itemName.Contains("Coin"))
                {
                    var coin = GetComponent<CoinItemComponent>();
                    int worth = coin.definedValue;
                    
                    var playerScore = collision.GetComponent<PlayerScoreComponent>();
                    playerScore.IncrementScore(worth);

                    ServiceLocator.Get<ISoundService>().PlaySFX(coinPickupSound);
                    coin.Destroy();
                }
                else
                {
                    ServiceLocator.Get<ISoundService>().PlaySFX(pickupSound);
                    inventory.AddItemToInventory(itemIcon, itemName);
                    gameObject.gameObject.SetActive(false);
                }
                   
            }
        }
    }
}
