using System.Collections;
using UnityEngine;

public class MorteInimigoSnail : MonoBehaviour
{
    private float vidaInimigo;
    private bool levaDano;

    private EnemyAnimations _enemyAnimation;
    [SerializeField] private CapsuleCollider2D capsuleCol2D;
    private Instanciamento _instancia;

    private void Start()
    {
        _enemyAnimation = GetComponentInParent<EnemyAnimations>();
        capsuleCol2D = GetComponent<CapsuleCollider2D>();
        capsuleCol2D.enabled = true;
        levaDano = false;
        vidaInimigo = 1;
        _instancia = GetComponent<Instanciamento>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector2();
                rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
            }
            StartCoroutine(TempoDeDano());
        }
    }     
    
    IEnumerator TempoDeDano()
    {
        if (levaDano) { yield break; }

        levaDano = true;
        vidaInimigo--;
       _enemyAnimation.AnimacaoDeHit("isHit");
       if (vidaInimigo <= 0)
       {
            vidaInimigo = 0;

            _instancia.InstanciarSegundaEtapa();
            Destroy(gameObject, 0.02f);
            capsuleCol2D.enabled = false;
       }
        yield return new WaitForSeconds(0.533f);

        levaDano = false;
    }
}


