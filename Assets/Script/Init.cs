using UnityEngine;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    [SerializeField]
    public Text Gold;
    private PlayerClass player;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerClass>();
        }
        //float gold = PlayerPrefs.GetFloat("Gold", 0);
        //Gold.text = "Gold: " + gold;
    }

    
    void Update()
    {
        float gold = PlayerPrefs.GetFloat("Gold", 0);
        Gold.text = "Gold: " + gold;

        if (Input.GetMouseButtonDown(0)) // Phát hi?n click chu?t trái
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                //Debug.Log("Enemy clicked: " + hit.collider.gameObject.name);
                EnemyClass enemyScript = hit.collider.gameObject.GetComponent<EnemyClass>();
                Debug.Log(player.getAttack());
                enemyScript.TakeDamage(player.getAttack());
            }
        }
    }
}
