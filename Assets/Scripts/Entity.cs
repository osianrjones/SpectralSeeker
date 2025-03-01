using UnityEngine;

public interface IEntity
{
    Color DamageColor();

    void ShowDamage(float damage, Color damageColor);
}
