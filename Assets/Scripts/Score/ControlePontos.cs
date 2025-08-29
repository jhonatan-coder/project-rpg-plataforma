using UnityEngine;

public class ControlePontos : MonoBehaviour
{
    public static ControlePontos instance;
    public int totalScore;
    // PrimeiraVezJogando is called once before the first execution of Update after the MonoBehaviour is created
    // PrimeiraVezJogando is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SaveSystem.Carregar();
        if (SaveSystem.dados != null)
        {
            totalScore = SaveSystem.dados.score;
        }
        else
        {
            totalScore = 0;
        }
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
