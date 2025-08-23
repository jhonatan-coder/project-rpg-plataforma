using System.Collections;
using UnityEngine;

public class ControleDeVidaDoPlayer : MonoBehaviour
{
    public static ControleDeVidaDoPlayer instance;

    private PlayerController _playerController;

    public int life;

    private bool isDeath;

    public bool IsDeath { get => isDeath; set => isDeath = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SaveSystem.Carregar();     
            IsDeath = false;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       _playerController = GetComponent<PlayerController>();
       life = SaveSystem.dados.vidasExtras;
        
    }

    public void DanoNoPlayer()
    {
        life--;
        _playerController.GetComponent<PlayerAnimationController>().AnimacaoTomandoDano();

        if (life < 0)
        {
            life = 0;
            SaveSystem.Salvar();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            
            DesativaPlayerPermanente();
        }

        PlayerController.instance.transform.position = SaveSystem.dados.posicaoJogador.ToVector3();

        SaveSystem.dados.vidasExtras = life;
        SaveSystem.Salvar();
        print("Player tem apenas "+life+" vidasExtras.");
    }
    public void DesativaPlayerPermanente()
    {

        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = true;
        PlayerController.instance.GetComponent<SpriteRenderer>().enabled = false;
        PlayerController.instance.GetComponent<Rigidbody2D>().gravityScale = 0;
        PlayerController.instance.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        IsDeath = true;
    }
}
