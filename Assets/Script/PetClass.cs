using TMPro;
using UnityEngine;

public class PetClass : MonoBehaviour
{
    [SerializeField] private Transform player; // Nhân vật trung tâm
    [SerializeField] private float area = 4f; // Bán kính khu vực di chuyển
    [SerializeField] private int attack = 5;
    [SerializeField] private TextMeshPro UIname;
    [SerializeField] private string namePet = "Dog";
    [SerializeField] private float moveSpeed = 4f; // Tốc độ di chuyển
    [SerializeField] private float returnThreshold = 7f; // Khoảng cách để quay về player
    [SerializeField] private string enemyTag = "Enemy"; // Tag của đối tượng Enemy
    [SerializeField] private float detectionRange = 12f; // Khoảng cách phát hiện Enemy
    [SerializeField] private float attackCooldown = 0.5f; // Thời gian chờ giữa các lần tấn công
    private Vector3 targetPosition; // Vị trí mục tiêu
    private bool isReturningToPlayer = false; // Trạng thái quay về player
    private Transform enemyTarget = null; // Mục tiêu Enemy
    private float lastAttackTime = 0f; // Thời gian lần tấn công cuối

    void Start()
    {
        init();
        SetRandomTarget(); // Đặt vị trí mục tiêu ngẫu nhiên ban đầu
    }

    void Update()
    {
        if (player != null)
        {
            UpdateClosestEnemy(); // Luôn tìm kiếm Enemy gần nhất

            if (enemyTarget != null)
            {
                // Cập nhật vị trí mục tiêu là Enemy hiện tại
                targetPosition = enemyTarget.position;

                // Nếu đến gần Enemy, thực hiện hành động
                if (Vector3.Distance(transform.position, enemyTarget.position) < 0.5f)
                {
                    if (Time.time - lastAttackTime >= attackCooldown)
                    {
                        EnemyClass enemyScript = enemyTarget.GetComponent<EnemyClass>();
                        enemyScript.TakeDamage(this.attack);
                        lastAttackTime = Time.time; // Cập nhật thời gian tấn công cuối
                        Debug.Log("Attacked Enemy: " + enemyTarget.name);
                    }
                }
            }
            else
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                // Nếu ra khỏi khu vực, quay về player
                if (distanceToPlayer > returnThreshold)
                {
                    isReturningToPlayer = true;
                    targetPosition = player.position;
                }
                // Nếu đến gần player, bắt đầu di chuyển xung quanh
                else if (isReturningToPlayer && distanceToPlayer <= area)
                {
                    isReturningToPlayer = false;
                    SetRandomTarget();
                }

                // Nếu đang di chuyển xung quanh và đã đến gần mục tiêu, đặt mục tiêu mới
                if (!isReturningToPlayer && Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    SetRandomTarget();
                }
            }

            // Di chuyển về vị trí mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void init()
    {
        this.UIname.text = this.namePet;
    }

    void SetRandomTarget()
    {
        // Tạo một vị trí ngẫu nhiên trong khu vực xung quanh player
        float randomX = Random.Range(-area, area);
        float randomY = Random.Range(-area, area);

        // Đặt mục tiêu là vị trí xung quanh player
        targetPosition = new Vector3(player.position.x + randomX, player.position.y + randomY, transform.position.z);
    }

    private void UpdateClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && distance <= detectionRange) // Chỉ xét Enemy trong phạm vi phát hiện
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        // Cập nhật mục tiêu Enemy gần nhất nếu tìm thấy
        enemyTarget = closest;
    }
}
