using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    // Phương thức nhận dữ liệu từ PlayerHealth để hiển thị
    public void SetHealthText(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth} / {maxHealth}";
        }
    }
}
