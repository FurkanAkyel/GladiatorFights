using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 20; // Düşmanın verdiği hasar
    public float attackRange = 2f; // Saldırı menzili
    public Transform attackPoint; // Saldırı noktasının belirlenmesi
    public float attackCooldown = 1f; // Saldırı cooldown süresi
    private float lastAttackTime = 0f; // Son saldırı zamanını tutan değişken

    public LayerMask playerLayer; // Oyuncuyu tanımak için layer
    private Animator animator; // Animator bileşeni

    private void Start()
    {
        // Animator bileşenini al
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Eğer cooldown süresi dolmuşsa ve oyuncu yakınsa, saldır
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

            foreach (Collider player in hitPlayers)
            {
                // Eğer oyuncu varsa ona hasar ver
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage); // Oyuncuya hasar ver
                    lastAttackTime = Time.time; // Son saldırıyı güncelle
                    Debug.Log("Düşman oyuncuya saldırdı!");
                    TriggerAttackAnimation(); // Saldırı animasyonunu başlat
                }
            }
        }
    }

    private void TriggerAttackAnimation()
    {
        // Animator'daki "Attack" trigger'ını tetikle
        animator.SetTrigger("Attack");
    }

    // Görsellik: saldırı menzilini görmek için bir sphere
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
