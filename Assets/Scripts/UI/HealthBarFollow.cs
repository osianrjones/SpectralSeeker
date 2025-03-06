using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // The entity the health bar follows
    [SerializeField] private Vector3 offset; // Offset to position the health bar above the entity

    private void Update()
    {
        if (target != null)
        {
            // Update the health bar's position to follow the entity
            transform.position = target.position + offset;
        }
    }
}