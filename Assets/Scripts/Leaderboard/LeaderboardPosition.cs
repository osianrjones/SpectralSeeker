using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPosition : MonoBehaviour
{
    [SerializeField] GameObject positionObject;
    [SerializeField] GameObject upArrow;

    private Text positionText;
    private int position;

    private void Awake()
    {
        positionText = positionObject.GetComponent<Text>();
        if (positionText == null)
        {
            Debug.LogError("Position Text component not found on the assigned GameObject.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = Leaderboard.Instance.playerPosition(Leaderboard.activeUser);
        positionText.text = position.ToString();

    }

    void Update()
    {
       int newPosition = Leaderboard.Instance.playerPosition(Leaderboard.activeUser);

        if (position != newPosition)
        {
            position = newPosition;
            positionText.text = position.ToString();
            upArrow.SetActive(true);
            StartCoroutine(MoveUpArrow());
        }
    }

  
    private IEnumerator MoveUpArrow()
    {
        Vector3 originalPosition = upArrow.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, 10, 0);
        float duration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            upArrow.transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        upArrow.SetActive(false);
    }
}
