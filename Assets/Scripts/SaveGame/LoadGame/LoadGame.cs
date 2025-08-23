using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private void Start()
    {
        print(SaveSystem.dados.cenaAtual);
    }
    public void btnLoadGame()
    {
        StartGame.isNewGame = false;
        SaveSystem.Carregar();
        if (!string.IsNullOrEmpty(SaveSystem.dados.cenaAtual))
        {
            SceneManager.LoadScene(SaveSystem.dados.cenaAtual);
            PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = false;
            PlayerController.instance.GetComponent<SpriteRenderer>().enabled = true;
            PlayerController.instance.GetComponent<Rigidbody2D>().gravityScale = 1;
            PlayerController.instance.GetComponent<Rigidbody2D>().linearVelocityY = 0;
            ControleDeVidaDoPlayer.instance.IsDeath = false;
        }
        else
        {
            Debug.LogWarning("Nenhum save encontrado");
        }
    }
}
