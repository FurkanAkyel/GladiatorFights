using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100; // Düşmanın başlangıç sağlığı
    private Animator animator; // Animator bileşenine referans
    private bool isDead = false; // Düşmanın öldüğünü kontrol etmek için bir bayrak

    private void Start()
    {
        animator = GetComponent<Animator>(); // Animator bileşenini al
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Eğer düşman öldüyse, daha fazla işlem yapma

        // Düşman hasar aldığında, sağlığı azalt
        health -= damage;

        // Düşman ölürse
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Ölüm işlemi zaten gerçekleşmişse, tekrar oynatma

        isDead = true;
        animator.SetTrigger("Die"); // Ölüm animasyonunu tetikle
        Debug.Log("Düşman öldü!");

        // Ölüm animasyonu tamamlandıktan sonra düşmanı yok et
        Destroy(gameObject, 2f); // 2 saniye bekle
    }
}
