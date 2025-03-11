using UnityEngine;

public class ParrallaxComponent : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallaxFactor = 0.5f;
    
    private Vector3 startPosition;
    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        startPosition = transform.position;
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        Vector3 position = cameraTransform.transform.position;
        float distance = position.x * parallaxFactor;
        transform.position = new Vector3(startPosition.x + distance, transform.position.y, transform.position.z);
        
    }
}
