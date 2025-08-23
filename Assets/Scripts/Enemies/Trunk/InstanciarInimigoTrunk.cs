using UnityEngine;

public class InstanciarInimigoTrunk : MonoBehaviour
{

    public GameObject prefabDoInimigo;
    public Transform pontoDeRespawn;
    private GameObject inimigoAtual;

    private float tempoDeRespawn = 10f;
    private float tempoAtual = 0;
    // StartFase is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RespawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (inimigoAtual == null)
        {
            tempoAtual += Time.deltaTime;
            if (tempoAtual >= tempoDeRespawn)
            {
                RespawnEnemy();
                tempoAtual = 0f;
            }
        }
    }


    public void RespawnEnemy()
    {
        GameObject temp = Instantiate(prefabDoInimigo, pontoDeRespawn.position, Quaternion.identity);
        //armazena o inimigo para controlar o fluxo de criação
        inimigoAtual = temp;
        EnemyTrunkController controle = temp.GetComponent<EnemyTrunkController>();
        // Procura um filho chamado PontosDePatrulhas, e então retorna os Transforms que estão ali dentro
        Transform paiDosPontos = transform.Find("PontosDePatrulhas");

        //Aqui vai ser criado um array do tamanho do número de filhos anteriormente armazenado
        Transform[] pontos = null;
        if (paiDosPontos != null)
        {
            //Array sera preenchido com todos os filhos do objeto paiDosPontos
            pontos = new Transform[paiDosPontos.childCount];
            for (int i = 0; i < paiDosPontos.childCount; i++)
            {
                pontos[i] = paiDosPontos.GetChild(i);
            }
        }
        //vai inicializar os pontos para poder ter o patrulhamento

        controle.Inicializar(pontos);
        controle.instanciador = this;
    }
}
