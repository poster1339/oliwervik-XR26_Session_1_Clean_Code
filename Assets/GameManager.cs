using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    public void GameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        UIManager.Instance.ShowGameOver();
        Invoke(nameof(RestartGame), 2f);
    }
    public void WinGame(int score)
    {
        if (IsGameOver) return;
        IsGameOver = true;
        UIManager.Instance.ShowWin(score);
        Invoke(nameof(RestartGame), 2f);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}