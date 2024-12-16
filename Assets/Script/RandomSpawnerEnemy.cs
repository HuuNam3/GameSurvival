using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawnerEnemy : MonoBehaviour
{
    public GameObject objectToSpawn;   // Prefab mà bạn muốn spawn
    public Text enemyText;
    public Transform player;
    public List<GameObject> enemies;

    private float minX = -50f;     // Giới hạn tọa độ X
    private float maxX = 50f;      // Giới hạn tọa độ X
    private float minY = -50f;     // Giới hạn tọa độ Y
    private float maxY = 50f;        // Tọa độ y lớn nhất
    private int enemyCount = 0;
    private float minDistanceFromPlayer = 3f;

    void Start()
    {
        enemies = new List<GameObject>();
        //InvokeRepeating("SpawnObject", 0f, 6f);
        SpawnObject();
    }

    void SpawnObject()
    {
        // Tạo vị trí ngẫu nhiên trong phạm vi đã xác định
        enemyCount += 1;
        enemyText.text = "Enemy: " + enemyCount.ToString();

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        if (Mathf.Abs(randomX - player.position.x) < minDistanceFromPlayer)
        {
            randomX += (randomX > player.position.x) ? minDistanceFromPlayer : -minDistanceFromPlayer;
        }

        if (Mathf.Abs(randomY - player.position.y) < minDistanceFromPlayer)
        {
            randomY += (randomY > player.position.y) ? minDistanceFromPlayer : -minDistanceFromPlayer;
        }

        randomX = Mathf.Clamp(randomX, minX, maxX);
        randomY = Mathf.Clamp(randomY, minY, maxY);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        // Spawn Prefab tại vị trí ngẫu nhiên
        GameObject enemy = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        
        EnemyClass enemyScript = enemy.GetComponent<EnemyClass>();
        enemies.Add(enemy);

        //if (enemies.Count < 5 )
        //{
        //    enemyScript.EnemyLevel(1);
        //} else if(enemies.Count < 10)
        //{
        //    enemyScript.EnemyLevel(2);
        //} else if (enemies.Count < 13)
        //{
        //    enemyScript.EnemyLevel(3);
        //} else
        //{
        //    enemyScript.EnemyLevel(4);
        //    CancelInvoke("SpawnObject");
        //}
    }
}