using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleFrutas : MonoBehaviour
{
    private Animator animFruits;

    private string itemID;
    public int pontos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animFruits = GetComponent<Animator>();
        itemID = SceneManager.GetActiveScene().name + "_" + transform.position.ToString();

        if (SaveSystem.dados.itensColetados.Contains(itemID))
        {
            Destroy(gameObject);
        }
        
    }

    public void FrutaColetada()
    {
        animFruits.SetTrigger("isCollected");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ControlePontos.instance != null)
            {
                ControlePontos.instance.AdicionarPontos(pontos);
            }
            else
            {
                Debug.LogWarning("ControlePontos.instance não encontrado!");
            }
            if (!SaveSystem.dados.itensColetados.Contains(itemID))
            {
                SaveSystem.dados.itensColetados.Add(itemID);
                SaveSystem.dados.items++;
                //SaveSystem.dados.score = ControlePontos.instance.totalScore;
                SaveSystem.Salvar();
            }

            Debug.Log(pontos + " Pontos adicionados");
            FrutaColetada();

            Debug.Log("Total de pontos: " + ControlePontos.instance.totalScore);
            Destroy(this.gameObject, 0.51f);

        }
    }
}
