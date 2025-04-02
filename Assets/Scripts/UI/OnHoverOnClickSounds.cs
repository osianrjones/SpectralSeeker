using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverOnClickSounds : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    [Header("Settings")]
    [Range(0, 1)] public float hoverVolume = 0.8f;
    [Range(0, 1)] public float clickVolume = 1f;

    private AudioSource audioSource;
    private Button button;

    void Awake()
    {
        // Get or create AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // Set up click sound
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null && button.interactable)
        {
            audioSource.volume = hoverVolume;
            audioSource.PlayOneShot(hoverSound);
        }
    }

    private void OnButtonClick()
    {
        if (clickSound != null && button.interactable)
        {
            audioSource.volume = clickVolume;
            audioSource.PlayOneShot(clickSound);
        }
    }

    void OnDestroy()
    {
        // Clean up click listener
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}