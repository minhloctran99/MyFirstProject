using TMPro; // 1. Thêm namespace TMPro để sử dụng TextMeshProUGUI
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    // 2. Khai báo thêm các field tham chiếu
    [SerializeField]
    private TextMeshProUGUI finalScoreText;

    [SerializeField]
    private GameScore gameScore;

    private bool isGameOver;

    private void Start()
    {
        // Tự động tìm GameScore trên cùng GameObject nếu chưa kéo vào Inspector
        if (gameScore == null)
        {
            gameScore = GetComponent<GameScore>();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        // 3. Cập nhật Final Score trước khi bật Panel
        if (gameScore != null && finalScoreText != null)
        {
            finalScoreText.SetText(
            "Final Score: {0:0}",
            gameScore.Score
            );
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager
        .LoadScene(SceneManager
        .GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
