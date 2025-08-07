using UnityEngine;

public class InstanciarInimigoTrunk : MonoBehaviour
{

    public GameObject prefabDoInimigo;
    public Transform pontoDeRespawn;
    private GameObject inimigoAtual;

    private float tempoDeRespawn = 10f;
    private float tempoAtual = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        inimigoAtual = temp;
        EnemyTrunkController controle = temp.GetComponent<EnemyTrunkController>();
        Transform paiDosPontos = transform.Find("PontosDePatrulhas");
        Transform[] pontos = null;

        if (paiDosPontos != null)
        {
            pontos = new Transform[paiDosPontos.childCount];
            for (int i = 0; i < paiDosPontos.childCount; i++)
            {
                pontos[i] = paiDosPontos.GetChild(i);
            }
        }
        controle.Inicializar(pontos);
        controle.instanciador = this;
    }
}
