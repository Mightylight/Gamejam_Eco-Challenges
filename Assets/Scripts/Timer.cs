using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public Slider timerSlider;
    public float initialGameTime; // Initial time for the countdown
    [SerializeField] private float gameTimeRemaining; // Remaining time for the countdown
    public Color startColor = Color.green;
    public Color middleColor = Color.yellow;
    public Color endColor = Color.red;

    private bool stopTimer;
    private Image sliderFillImage;

    void Start()
    {
        stopTimer = false;
        gameTimeRemaining = initialGameTime; // Set remaining time to initial game time
        timerSlider.maxValue = initialGameTime;
        timerSlider.value = initialGameTime;
        sliderFillImage = timerSlider.fillRect.GetComponent<Image>(); // Get the fill area of the slider
    }

    void Update()
    {
        if (!stopTimer)
        {
            gameTimeRemaining -= Time.deltaTime; // Reduce remaining time
            timerSlider.value = gameTimeRemaining;

            // Calculate percentage of time elapsed
            float percentRemaining = gameTimeRemaining / initialGameTime;

            // Change color based on percentage remaining
            if (percentRemaining <= 0.8f)
            {
                sliderFillImage.color = Color.Lerp(startColor, middleColor, (0.6f - percentRemaining) * 2f);
            }
            if (percentRemaining <= 0.5f)
            {
                sliderFillImage.color = Color.Lerp(middleColor, endColor, (0.4f - percentRemaining) * 5f);
            }

            // Check if time is up
            if (gameTimeRemaining <= 0)
            {
                stopTimer = true;
                HighscoreCounter.Instance.GameOver();
            }
        }
    }
}