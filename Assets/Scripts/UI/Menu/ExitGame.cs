using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Finish()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {

            gameObject.SetActive(true);

            int score = collision.GetComponentInChildren<PlayerScoreComponent>().score;
        }
    }
}
