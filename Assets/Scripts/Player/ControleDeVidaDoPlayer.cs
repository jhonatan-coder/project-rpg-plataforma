using System.Collections;
using UnityEngine;

public class ControleDeVidaDoPlayer : MonoBehaviour
{
    public static ControleDeVidaDoPlayer instance;

    private PlayerController _playerController;

    public int life;

    private bool isDeath;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        //AtivaPlayer();
    }

    public void DanoNoPlayer()
    {
        life--;
        _playerController.GetComponent<PlayerAnimationController>().AnimacaoTomandoDano();
        //DesativaPlayer();
        if (life < 0)
        {
            //StartCoroutine(ResetaPlayer());
            life = 0;
            //Chama Game Over
            DesativaPlayerPermanente();
        }
        CheckPointManager.instance.CarregarPosicao(PlayerController.instance.transform);
        print("Player tem apenas "+life+" vidas.");
    }
    public void DesativaPlayerPermanente()
    {
        PlayerController.instance.GetComponent<SpriteRenderer>().enabled = false;
        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = true;
        PlayerController.instance.GetComponent<Rigidbody2D>().gravityScale = 0;
        PlayerController.instance.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
