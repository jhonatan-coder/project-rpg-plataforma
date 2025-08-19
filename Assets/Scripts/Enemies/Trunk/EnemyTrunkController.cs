using System.Collections;
using UnityEngine;

public class EnemyTrunkController : MonoBehaviour
{
    private PlayerController _playerController;
    private EnemyAnimations _enemyAnimations;
    private Rigidbody2D inimigo;
    //private MorteInimigos _morteInimigos;

    public Transform[] posicoes;

    public Transform rayCastLine;
    public float velocidadeInimigo;
    public int vidaInimigo;

    public GameObject municao;
    public Transform arma;
    private int indiceDestinoAtual;

    private float tempoSemVerPlayer = 0f;
    public float tempoParaDesistir = 2f;
    private bool playerVisivel = false;

    private bool inimigoCaminhando;
    public float velocidadeDisparo;
    public float intervaloDeAtaque;

    private bool podeAtirar;
    private bool atacando;
    private bool levandoDano;
    private bool isMorreu;

    public LayerMask layerMask;

    private bool inicializado = false;

    private EstadoDoInimigo estadoAtual;

    [HideInInspector] public InstanciarInimigoTrunk instanciador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        estadoAtual = EstadoDoInimigo.Patrulhando;

        _playerController = FindFirstObjectByType<PlayerController>();

        _enemyAnimations = GetComponent<EnemyAnimations>();

        inimigo = GetComponent<Rigidbody2D>();

        //_morteInimigos = GetComponent<MorteInimigos>();

        indiceDestinoAtual = 1;
        podeAtirar = true;

        levandoDano = false;
        isMorreu = false;
        if (posicoes != null && posicoes.Length > 0)
        {
            inicializado = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (estadoAtual)
        {
            case EstadoDoInimigo.Patrulhando:
                VerificaVisibilidadePlayer();
                PatrulhaInimigo();
                atacando = false;
                inimigoCaminhando = true;
                break;
            case EstadoDoInimigo.Atacando:
                VerificaVisibilidadePlayer();
                AtacandoPlayer();
                atacando = true;
                inimigoCaminhando = false;          
                break;
            case EstadoDoInimigo.Esperando:
                if (isMorreu == false) { VerificaVisibilidadePlayer(); }
                inimigoCaminhando = false;
                atacando = false;
                break;
        }

    }
    private void FixedUpdate()
    {
        Animacoes();
    }

    public void Animacoes()
    {
        _enemyAnimations.AnimacaoDeCaminhada(inimigoCaminhando);
    }
    public void PatrulhaInimigo()
    {

        Vector2 direcao = (posicoes[indiceDestinoAtual].position - transform.position).normalized;

        inimigo.linearVelocity = direcao * velocidadeInimigo;

        float distancia = Vector2.Distance(inimigo.position, posicoes[indiceDestinoAtual].position);

        if (distancia < 0.1f)
        {
            //coroutina que pausa a patrulha
            StartCoroutine(PausaPatrulhaInimigo());
        }
        //VerificaVisibilidadePlayer();
    }

    public void Flip()
    {

        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }


    IEnumerator PausaPatrulhaInimigo()
    {
        estadoAtual = EstadoDoInimigo.Esperando;

        inimigo.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(2f);
        indiceDestinoAtual++;
        if (indiceDestinoAtual == posicoes.Length)
        {
            indiceDestinoAtual = 0;
        }
        Flip();
        estadoAtual = EstadoDoInimigo.Patrulhando;
    }

    public void VerificaVisibilidadePlayer()
    {
        if (isMorreu == true)
        {
            return;
        }

        Vector2 direcao = transform.localScale.x < 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(rayCastLine.position, direcao, 1.5f, layerMask);

        Debug.DrawRay(rayCastLine.position, direcao * 1.5f, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player") && podeAtirar)
        {
            //vai atacar o player e faz uma pequena parada
            tempoSemVerPlayer = 0f;
            if (!playerVisivel)
            {
                inimigo.linearVelocity = Vector2.zero;
                estadoAtual = EstadoDoInimigo.Atacando;
                playerVisivel = true;
            }

        }
        else 
        {
            if (playerVisivel)
            {
                tempoSemVerPlayer += Time.deltaTime;
                if (tempoSemVerPlayer >= tempoParaDesistir)
                {
                    estadoAtual = EstadoDoInimigo.Patrulhando;
                    playerVisivel = false;
                }
            }
        }

    }

    public void AtacandoPlayer()
    {
        if (!podeAtirar) { return; }
        
        StartCoroutine(TempoDisparo());
    }


    IEnumerator TempoDisparo()
    {
        podeAtirar = false;
        Vector2 direcao = transform.localScale.x < 0 ? Vector2.right : Vector2.left;
        print(direcao);
        GameObject temp = Instantiate(municao, arma.transform.position, Quaternion.identity);
        temp.GetComponent<Rigidbody2D>().linearVelocityX = velocidadeDisparo * direcao.x;
        if (direcao.x < 0)
        {
            Vector3 escala = temp.transform.localScale;
            escala.x = Mathf.Abs(escala.x);
            temp.transform.localScale = escala;
        }
        else
        {
            Vector3 escala = temp.transform.localScale;
            escala.x = -Mathf.Abs(escala.x);
            temp.transform.localScale = escala;
        }
        _enemyAnimations.AnimacaoDeAtaque("isAttack");
        yield return new WaitForSeconds(intervaloDeAtaque);
        podeAtirar = true;
    }


    public void Inicializar(Transform[] pontos)
    {
        posicoes = pontos;
        inicializado = true;
    }

}
