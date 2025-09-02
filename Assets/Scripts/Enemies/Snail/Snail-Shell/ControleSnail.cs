using System.Collections;
using UnityEngine;


public class ControleSnail : MonoBehaviour
{
    private EstadoDoInimigo estadoAtual;

    private Rigidbody2D rig2D;
    public Transform distanciaRaycast;
    private EnemyAnimations _enemyAnim;

    public float velocidadeMovimento;
    public float tamanhoRaycast;
    public int vida;

    public LayerMask layerMask;
   
    public Vector2 direcao;

    private bool isIdle;
    private bool isWalk;

    private bool olhandoDireita = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        _enemyAnim = GetComponent<EnemyAnimations>();
        direcao = Vector2.right;
        estadoAtual = EstadoDoInimigo.Patrulhando;
        isWalk = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        switch (estadoAtual)
        {
            case EstadoDoInimigo.Esperando:
                rig2D.linearVelocity = Vector2.zero;
                isIdle = true;
                isWalk = false;
                break;
            case EstadoDoInimigo.Patrulhando:
                Movimentacao();
                isIdle = false;
                isWalk = true;
                break;
        }
        Animacoes();
    }

    public void Animacoes()
    {
        _enemyAnim.AnimacaoDeCaminhada(isWalk);
        _enemyAnim.AnimacaoParado(isIdle);
    }

    public void Movimentacao()
    {
        rig2D.linearVelocity = new Vector2(direcao.x * velocidadeMovimento, rig2D.linearVelocityY);

        RaycastHit2D hit = Physics2D.Raycast(distanciaRaycast.position, direcao, tamanhoRaycast);

        if (hit.collider != null && hit.collider.CompareTag("Parede") && olhandoDireita)
        {
            Debug.Log(hit.collider.name);

            StartCoroutine(TempoDeVirada(1f));

        }


    }

    IEnumerator TempoDeVirada(float tempo)
    {
        olhandoDireita = false;
        estadoAtual = EstadoDoInimigo.Esperando;
        yield return new WaitForSeconds(tempo);
        direcao *= -1;

        if (direcao.x < 0)
        {
            Flip();
        }
        else if (direcao.x > 0)
        {
            Flip();
        }
        olhandoDireita = true;
        estadoAtual = EstadoDoInimigo.Patrulhando;

    }

    public void Flip()
    {
        float x = transform.localScale.x;

        x *= -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(distanciaRaycast.position, direcao * tamanhoRaycast);
    }
}
