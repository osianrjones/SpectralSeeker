using System;
using UnityEngine;

public class SnakeCollisionComponent : MonoBehaviour
{
    [SerializeField] private float distance = 10f;
    [SerializeField] private GameObject player;

    private Transform playerTransform;
    private bool attacked = false;

    public Action<Transform> Attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerWithinDistance() && !attacked)
        {
            Debug.Log("Attack");
            attacked = true;
            AttackPlayer();
        }
    }

    public bool IsPlayerWithinDistance()
    {
        if (player == null) return false;

        float horizontalDistance = Mathf.Abs(playerTransform.position.x - transform.position.x);

        return horizontalDistance <= distance;
    }

    public void AttackPlayer()
    {
        Attack?.Invoke(playerTransform.transform);
    }

    private void OnDrawGizmosSelected()
    {
        if (enabled)
        {
            Gizmos.color = Color.yellow;
            Vector3 position = transform.position;

            // Draw horizontal line showing check distance
            Gizmos.DrawLine(
                new Vector3(position.x - distance, position.y, 0),
                new Vector3(position.x + distance, position.y, 0)
            );
        }
    }
}
