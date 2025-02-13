using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightItemComponent : MonoBehaviour
{

    private Light2D torchLight;
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float offset;

    public bool isActivated = false;

    private void Awake()
    {
        torchLight = GetComponent<Light2D>();
        torchLight.enabled = false;
    }


    private void Update()
    {
        if (isActivated)
        {
            float facingDirection = playerSprite.flipX ? -1 : 1;

            transform.position = player.position;

            var targetRotationZ = (facingDirection == 1) ? -90f : 90f;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetRotationZ);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void ToggleUse()
    {
        isActivated = !isActivated;
        torchLight.enabled = !torchLight.enabled;
        //Light2D playerLightComponent = transform.parent.Find("Player Light")?.GetComponent<Light2D>();
        //playerLightComponent.enabled = !playerLightComponent.enabled;
    }
}
