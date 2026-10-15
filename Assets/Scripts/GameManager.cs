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
        if (finalScoreText != null && gameScore != null)
        {
            finalScoreText.text = "Final Score: " + gameScore.Score;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager
        .LoadScene(SceneManager
        .GetActiveScene().buildIndex);
    }
}
