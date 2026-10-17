using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float respawnDelay = 2f;

    public void Start()
    {
        SpawnEnemyImmediately();
    }
    public void SpawnEnemyImmediately()
    {
        GameObject enemy = Instantiate(
        enemyPrefab, transform.position, Quaternion.identity
        );

        
        if (enemy.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.SetSpawner(this);
        }

    }

    private IEnumerator SpawnEnemyWitchDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnEnemyImmediately();
    }

    public void SpawnEnemy()
    {
        StartCoroutine(SpawnEnemyWitchDelay());
    }
}
