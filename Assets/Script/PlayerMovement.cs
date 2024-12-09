using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    public GameObject btnOver;
    private bool isGameOver = false;
    public float minX = -17f;
    public float maxX = 17f;
    public float minY = -11f;
    public float maxY = 11f;

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

}