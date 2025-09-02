using System.Collections;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private EnemyAnimations _enemyAnim;
    private Vector2 direcao;

    public float velocidadeMovimento;

    private bool isHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        _enemyAnim = GetComponent<EnemyAnimations>();
        direcao = Vector2.left;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHit == true)
        {
            MovimentoAtivado();
        }        
    }
    
    public void MovimentoAtivado()
    {
        rig2D.linearVelocity = new Vector2(direcao.x * velocidadeMovimento, rig2D.linearVelocityY);
    }

    public void Flip()
    {
        float x = transform.localScale.x;

        x *= -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //ativa animacao de hit e ataca o player;
            isHit = true;
            _enemyAnim.AnimacaoTopHit("isTopHit");
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector2();
                rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
            }
        }

        if (collision.collider.CompareTag("Parede"))
        {
            _enemyAnim.AnimacaoShellWall(true);
            direcao *= -1;
            Flip();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Parede"))
        {
            _enemyAnim.AnimacaoShellWall(false);
        }
    }
}
