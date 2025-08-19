using UnityEngine;

public class PosicaoinicialNovaFase : MonoBehaviour
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
            SaveSystem.dados.itensAtivados[startGameID] = false;
            SaveSystem.Salvar();
        }
    }
}
