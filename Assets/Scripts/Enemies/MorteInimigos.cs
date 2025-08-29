using System.Collections;
using UnityEngine;

public class MorteInimigos : MonoBehaviour
{
    private float vidaInimigo;
    private bool levaDano;

    private EnemyAnimations _enemyAnimation;
    private BoxCollider2D box2D;
    [SerializeField]private EnemyControll _enemyControll;

    private void Start()
    {
        _enemyControll = GetComponentInParent<EnemyControll>();
        _enemyAnimation = GetComponentInParent<EnemyAnimations>();
        box2D = GetComponent<BoxCollider2D>();
        box2D.enabled = true;
        levaDano = false;
        vidaInimigo = 2;
        _enemyControll.vidaInimigo = Mathf.Max(vidaInimigo, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

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
            _enemyAnimation.AnimacaoDeMorte();
            box2D.enabled = false;
            _enemyControll.IsMorreu = true;
       }
        yield return new WaitForSeconds(0.533f);

        levaDano = false;
    }

}


