using TMPro;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 dir;

    private float speed = 3f;

    public TextMeshPro info;
    public Transform Player;

    private float detectionRange;
    private float hp;           // HP c?a enemy
    private float armor;
    private float goldDrop;
    private string enemyName; // Tên c?a enemy

    //private float minX = -17f;       // Gi?i h?n t?a ?? X
    //private float maxX = 17f;        // Gi?i h?n t?a ?? X
    //private float minY = -11f;       // Gi?i h?n t?a ?? Y
    //private float maxY = 11f;
    //private Vector3 targetPosition; // V? trí m?c tiêu
    //private float chaseDuration = 10f;  // Th?i gian ?u?i theo Player
    //private float cooldownDuration = 3f; // Th?i gian ch? tr??c khi ?u?i l?i
    //private float chaseTimer = 0f;       // B? ??m th?i gian ?u?i theo
    //private float cooldownTimer = 0f;    // B? ??m th?i gian ch?
    //private bool isChasing = false;      // Tr?ng thái ?u?i theo Player

    void Start()
    {
        dir = Vector2.zero;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //SetRandomDirection();
        //Player = GameObject.FindWithTag("Player").transform;
        //UpdateText();
        //SetRandomTargetPosition();
    }

    void Update()
    {
        MoveEnemy();
        // Tính toán kho?ng cách gi?a Enemy và Player
        //float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(Player.position.x, Player.position.y, 0));
        if (ShouldChangeDirection())
        {
            SetRandomDirection();
        }
        //if (isChasing)
        //{
        //    // ?u?i theo Player trong th?i gian quy ??nh
        //    if (chaseTimer < chaseDuration)
        //    {
        //        chaseTimer += Time.deltaTime;
        //        MoveTowardsPlayer();
        //    }
        //    else
        //    {
        //        // K?t thúc th?i gian ?u?i theo và chuy?n sang ch? ?? cooldown
        //        isChasing = false;
        //        chaseTimer = 0f;
        //        SetRandomTargetPosition();
        //    }
        //}
        //else
        //{
        //    // Trong th?i gian cooldown
        //    if (cooldownTimer < cooldownDuration)
        //    {
        //        cooldownTimer += Time.deltaTime;
        //        MoveTowardsTarget();
        //    }
        //    else if (distanceToPlayer <= detectionRange)
        //    {
        //        // B?t ??u ?u?i n?u g?p l?i Player sau cooldown
        //        isChasing = true;
        //        cooldownTimer = 0f;
        //    }
        //    else
        //    {
        //        // Ti?p t?c di chuy?n ??n v? trí ng?u nhiên
        //        MoveTowardsTarget();
        //    }
        //}
    }
    private void SetRandomDirection()
    {
        // Lấy hướng ngẫu nhiên trong phạm vi (-1, 1) cho cả x và y
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        dir = new Vector2(randomX, randomY).normalized; // Chuẩn hóa vector để giữ tốc độ nhất quán
    }
    private void MoveEnemy()
    {
        rb.velocity = dir * speed;
    }

    private bool ShouldChangeDirection()
    {
        // Thay đổi hướng nếu chạm biên hoặc theo logic tùy chỉnh
        // Ví dụ: Chạm tường hoặc sau một khoảng thời gian
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.5f);
        return hit.collider != null || Random.Range(0, 100) < 5; // 5% thay đổi hướng mỗi frame
    }

    void UpdateText()
    {
        this.info.text = $"{this.enemyName} - HP: {this.hp}";
    }
    // Hàm gi?m HP
    public void TakeDamage(int damage)
    {
        if(damage - this.armor <= 0)
        {
            this.hp -= 1;
        } else
        {
            this.hp -= (damage - this.armor);
        };
        UpdateText();

        //if (hp <= 0)
        //{
        //    getGold();
        //    Die();
        //}
    }

    // Hàm x? lý khi enemy ch?t
    private void Die()
    {
        Destroy(gameObject); // Xóa ??i t??ng kh?i scene
    }

    //public void EnemyLevel(int level)
    //{
    //    if (level == 1)
    //    {
    //        this.enemyName = "slime";
    //        this.hp = 10;
    //        this.armor = 0;
    //        this.info.color = Color.blue;
    //        this.speed = 3f;
    //        this.detectionRange = 9f;
    //        this.goldDrop = 1;
    //    }
    //    else if (level == 2)
    //    {
    //        this.enemyName = "Goblin";
    //        this.hp = 20;
    //        this.armor = 2;
    //        this.info.color = Color.green;
    //        this.speed = 2.5f;
    //        this.detectionRange = 9f;
    //        this.goldDrop = 2;
    //    }
    //    else if (level == 3)
    //    {
    //        this.enemyName = "Boss";
    //        this.hp = 30;
    //        this.armor = 3;
    //        this.speed = 3.5f;
    //        this.info.color = Color.red;
    //        this.detectionRange = 12f;
    //        this.goldDrop = 5;
    //    }
    //    else if (level == 4)
    //    {
    //        this.enemyName = "Super Boss";
    //        this.hp = 100;
    //        this.armor = 5;
    //        this.speed = 4f;
    //        this.info.color = Color.red;
    //        this.detectionRange = 15f;
    //        this.goldDrop = 20;
    //    }
    //}

    //private void MoveTowardsPlayer()
    //{
    //    // Di chuy?n v? phía Player
    //    Vector3 direction = (Player.position - transform.position).normalized; // Tính h??ng
    //    transform.position += direction * moveSpeed * Time.deltaTime;  // Di chuy?n v? phía Player
    //}

    //private void MoveTowardsTarget()
    //{
    //    // Di chuy?n v? v? trí m?c tiêu
    //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    //    // N?u ?ã t?i v? trí m?c tiêu, ??t l?i m?c tiêu m?i
    //    if (transform.position == targetPosition)
    //    {
    //        SetRandomTargetPosition();
    //    }
    //}

    //private void SetRandomTargetPosition()
    //{
    //    // T?o v? trí ng?u nhiên trong ph?m vi ?ã xác ??nh
    //    float randomX = Random.Range(minX, maxX);
    //    float randomY = Random.Range(minY, maxY);
    //    targetPosition = new Vector3(randomX, randomY, transform.position.z); // ??m b?o Z không thay ??i
    //}

    //private void getGold()
    //{
    //    float gold = PlayerPrefs.GetFloat("Gold",0);
    //    gold += this.goldDrop;
    //    PlayerPrefs.SetFloat("Gold", gold);
    //}
}
