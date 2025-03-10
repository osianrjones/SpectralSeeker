using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class ScrollVisibilityComponent : MonoBehaviour
{
    [SerializeField] private GameObject playerTorch;
    [SerializeField] private float maxVisibilityDistance = 10f;
    [SerializeField] private LayerMask playerLayer;

    private SpriteRenderer scrollRenderer;
    private FlashlightItemComponent torch;
    private BoxCollider2D collider;
    private bool isVisible = false;

    void Start()
    {
        scrollRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        scrollRenderer.enabled = false;

        torch = playerTorch.GetComponent<FlashlightItemComponent>();
    }

    void Update()
    {
        if (!torch.isActivated)
        {
            return;
        }

        if (CheckVisibility())
        {
            scrollRenderer.enabled = true;
        }
    }

    private bool CheckVisibility()
    {
         float distanceToTorch = Vector2.Distance(transform.position, playerTorch.transform.position);
        if (distanceToTorch > maxVisibilityDistance)
        {
            return false;
        }

        Vector2 directionToScroll = (playerTorch.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToScroll, maxVisibilityDistance, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);
        }

        return hit.collider == null || hit.collider.transform == playerTorch;
    }
}