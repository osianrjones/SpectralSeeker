using UnityEngine;

public class BatSoundComponent : MonoBehaviour
{
    public static BatSoundComponent Instance { get; set; }

    [SerializeField] private AudioClip fly;

    [SerializeField] private AudioClip attack;

    [SerializeField] private AudioClip hurt;

    private float lastPlayTime;
    public float minDelayBetweenSFX = 25f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }    
    }

    public void PlaySound(BatSounds sound)
    {
        switch (sound)
        {
            case BatSounds.Fly:
                if (Time.time < lastPlayTime + minDelayBetweenSFX)
                    return;
                ServiceLocator.Get<ISoundService>().PlaySFX(fly);
                lastPlayTime = Time.time;
                break;
            case BatSounds.Attack:
                ServiceLocator.Get<ISoundService>().PlaySFX(attack);
                break;
            case BatSounds.Hurt:
                ServiceLocator.Get<ISoundService>().PlaySFX(hurt);
                break;
            default:
                break;
        }
    }
}
