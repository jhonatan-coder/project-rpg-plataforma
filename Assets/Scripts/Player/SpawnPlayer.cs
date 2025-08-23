using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform spawnInicial;
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene cena, LoadSceneMode modo)
    {
        //serve para manter o script CheckPointManager ativado nos objetos.
        var checkpoint = GameObject.FindObjectsByType<CheckPointManager>(FindObjectsSortMode.None);
        foreach (var cp in checkpoint)
        {
            cp.enabled = true; // garante ativo
        }

        //verifica se ha algum plauyer na cena
        if (playerPrefab == null)
        {
            playerPrefab = GameObject.FindGameObjectWithTag("Player");
        }


        // Se spawnInicial não foi definido, procura um objeto na cena
        if (spawnInicial == null)
        {
            GameObject startObj = GameObject.Find("PosicaoInicialPlayer");
            if (startObj != null)
            {
                spawnInicial = startObj.transform;
            }
        }

        //só sobrescreve o save com a posição inicial se nenhum checkpoint estiver ativo
        bool temCheckpointAtivo = SaveSystem.dados.checkpointsAtivados.Exists(cp => cp.StartsWith(cena.name));
        if (!temCheckpointAtivo && spawnInicial != null)
        {
            SaveSystem.dados.cenaAtual = cena.name;
            SaveSystem.dados.posicaoJogador = SerializableVector3.FromVector3(spawnInicial.position);
            SaveSystem.Salvar();
        }

        // Verifica se já existe um player na cena
        playerInstance = GameObject.FindGameObjectWithTag("Player");

        Vector3 pos = SaveSystem.dados.posicaoJogador.ToVector3(); ;

        if (playerInstance == null)
        {
            // Instancia o player na cena
            playerInstance = Instantiate(playerPrefab, pos, Quaternion.identity);
        }
        else
        {
            playerInstance.transform.position = pos;
        }
    }


}