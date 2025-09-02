using System.Collections;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Rigidbody2D rig2d;
    [SerializeField] private CapsuleCollider2D col2D;
    [SerializeField] private SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rig2d = GetComponent<Rigidbody2D>();
        col2D = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (rig2d == null)
            rig2d = GetComponentInChildren<Rigidbody2D>();
        if (col2D == null)
            col2D = GetComponentInChildren<CapsuleCollider2D>();
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();

        // Aviso caso algum componente não seja encontrado
        if (anim == null)
            Debug.LogWarning($"Animator não encontrado em {name} ou nos filhos!");
        if (rig2d == null)
            Debug.LogWarning($"Rigidbody2D não encontrado em {name} ou nos filhos!");
        if (col2D == null)
            Debug.LogWarning($"CapsuleCollider2D não encontrado em {name} ou nos filhos!");
        if (sprite == null)
            Debug.LogWarning($"SpriteRenderer não encontrado em {name} ou nos filhos!");
    }

    public void AnimacaoDeCaminhada(bool value)
    {
        anim.SetBool("isWalk", value);
    }

    public void AnimacaoDeCorrida(bool value)
    {
        anim.SetBool("isRun", value);
    }

    public void AnimacaoShellWall(bool value)
    {
        anim.SetBool("isWallHit", value);
    }
    public void AnimacaoParado(bool value)
    {
        anim.SetBool("isIdle", value);
    }

    public void AnimacaoTopHit(string value)
    {
        anim.SetTrigger(value);
    }

    public void AnimacaoDeHit(string value)
    {
        anim.SetTrigger(value);
    }
    //animacao da bala do trunk
    public void AnimacaoDeDano(string value)
    {
        anim.SetTrigger(value);
    }
    //animacao de ataque do trunk
    public void AnimacaoDeAtaque(string value)
    {
        anim.SetTrigger(value);
    }


    public void AnimacaoDeMorte()
    {
        if (rig2d == null || col2D == null || sprite == null)
        {
            Debug.LogWarning($"Não é possível executar AnimacaoDeMorte() em {name} porque algum componente está faltando!");
            return;
        }
        StartCoroutine(AnimacaoMorte());
    }
    private IEnumerator AnimacaoMorte()
    {
        col2D.enabled = false;

        rig2d.bodyType = RigidbodyType2D.Dynamic;
        rig2d.linearVelocity = new Vector2(rig2d.linearVelocityX, 0f);
        rig2d.AddForce(Vector2.up * 2.5f, ForceMode2D.Impulse);
        rig2d.freezeRotation = true;
        rig2d.gravityScale = 1;

        float tempo = 0f;
        float duracao = 0.8f;
        float giro = 0f;


        while (tempo <= duracao)
        {
            tempo += Time.deltaTime;
            giro += 90 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, giro);
            yield return null;
        }

        sprite.enabled = false;
        Destroy(gameObject, 0.8f);
    }
}
