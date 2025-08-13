using System.Collections;
using UnityEngine;

public class InstanciarInimigo : MonoBehaviour
{
    public GameObject prefabDoInimigo;
    public Transform posicaoDeInstance;

    private GameObject inimigoAtual;

    private float tempoDeRespawn = 10f;
    private float tempoAtual = 0;

    void Start()
    {
        InstanciandoNovoInimigo();
    }

    // Update is called once per frame
    void Update()
    {
        if (inimigoAtual == null)
        {
            tempoAtual += Time.deltaTime;
            if (tempoAtual >= tempoDeRespawn) 
            {
                InstanciandoNovoInimigo();
                tempoAtual = 0f;
            }
        }
    }

    public void InstanciandoNovoInimigo()
    {
        GameObject temp = Instantiate(prefabDoInimigo, posicaoDeInstance.position, Quaternion.identity);
        inimigoAtual = temp;

        EnemyControll controle = temp.GetComponent<EnemyControll>();

        Transform paiDosPontos = transform.Find("PontosDePatrulha");
        Transform[] pontos = null;
        if (paiDosPontos != null)
        {
            pontos = new Transform[paiDosPontos.childCount];

            for (int i = 0; i < paiDosPontos.childCount; i++)
            {
                pontos[i] = paiDosPontos.GetChild(i);
            }

        }

        Transform limiteEsquerdo = transform.Find("LimiteEsquerdo");
        Transform limiteDireito = transform.Find("LimiteDireito");

        controle.Inicializar(pontos, limiteEsquerdo, limiteDireito);
        controle.instanciador = this;
    }

}
