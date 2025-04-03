using UnityEngine;

public class ServiceRegister : MonoBehaviour
{

    [SerializeField] private SoundManager _audioSFXService;
    void Start()
    {
        ServiceLocator.Register<ISoundService>(_audioSFXService);
    }

}
