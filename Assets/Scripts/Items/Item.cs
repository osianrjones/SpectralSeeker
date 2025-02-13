using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] public Sprite itemIcon;
    [SerializeField] private string itemName;

    public bool isActivated { get; set; }
    private bool isDroppable;

}
