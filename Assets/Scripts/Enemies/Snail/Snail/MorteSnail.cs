using UnityEngine;

public class MorteSnail : MonoBehaviour
{
    private EnemyAnimations _enemyAnim;

    void Start()
    {
        _enemyAnim = GetComponent<EnemyAnimations>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2();
                rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
            }
            _enemyAnim.AnimacaoDeMorte();
        }
    }
}
