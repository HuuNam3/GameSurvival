using TMPro;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public TextMeshPro info;
    public Transform Player;

    private float detectionRange;
    private float hp;           // HP c?a enemy
    private float armor;
    private float moveSpeed;
    private float goldDrop;
    private string enemyName; // T�n c?a enemy

    private float minX = -17f;       // Gi?i h?n t?a ?? X
    private float maxX = 17f;        // Gi?i h?n t?a ?? X
    private float minY = -11f;       // Gi?i h?n t?a ?? Y
    private float maxY = 11f;
    private Vector3 targetPosition; // V? tr� m?c ti�u
    private float chaseDuration = 10f;  // Th?i gian ?u?i theo Player
    private float cooldownDuration = 3f; // Th?i gian ch? tr??c khi ?u?i l?i
    private float chaseTimer = 0f;       // B? ??m th?i gian ?u?i theo
    private float cooldownTimer = 0f;    // B? ??m th?i gian ch?
    private bool isChasing = false;      // Tr?ng th�i ?u?i theo Player

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
        UpdateText();
        SetRandomTargetPosition();
    }

    void Update()
    {
        // T�nh to�n kho?ng c�ch gi?a Enemy v� Player
        float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(Player.position.x, Player.position.y, 0));

        if (isChasing)
        {
            // ?u?i theo Player trong th?i gian quy ??nh
            if (chaseTimer < chaseDuration)
            {
                chaseTimer += Time.deltaTime;
                MoveTowardsPlayer();
            }
            else
            {
                // K?t th�c th?i gian ?u?i theo v� chuy?n sang ch? ?? cooldown
                isChasing = false;
                chaseTimer = 0f;
                SetRandomTargetPosition();
            }
        }
        else
        {
            // Trong th?i gian cooldown
            if (cooldownTimer < cooldownDuration)
            {
                cooldownTimer += Time.deltaTime;
                MoveTowardsTarget();
            }
            else if (distanceToPlayer <= detectionRange)
            {
                // B?t ??u ?u?i n?u g?p l?i Player sau cooldown
                isChasing = true;
                cooldownTimer = 0f;
            }
            else
            {
                // Ti?p t?c di chuy?n ??n v? tr� ng?u nhi�n
                MoveTowardsTarget();
            }
        }
    }

    void UpdateText()
    {
        this.info.text = $"{this.enemyName} - HP: {this.hp}";
    }
    // H�m gi?m HP
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

        if (hp <= 0)
        {
            getGold();
            Die();
        }
    }

    // H�m x? l� khi enemy ch?t
    private void Die()
    {
        Destroy(gameObject); // X�a ??i t??ng kh?i scene
    }

    public void EnemyLevel(int level)
    {
        if (level == 1)
        {
            this.enemyName = "slime";
            this.hp = 10;
            this.armor = 0;
            this.info.color = Color.blue;
            this.moveSpeed = 3f;
            this.detectionRange = 9f;
            this.goldDrop = 1;
        }
        else if (level == 2)
        {
            this.enemyName = "Goblin";
            this.hp = 20;
            this.armor = 2;
            this.info.color = Color.green;
            this.moveSpeed = 2.5f;
            this.detectionRange = 9f;
            this.goldDrop = 2;
        }
        else if (level == 3)
        {
            this.enemyName = "Boss";
            this.hp = 30;
            this.armor = 3;
            this.moveSpeed = 3.5f;
            this.info.color = Color.red;
            this.detectionRange = 12f;
            this.goldDrop = 5;
        }
        else if (level == 4)
        {
            this.enemyName = "Super Boss";
            this.hp = 100;
            this.armor = 5;
            this.moveSpeed = 4f;
            this.info.color = Color.red;
            this.detectionRange = 15f;
            this.goldDrop = 20;
        }
    }

    private void MoveTowardsPlayer()
    {
        // Di chuy?n v? ph�a Player
        Vector3 direction = (Player.position - transform.position).normalized; // T�nh h??ng
        transform.position += direction * moveSpeed * Time.deltaTime;  // Di chuy?n v? ph�a Player
    }

    private void MoveTowardsTarget()
    {
        // Di chuy?n v? v? tr� m?c ti�u
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // N?u ?� t?i v? tr� m?c ti�u, ??t l?i m?c ti�u m?i
        if (transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        // T?o v? tr� ng?u nhi�n trong ph?m vi ?� x�c ??nh
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector3(randomX, randomY, transform.position.z); // ??m b?o Z kh�ng thay ??i
    }

    private void getGold()
    {
        float gold = PlayerPrefs.GetFloat("Gold",0);
        gold += this.goldDrop;
        PlayerPrefs.SetFloat("Gold", gold);
    }
}
