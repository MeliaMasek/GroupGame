using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f;

    [SerializeField] private Text countdownText;
    public float currentTime;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject ShopUIMenu;
    [SerializeField] private GameObject CraftingUIMenu;



    void Start()
    {
        countdownText = GetComponent<Text>();
        currentTime = countdownTime;
        endGameUI.SetActive(false);
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 0)
        {
            currentTime = 0; // Ensure timer doesn't display negative values
            countdownText.text = "00:00"; // Update UI to display 00:00
            Debug.Log("Countdown finished!");
            TriggerEndGameUI(); // Call function to activate end game UI
        }
    }
    
    void TriggerEndGameUI()
    {
        endGameUI.SetActive(true);
        ShopUIMenu.SetActive(false);
        CraftingUIMenu.SetActive(false);
    }
    
    public void ResetTimer()
    {
        currentTime = countdownTime;
    }
}