using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : MonoBehaviour
{
    public GameObject btnOver;

    private int attack = 5;
    [SerializeField] private float speed = 3f;
    private bool isGameOver = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isGameOver)
            return;

        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetInteger("Direction", 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            animator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
            animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetInteger("Direction", 0);
        }

        dir.Normalize();
        animator.SetBool("IsMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }

    void OnTriggerEnter2D(Collider2D other) // Thay thế Collider bằng Collider2D
    {
        if (other.CompareTag("Enemy"))
        {
            GameOver();
        }
    }
    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Dừng game
        btnOver.SetActive(true); // Hiển thị nút Game Over
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        // Hàm gọi khi nhấn nút Restart
        Time.timeScale = 1f; // Khôi phục game
        btnOver.SetActive(false); // Ẩn nút Game Over

        // Reset trạng thái Player hoặc load lại scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public int getAttack()
    {
        return this.attack;
    }
}