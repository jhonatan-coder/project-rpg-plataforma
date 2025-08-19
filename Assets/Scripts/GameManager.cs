using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    void Awake()
    {
        //previne de haver duplicatas
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        //delay para dar tempo de carregar o que é necessario
        StartCoroutine(DelayInit());
        //--------------------------------------------//

        SaveSystem.Carregar();
        //Verifica se a variavel player esta atribuída
        if (player == null)
        {
            //caso esteja, procura na cena um gameObject com a tag player e armazena na variável player.
            player = GameObject.FindGameObjectWithTag("Player");
        }
        /*if (SaveSystem.dados.posicaoJogador.posX != 0 ||
            SaveSystem.dados.posicaoJogador.posY != 0 ||
            SaveSystem.dados.posicaoJogador.posZ !=0)
        {
            player.transform.position = SaveSystem.dados.posicaoJogador.ToVector3();
            SaveSystem.Salvar();
        }*/
    }

    private IEnumerator DelayInit()
    {
        yield return null;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

}
