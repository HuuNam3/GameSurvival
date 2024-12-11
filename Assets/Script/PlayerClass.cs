using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : MonoBehaviour
{
    public GameObject btnOver;

    private int attack = 5;
    private float speed = 4f;
    private bool isGameOver = false;
    private float minX = -17f;
    private float maxX = 17f;
    private float minY = -11f;
    private float maxY = 11f;

    void Update()
    {
        if (isGameOver)
            return;
        // Lấy input từ bàn phím
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Tính toán vị trí mới
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + movement;
        // Giới hạn bản đồ
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
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