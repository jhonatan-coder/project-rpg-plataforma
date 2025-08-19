using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform spawnInicial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
    void OnSceneLoad(Scene cena, LoadSceneMode modo)
    {

        if (spawnInicial == null)
        {
            GameObject spawnObj = GameObject.Find("PosicaoinicialPlayer");//pega automaticamente o objeto da posi��o inicial

            if (spawnObj != null)
            {
                spawnInicial = spawnObj.transform;
            }
        }
        //verifica se ja jogou essa fase antes
        if (SaveSystem.dados != null && SaveSystem.dados.cenasVisitadas.Contains(cena.name))
        {
            //J� jogou antes -> volta para a posi��o salva
            transform.position = SaveSystem.dados.posicaoJogador.ToVector3();
            Debug.Log("Player voltou para posi��o salva: " + transform.position);
        }
        else if(spawnInicial != null)
        {
            transform.position = spawnInicial.position;
            Debug.Log("Player iniciou na posi��o inicial: " + spawnInicial.position);
        }

    }


}
