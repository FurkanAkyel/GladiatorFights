using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;         // Ana karakterin (oyuncunun) Transform'u
    public float followRange = 10f;  // Düşmanın takip edeceği mesafe
    public float attackRange = 2f;   // Düşmanın saldırabileceği mesafe
    public float patrolRange = 5f;   // Düşmanın rastgele gezinme mesafesi
    public float patrolWaitTime = 2f; // Rastgele gezinme sırasında bekleme süresi

    private NavMeshAgent agent;      // NavMesh Agent bileşeni
    private Vector3 patrolDestination; // Rastgele gezinti hedefi
    private float patrolTimer; // Rastgele gezinti süresi
    private Animator animator; // Animator bileşeni

    private void Start()
    {
        // NavMesh Agent ve Animator bileşenlerini al
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Animator bileşenini al

        // İlk gezinti hedefini belirle
        SetNewPatrolDestination();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Eğer oyuncu belli bir mesafede ise, takip et
        if (distanceToPlayer < followRange)
        {
            agent.SetDestination(player.position);

            // Eğer oyuncu çok yakınsa, saldırıya geç
            if (distanceToPlayer < attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // Rastgele gezinti yap
            Patrol();
        }

        // Animator'a hız bilgisini gönder
        AnimateMovement();
    }

    private void AttackPlayer()
    {
        // Saldırı animasyonunu burada tetikleyebilirsiniz
        Debug.Log("Düşman oyuncuyu saldırdı!");
        // Hasar verme ve animasyon kodlarını buraya ekleyebilirsiniz
    }

    private void Patrol()
    {
        // Eğer patrol hedefi çok uzaktaysa, yeni hedef belirle
        if (Vector3.Distance(transform.position, patrolDestination) < 1f)
        {
            patrolTimer += Time.deltaTime;

            // Rastgele gezinme sırasında belirli bir süre bekleyebiliriz
            if (patrolTimer >= patrolWaitTime)
            {
                SetNewPatrolDestination();
                patrolTimer = 0f;
            }
        }

        // Eğer patikada bir hedef varsa, o hedefe git
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(patrolDestination);
        }
    }

    private void SetNewPatrolDestination()
    {
        // Rastgele bir koordinat belirleyin
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);
        patrolDestination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Hedefi NavMesh üzerinde bir noktaya yerleştirme
        NavMeshHit hit;
        if (NavMesh.SamplePosition(patrolDestination, out hit, patrolRange, NavMesh.AllAreas))
        {
            patrolDestination = hit.position;
        }
    }

    // Animator'daki hareket animasyonunu kontrol et
    private void AnimateMovement()
    {
        // NavMeshAgent'in hızını kullanarak speed parametresini güncelle
        float speed = agent.velocity.magnitude; // Hız = hareketin büyüklüğü
        animator.SetFloat("Speed", speed); // Speed parametresini animator'a ilet
    }
}
