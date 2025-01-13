using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20; // Oyuncunun silahı ile verdiği hasar
    public float attackRange = 2f; // Saldırı menzili (ne kadar yakın olmalı)
    public Transform attackPoint; // Saldırı noktasının belirlenmesi (örneğin, silahın ucu)

    public LayerMask enemyLayer; // Düşmanların olduğu layer (Düşmanları tanımak için)

    private void Update()
    {
        // Sol tıkla saldırıyı tetikle
        if (Input.GetMouseButtonDown(0)) // Sol tıklama (0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Saldırı alanında düşman var mı kontrol et
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            // Eğer düşman var ise ona hasar ver
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage); // Düşmana hasar ver
                Debug.Log("Düşman hasar aldı!");
            }
        }
    }

    // Görsellik: saldırı menzilini görmek için bir sphere
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
