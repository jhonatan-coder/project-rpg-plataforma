using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MorteShell : MonoBehaviour
{
    public int vidaInimigo = 2;
    private EnemyAnimations _enemyAnim;
    private Rigidbody2D rig2D;
    private CapsuleCollider2D col2D;
    private Movimento movimento;

    private bool levaDano = false;

    void Start()
    {
        _enemyAnim = GetComponent<EnemyAnimations>();
        col2D = GetComponent<CapsuleCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
        movimento = GetComponent<Movimento>();
        col2D.enabled = true;
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
            StartCoroutine(TempoDeDano());
        }
    }

    IEnumerator TempoDeDano()
    {

        if (levaDano) { yield break; }
        levaDano = true;
        Debug.Log("Levou dano");
        vidaInimigo--;
        if (vidaInimigo == 1)
        {
            movimento.MovimentoAtivado();
        }
        else if (vidaInimigo <= 0)
        {
            vidaInimigo = 0;
            rig2D.constraints = RigidbodyConstraints2D.None;
            _enemyAnim.AnimacaoDeMorte();
            col2D.enabled = false;
        }

        yield return new WaitForSeconds(0.533f);

        levaDano = false;

    }
}
