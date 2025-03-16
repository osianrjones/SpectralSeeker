using System;
using UnityEngine;

public class CoinItemComponent : MonoBehaviour
{
    public int definedValue;

    //destroy coin
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public int getDefinedValue()
    {
        return definedValue;
    }
}
