using UnityEngine;

public class DestruirMunicao : MonoBehaviour
{
    private EnemyAnimations _enemyAnimations;
    private void Start()
    {
        _enemyAnimations = GetComponent<EnemyAnimations>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeInHierarchy) { return; }

        if (collision.gameObject.CompareTag("Municao")) { return; }

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        _enemyAnimations.AnimacaoDeDano("isHit");
        
        Destroy(gameObject, 0.333f);

    }

    
}
