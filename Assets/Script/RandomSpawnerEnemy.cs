using UnityEngine;
using UnityEngine.UI;

public class RandomSpawnerEnemy : MonoBehaviour
{
    public GameObject objectToSpawn;   // Prefab mà bạn muốn spawn
    public float minX = -17f;     // Giới hạn tọa độ X
    public float maxX = 17f;      // Giới hạn tọa độ X
    public float minY = -11f;     // Giới hạn tọa độ Y
    public float maxY = 11f;        // Tọa độ Z lớn nhất
    public Text enemyText;
    private int enemyCount = 0;

    void Start()
    {
        // Gọi phương thức SpawnObject khi game bắt đầu
        InvokeRepeating("SpawnObject", 0f, 6f);
    }

    void SpawnObject()
    {
        // Tạo vị trí ngẫu nhiên trong phạm vi đã xác định
        enemyCount += 1;
        enemyText.text = "Enemy: " + enemyCount.ToString();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0 );

        // Spawn Prefab tại vị trí ngẫu nhiên
        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }
}
