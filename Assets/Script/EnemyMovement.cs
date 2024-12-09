using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;     // Tốc độ di chuyển
    public float minX = -17f;       // Giới hạn tọa độ X
    public float maxX = 17f;        // Giới hạn tọa độ X
    public float minY = -11f;       // Giới hạn tọa độ Y
    public float maxY = 11f;        // Giới hạn tọa độ Y
    public float detectionRange = 7f;  // Khoảng cách để phát hiện người chơi
    public Transform Player;        // Tham chiếu đến người chơi

    private Vector3 targetPosition; // Vị trí mục tiêu
    private float chaseDuration = 10f;  // Thời gian đuổi theo Player
    private float cooldownDuration = 5f; // Thời gian chờ trước khi đuổi lại
    private float chaseTimer = 0f;       // Bộ đếm thời gian đuổi theo
    private float cooldownTimer = 0f;    // Bộ đếm thời gian chờ
    private bool isChasing = false;      // Trạng thái đuổi theo Player

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player")?.transform;
            if (Player == null)
            {
                Debug.LogError("Player object not found! Please assign it in the Inspector.");
            }
        }
        SetRandomTargetPosition();
    }

    void Update()
    {
        // Tính toán khoảng cách giữa Enemy và Player
        float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(Player.position.x, Player.position.y, 0));

        if (isChasing)
        {
            // Đuổi theo Player trong thời gian quy định
            if (chaseTimer < chaseDuration)
            {
                chaseTimer += Time.deltaTime;
                MoveTowardsPlayer();
            }
            else
            {
                // Kết thúc thời gian đuổi theo và chuyển sang chế độ cooldown
                isChasing = false;
                chaseTimer = 0f;
                SetRandomTargetPosition();
            }
        }
        else
        {
            // Trong thời gian cooldown
            if (cooldownTimer < cooldownDuration)
            {
                cooldownTimer += Time.deltaTime;
                MoveTowardsTarget();
            }
            else if (distanceToPlayer <= detectionRange)
            {
                // Bắt đầu đuổi nếu gặp lại Player sau cooldown
                isChasing = true;
                cooldownTimer = 0f;
            }
            else
            {
                // Tiếp tục di chuyển đến vị trí ngẫu nhiên
                MoveTowardsTarget();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Di chuyển về phía Player
        Vector3 direction = (Player.position - transform.position).normalized; // Tính hướng
        transform.position += direction * moveSpeed * Time.deltaTime;  // Di chuyển về phía Player
    }

    void MoveTowardsTarget()
    {
        // Di chuyển về vị trí mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Nếu đã tới vị trí mục tiêu, đặt lại mục tiêu mới
        if (transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        // Tạo vị trí ngẫu nhiên trong phạm vi đã xác định
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector3(randomX, randomY, transform.position.z); // Đảm bảo Z không thay đổi
    }
}
