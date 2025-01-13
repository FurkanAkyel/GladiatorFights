using UnityEngine;
using UnityEngine.SceneManagement; // Scene yönetimi için
using UnityEngine.UI; // Slider ve UI bileşenleri için
using TMPro; // TextMeshPro bileşenleri için

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Oyuncunun başlangıç sağlığı
    public int maxHealth = 100; // Maksimum sağlık
    public Slider healthBar; // Sağlık çubuğu (Slider)
    public TextMeshProUGUI gameOverText; // Game Over yazısını gösterecek TextMeshProUGUI

    void Start()
    {
        // Sağlık çubuğunu başlat
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health; // Başlangıç değeri
        }

        // Başlangıçta Game Over yazısını gizle
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Eğer oyuncu öldü ise, R tuşuna basıldığında oyunu yeniden başlat
        if (health <= 0)
        {
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true); // Game Over yazısını göster
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame(); // R tuşuna basıldığında oyunu yeniden başlat
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Oyuncuya hasar verildiğinde, sağlığı azalt
        health -= damage;

        // Sağlık sıfır veya daha düşükse oyuncuyu öldür
        if (health <= 0)
        {
            health = 0; // Sağlık sıfırdan düşük olmasın
            Die();
        }

        // Sağlık çubuğunu güncelle
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }

    private void Die()
    {
        // Oyuncu öldüğünde yapılacaklar
        Debug.Log("Oyuncu öldü!");
        // Oyun duraklatılır
        Time.timeScale = 0; // Oyun durdurulur
    }

    public void RestartGame()
    {
        // Zamanı yeniden başlat
        Time.timeScale = 1; // Oyunun hızını normal seviyeye getir

        // Şu anki sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
