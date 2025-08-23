using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject canvasPrefab;
    public GameObject playerPrefab;

    private GameObject playerInstance;
    private GameObject canvasInstance;

    // StartFase is called once before the first execution of Update after the MonoBehaviour is created
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
        if (!StartGame.isNewGame)
        {
            SaveSystem.Carregar();
        }

        //Verifica se a variavel player esta atribuída
        if (playerPrefab != null && playerInstance == null)
        {
            //caso esteja, procura na cena um gameObject com a tag player e armazena na variável player.
            playerInstance = GameObject.FindGameObjectWithTag("Player");
            if (playerInstance == null)
            {
                playerInstance = Instantiate(playerPrefab, SaveSystem.dados.posicaoJogador.ToVector3(), Quaternion.identity);
            }
        }

        if (canvasInstance == null && canvasPrefab != null)
        {
            canvasInstance = Instantiate(canvasPrefab);
            DontDestroyOnLoad(canvasInstance);
        }
    }

    private IEnumerator DelayInit()
    {
        yield return null;

        if (playerPrefab == null)
        {
            playerPrefab = GameObject.FindGameObjectWithTag("Player");
        }
    }

}
