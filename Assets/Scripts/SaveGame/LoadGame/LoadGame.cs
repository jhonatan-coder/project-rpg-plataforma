using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void btnLoadGame()
    {

        SaveSystem.Carregar();
        if (!string.IsNullOrEmpty(SaveSystem.dados.cenaAtual))
        {
            SceneManager.LoadScene(SaveSystem.dados.cenaAtual);
        }
        else
        {
            Debug.LogWarning("Nenhum save encontrado");
        }
    }
}
