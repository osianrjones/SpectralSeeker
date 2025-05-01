using Unity.VisualScripting;
using UnityEngine;

public class SnakeSoundComponent : MonoBehaviour
{
    [SerializeField] AudioClip snakeAttackOne;
    [SerializeField] AudioClip snakeAttackTwo;
    [SerializeField] AudioClip snakeDeath;
    [SerializeField] AudioClip snakeHit;

    public void PlayAttackSound()
    {
        AudioClip clipToPlay = Random.Range(0, 2) == 0 ? snakeAttackOne : snakeAttackTwo;
        ServiceLocator.Get<ISoundService>().PlaySFX(clipToPlay);
    }
    
    public void Hit()
    {
        ServiceLocator.Get<ISoundService>().PlaySFX(snakeHit);
    }

    public void PlayDeathSound()
    {
        ServiceLocator.Get<ISoundService>().PlaySFX(snakeDeath);
    }
}
