using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Spawnlanacak düşman prefab'ı
    public float spawnInterval = 10f;      // Düşmanların ne kadar aralıklarla spawnlanacağı
    public float spawnRange = 1f;        // Spawn alanının çapı
    public int maxEnemies = 3;            // Sahnedeki maksimum düşman sayısı
    private int currentEnemyCount = 0;    // Şu anda sahnedeki düşman sayısı

    private void Start()
    {
        // Başlangıçta spawn işlemine başla
        InvokeRepeating("TrySpawnEnemy", 0f, spawnInterval);
    }

    private void TrySpawnEnemy()
    {
        // Eğer sahnedeki düşman sayısı maksimumdan küçükse, yeni düşman spawnla
        if (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Spawnlanacak rastgele bir pozisyon belirle
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnRange, spawnRange),
            transform.position.y,
            transform.position.z + Random.Range(-spawnRange, spawnRange)
        );

        // Düşmanı spawnla
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Düşman sayısını arttır
        currentEnemyCount++;
    }

    // Düşman öldüğünde çağrılacak metot
    public void EnemyDied()
    {
        currentEnemyCount--;
    }
}
