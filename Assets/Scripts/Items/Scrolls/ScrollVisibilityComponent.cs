using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class ScrollVisibilityComponent : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float maxVisibilityDistance = 10f;
    [SerializeField] private LayerMask playerLayer;
   

    private SpriteRenderer scrollRenderer;
    private FlashlightItemComponent torch;
    private Player player;

    void Start()
    {
        scrollRenderer = GetComponent<SpriteRenderer>();
        scrollRenderer.enabled = false;

        torch = playerObject.GetComponentInChildren<FlashlightItemComponent>();
        player = playerObject.GetComponent<Player>();
     
    }

    void Update()
    {
        if (!torch.isActivated)
        {
            scrollRenderer.enabled = false;
            return;
        }

        if (CheckVisibility())
        {
            scrollRenderer.enabled = true;
        } else
        {
            scrollRenderer.enabled = false;
        }
    }

    private bool CheckVisibility()
    {
         float distanceToTorch = Vector2.Distance(transform.position, playerObject.transform.position);
        if (distanceToTorch > maxVisibilityDistance)
        {
            return false;
        }

        Vector2 directionToScroll = (playerObject.transform.position - transform.position).normalized;

        float facingDirection = player.FacingDirection();

        //Check player is facing scroll
        if ((facingDirection == -1 && directionToScroll.x < 0) || (facingDirection == 1 && directionToScroll.x > 0))
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToScroll, maxVisibilityDistance, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "Player")
            {
                return true;
            }
        }

        return false;
    }
}