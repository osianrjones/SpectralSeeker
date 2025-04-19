using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPosition : MonoBehaviour
{
    [SerializeField] GameObject positionObject;

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
            //show green up arrow
        }
    }
}
