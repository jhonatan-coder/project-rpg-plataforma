using UnityEngine;

public class PosicaoInicialNovaFase : MonoBehaviour
{
    private string startGameID;
    void Start()
    {
        startGameID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + transform.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            string cenaAtual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (!SaveSystem.dados.cenasVisitadas.Contains(cenaAtual))
            {
                SaveSystem.dados.cenasVisitadas.Add(cenaAtual);
            }

            SaveSystem.dados.posicaoJogador = SerializableVector3.FromVector3(collision.gameObject.transform.position);
            transform.GetComponent<BoxCollider2D>().enabled = false;
            if (!SaveSystem.dados.checkpointsAtivados.Contains(startGameID))
            {
                SaveSystem.dados.checkpointsAtivados.Add(startGameID);
            }

            Debug.Log("Script:[PosicaoInicialPlayer] Posicao salva do player" + SaveSystem.dados.posicaoJogador);
            SaveSystem.Salvar();
        }
    }
}
