using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static bool isNewGame = false;

    public void PrimeiraVezJogando(string nameScena)
    {
        isNewGame = true;

        SaveSystem.DeletarSave();   
        SceneManager.LoadScene(nameScena);
    }

    public void RetornaMenuInicial()
    {
        SceneManager.LoadScene("Tela-Inicial");
    }

    public void ReiniciaJogo()
    {
        SceneManager.LoadScene(SaveSystem.dados.cenaAtual);
        SaveSystem.Carregar();
    }

    public void ContinuaOJogo(string nameScena)
    {
        isNewGame = true;
        SaveSystem.DeletarSave();
        SceneManager.LoadScene(nameScena);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}
