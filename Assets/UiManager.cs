using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject gameOverPanel;
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    public void UpdateScore(int score)
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }
    public void UpdateHealth(float health)
    {
        if (healthBar != null) healthBar.value = health;
    }
    public void ShowGameOver()
    {
        if (statusText != null) statusText.text = "GAME OVER!";
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
    public void ShowWin(int score)
    {
        if (statusText != null) statusText.text = $"YOU WIN! Score: {score}";
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}