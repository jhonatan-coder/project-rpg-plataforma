using System.Collections;
using UnityEngine;


public enum EstadoDoInimigo
{
    Patrulhando,
    Perseguindo,
    Atacando,
    Esperando
}


public class EnemyControll : MonoBehaviour
{
    private EnemyAnimations enemyAnim;
    private PlayerController _playerController;
    private Rigidbody2D inimigo;

    //private MorteInimigos _morteInimigos;

    public Transform rayCastLine;
    public Transform[] posicoes;

    public Transform areaDeDano;

    public float visaoDoInimigo;

    public float vidaInimigo;


    //limita até o ponto em que pode seguir o player
    public Transform limiteCantoDireito;
    public Transform limiteCantoEsquerdo;
    public float velocidadeInimigo;

    //Acerto ataque prefabDoInimigo
    public Transform areaAtaque;
    public float tamanhoAreaAtaque;
    private int indiceDestinoAtual;
    public LayerMask layerPlayer;

    //variaveis de controle para animação
    private bool inimigoCaminhando;
    private bool inimigoCorrendo;

    private bool inicializado = false;

    private EstadoDoInimigo estadoAtual;

    [HideInInspector] public InstanciarInimigo instanciador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

        _playerController = FindFirstObjectByType<PlayerController>();
        enemyAnim = GetComponentInChildren<EnemyAnimations>();
        inimigo = GetComponent<Rigidbody2D>();
        estadoAtual = EstadoDoInimigo.Patrulhando;
        indiceDestinoAtual = 1;
        inimigoCaminhando = true;
        inimigoCorrendo = false;

        if (limiteCantoDireito != null && limiteCantoEsquerdo != null && posicoes != null && posicoes.Length > 0)
        {
            inicializado = true;
        }

           
    }

    // Update is called once per frame
    void Update()
    {
        if(!inicializado)
        {
            return;
        }

        switch (estadoAtual)
        {
            case EstadoDoInimigo.Patrulhando:
                MovimentoDoInimigo();
                IdentificarPlayer();
                inimigoCaminhando = true;
                inimigoCorrendo = false;
                break;
            case EstadoDoInimigo.Perseguindo:
                StartCoroutine(IniciarAtaqueNoPlayer());
                IdentificarPlayer();
                inimigoCaminhando = false;
                inimigoCorrendo = true;
                break;
            case EstadoDoInimigo.Esperando:
                inimigoCaminhando = false;
                inimigoCorrendo = false;
                break;
        }

        
        
    }
    private void FixedUpdate()
    {
        Animacoes();
    }
    public void Animacoes()
    {
        enemyAnim.AnimacaoDeCaminhada(inimigoCaminhando);
        enemyAnim.AnimacaoDeCorrida(inimigoCorrendo);
    }

    public void MovimentoDoInimigo()
    {
        Vector2 direcao = (posicoes[indiceDestinoAtual].position - transform.position).normalized;
        
        inimigo.linearVelocity = direcao * velocidadeInimigo;
        
        float distancia = Vector2.Distance(transform.position, posicoes[indiceDestinoAtual].position);
        if (distancia < 0.1f)
        {
            StartCoroutine(PausaPatrulhaInimigo());
        }
    }

    //vai identificar o player e logo após, vai atacá-lo
    public void IdentificarPlayer()
    {
        //Essas três variaveis irão determinar o limite em que o player pode ser eprseguido
        float posXPlayer = _playerController.transform.position.x;
        float limiteDireito = limiteCantoDireito.position.x;
        float limiteEsquerdo = limiteCantoEsquerdo.position.x;

        //Fara a logica do limite, desta forma o prefabDoInimigo não ficara perseguindo o player para sempre
        if (posXPlayer < limiteEsquerdo || posXPlayer > limiteDireito)
        {
            if (estadoAtual == EstadoDoInimigo.Perseguindo)
            {
                estadoAtual = EstadoDoInimigo.Patrulhando;
            }
            return;
        }

        Vector2 direcao = transform.localScale.x < 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(rayCastLine.position, direcao, visaoDoInimigo, layerPlayer);

        Debug.DrawRay(rayCastLine.position, direcao * visaoDoInimigo, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            estadoAtual = EstadoDoInimigo.Perseguindo;
        }
        else if (estadoAtual == EstadoDoInimigo.Perseguindo)
        {
            estadoAtual = EstadoDoInimigo.Patrulhando;
        }

    }

    public void AreaAtaque()
    {
        
        bool hit = Physics2D.OverlapCircle(areaAtaque.position, tamanhoAreaAtaque, layerPlayer);

        if (hit)
        {
            Debug.Log("Player detectado");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(areaAtaque.position, tamanhoAreaAtaque);
    }

    public void Flip()
    {
        float x = transform.localScale.x;

        x *= -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    
    //Ao chegar no destino, sera feito a troca de pontos e uma pequena pausa para virar
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

    //Ao encontrar o player, fara uma pequena pausa e então sua velocidade aumentara
    IEnumerator IniciarAtaqueNoPlayer()
    {
        if (!inicializado)
        {
            yield break;
        }

        //Rigidbody2D enemy = prefabDoInimigo.GetComponent<Rigidbody2D>();
        inimigo.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Vector2 direcaoPlayer = (_playerController.transform.position - transform.position).normalized;
        
        float posXPlayer = _playerController.transform.position.x;
        float limiteDireita = limiteCantoDireito.position.x;
        float limiteEsquerda = limiteCantoEsquerdo.position.x;
        if (posXPlayer >= limiteEsquerda && posXPlayer <= limiteDireita)
        {
            inimigo.linearVelocity = new Vector2(direcaoPlayer.x * (velocidadeInimigo + 0.4f), 0);

        }
        else
        {
            estadoAtual = EstadoDoInimigo.Patrulhando;
        }

        
    }
    
    public void Inicializar(Transform[] pontos, Transform limiteEsquerdo, Transform limiteDireito)
    {
        posicoes = pontos;
        limiteCantoEsquerdo = limiteEsquerdo;
        limiteCantoDireito = limiteDireito;
        inicializado = true;
    }
}
