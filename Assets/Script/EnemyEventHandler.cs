using UnityEngine;

public class EnemyEventHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Phát hiện click chuột trái
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy clicked: " + hit.collider.gameObject.name);
                EnemyClass enemyScript = hit.collider.gameObject.GetComponent<EnemyClass>();
                enemyScript.TakeDamage(5);
            }
        }
    }
}