using UnityEngine;

public class PlayerCameraComponent : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float aheadAmount = 2f;
    [SerializeField] private float smoothSpeed = 5f;

    private float lastPlayerX;
    private float direction = 1f;

    private void Start()
    {
        lastPlayerX = player.position.x;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        if (player.position.x > lastPlayerX)
            direction = 1f;
        else if (player.position.x < lastPlayerX)
            direction = -1f;

        lastPlayerX = player.position.x;

        // Target position: Follow player with an offset in the direction they are facing
        Vector3 targetPosition = new Vector3(player.position.x + (aheadAmount * direction), player.position.y, transform.position.z);

        // Smoothly move towards target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
