using UnityEngine;

public class ControlePontos : MonoBehaviour
{
    public static ControlePontos instance;
    public int totalScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (SaveSystem.dados != null)
        {
            totalScore = SaveSystem.dados.score;
        }
        else
        {
            totalScore = 0;
        }
        SaveSystem.Carregar();
        totalScore = SaveSystem.dados != null ? SaveSystem.dados.score : 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveSystem.DeletarSave();
            Debug.Log("Save deletado");
        }
    }

    public void AdicionarPontos(int valor)
    {
        totalScore += valor;
        SaveSystem.dados.score = totalScore;
        SaveSystem.Salvar();
    }

    
}
