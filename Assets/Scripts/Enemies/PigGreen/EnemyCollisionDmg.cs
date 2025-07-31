using UnityEngine;

public class EnemyCollisionDmg : MonoBehaviour
{
    public EnemyDeath enemyRef;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyRef.TomarDano();
        }
    }
}
