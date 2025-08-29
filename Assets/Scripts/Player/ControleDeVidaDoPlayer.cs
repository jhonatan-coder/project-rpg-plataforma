using System.Collections;
using UnityEngine;

public class ControleDeVidaDoPlayer : MonoBehaviour
{
    public static ControleDeVidaDoPlayer instance;

    private PlayerController _playerController;

    public int life;

    private bool isDeath;

    private bool invencivel = false;
    public bool IsDeath { get => isDeath; set => isDeath = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        if (invencivel)
        {
            return;
        }
        life--;
        _playerController.GetComponent<PlayerAnimationController>().AnimacaoTomandoDano();

        if (life < 0)
        {
            life = 0;
            SaveSystem.Salvar();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            
            DesativaPlayerPermanente();
            return;
        }
        StartCoroutine(InvencibilidadeTemporaria());//aplica invencibilidade
        StartCoroutine(RespawnNoCheckpoint());

        SaveSystem.dados.vidasExtras = life;
        SaveSystem.Salvar();
        print("Player tem apenas "+life+" vidasExtras.");
    }

    IEnumerator RespawnNoCheckpoint()
    {
        yield return new WaitForSeconds(0.05f);
        PlayerController.instance.transform.position = SaveSystem.dados.posicaoJogador.ToVector3();

    }
    //impede de sofrer danos multiplos de armadilhas
    IEnumerator InvencibilidadeTemporaria()
    {
        invencivel = true;
        yield return new WaitForSeconds(0.2f);
        invencivel = false;
    }

    public void DesativaPlayerPermanente()
    {

        PlayerController.instance.GetComponent<SpriteRenderer>().enabled = false;
        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = true;
        PlayerController.instance.GetComponent<Rigidbody2D>().gravityScale = 0;
        PlayerController.instance.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        IsDeath = true;
    }
}
