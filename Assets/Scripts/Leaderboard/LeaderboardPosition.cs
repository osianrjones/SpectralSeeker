using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPosition : MonoBehaviour
{
    [SerializeField] GameObject positionObject;
    [SerializeField] GameObject upArrow;
    [SerializeField] private AudioClip positionUpSound;

    private Text positionText;
    private int position;
    private bool initialize = true;

    private void Awake()
    {
        positionText = positionObject.GetComponent<Text>();
        if (positionText == null)
        {
            Debug.LogError("Position Text component not found on the assigned GameObject.");
        }
        upArrow.SetActive(false);
    }

    void Start()
    {
        position = Leaderboard.Instance.playerPosition(Leaderboard.activeUser);
        positionText.text = position.ToString();

    }

    void Update()
    {
       int newPosition = Leaderboard.Instance.playerPosition(Leaderboard.activeUser);

        if (position != newPosition && !initialize)
        {
            position = newPosition;
            positionText.text = position.ToString();
            upArrow.SetActive(true);
            ServiceLocator.Get<ISoundService>().PlaySFX(positionUpSound);
            StartCoroutine(MoveUpArrow());
        } else if (position != newPosition && initialize)
        {
            initialize = false;
            position = newPosition;
            positionText.text = position.ToString();
        }
    }

  
    private IEnumerator MoveUpArrow()
        {
            Vector3 originalPosition = upArrow.transform.position;
            Vector3 targetPosition = originalPosition + new Vector3(0, 2f, 0);
            float duration = 3f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                upArrow.transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            upArrow.SetActive(false);
            upArrow.transform.position = originalPosition;
    }
}
