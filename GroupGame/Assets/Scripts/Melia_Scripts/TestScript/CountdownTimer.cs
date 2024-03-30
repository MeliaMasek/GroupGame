using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f;

    [SerializeField] private Text countdownText;
    public float currentTime;

    void Start()
    {
        countdownText = GetComponent<Text>();
        currentTime = countdownTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 0)
        {
            Debug.Log("Countdown finished!");
        }
    }
}