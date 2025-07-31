using System.Collections;
using UnityEngine;


public enum EstadoDoInimigo
{
    Patrulhando,
    Perseguindo,
    Esperando
}


public class EnemyControll : MonoBehaviour
{
    public static EnemyControll instance;
    private EnemyAnimations enemyAnim;

    private PlayerController _playerController;
    public GameObject inimigo;

    public Transform rayCastLine;
    public Transform[] posicoes;

    public float velocidadeInimigo;

    private int indiceDestinoAtual;
    public LayerMask layerPlayer;

    //variaveis de controle para animação
    private bool inimigoCaminhando;
    private bool inimigoCorrendo;

    private bool inimigoAcertado = false;

    private EstadoDoInimigo estadoAtual;

    public bool InimigoAcertado { get => inimigoAcertado; set => inimigoAcertado = value; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _playerController = FindFirstObjectByType<PlayerController>();
        enemyAnim = GetComponentInChildren<EnemyAnimations>();
        estadoAtual = EstadoDoInimigo.Patrulhando;
        indiceDestinoAtual = 1;
        inimigoCaminhando = true;
        inimigoCorrendo = false;
    }

    // Update is called once per frame
    void Update()
    {
        

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
        Vector2 direcao = (posicoes[indiceDestinoAtual].position - inimigo.transform.position).normalized;
        
        inimigo.GetComponent<Rigidbody2D>().linearVelocity = direcao * velocidadeInimigo;
        
        float distancia = Vector2.Distance(inimigo.transform.position, posicoes[indiceDestinoAtual].position);
        if (distancia < 0.1f)
        {
            StartCoroutine(PausaPatrulhaInimigo());
        }
    }

    //vai identificar o player e logo após, vai atacá-lo
    public void IdentificarPlayer()
    {
        Vector2 direcao = inimigo.transform.localScale.x < 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(rayCastLine.position, direcao, 0.5f, layerPlayer);

        Debug.DrawRay(rayCastLine.position, direcao * 0.5f, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            estadoAtual = EstadoDoInimigo.Perseguindo;
        }
        else if(estadoAtual == EstadoDoInimigo.Perseguindo)
        {
            estadoAtual = EstadoDoInimigo.Patrulhando;
        }

    }



    public void Flip()
    {
        float x = inimigo.transform.localScale.x;

        x *= -1;

        inimigo.transform.localScale = new Vector3(x, inimigo.transform.localScale.y, inimigo.transform.localScale.z);
    }
    IEnumerator PausaVelocidade()
    {
        inimigo.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(1f);
    }
    //Ao chegar no destino, sera feito a troca de pontos e uma pequena pausa para virar
    IEnumerator PausaPatrulhaInimigo()
    {
        estadoAtual = EstadoDoInimigo.Esperando;

        inimigo.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

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
        Rigidbody2D enemy = inimigo.GetComponent<Rigidbody2D>();
        enemy.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Vector2 direcaoPlayer = (_playerController.transform.position - inimigo.transform.position).normalized;
        enemy.linearVelocity = new Vector2(direcaoPlayer.x * (velocidadeInimigo + 0.3f), 0);


    }



}
