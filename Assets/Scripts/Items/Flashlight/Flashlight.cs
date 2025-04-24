using UnityEngine;

public class Flashlight : Item, IItem
{
    private FlashlightItemComponent _flashlightItem;
    private void Awake()
    {
        _flashlightItem = GetComponent<FlashlightItemComponent>(); 
    }

    public bool ToggleUse()
    {
       return _flashlightItem.ToggleUse();
    }

}
