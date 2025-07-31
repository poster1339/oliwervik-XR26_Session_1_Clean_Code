using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime;
    void Update()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            elapsedTime += Time.deltaTime;
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.FloorToInt(elapsedTime)}s";
    }
}