using System.Collections;
using UnityEngine;

public class ControleDeVidaDoPlayer : MonoBehaviour
{
    public static ControleDeVidaDoPlayer instance;

    private PlayerController _playerController;

    public int life = 3;

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
        _playerController = FindFirstObjectByType<PlayerController>();
    }

    public void DanoNoPlayer()
    {
        life--;
        StartCoroutine(ResetaPlayer());
        if (life <= 0)
        {
            life = 0;
            //Chama Game Over
        }
        print("Player tem apenas "+life+" vidas.");
        CheckPointManager.instance.CarregarPosicao(PlayerController.instance.transform);
    }

    IEnumerator ResetaPlayer()
    {
        DesativaPlayer();
        print("Player desativado");
        yield return new WaitForSeconds(2f);
        AtivaPlayer();
        print("Player ativado");

    }

    public void AtivaPlayer()
    {
        PlayerController.instance.GetComponent<SpriteRenderer>().enabled = true;
        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = false;
        PlayerController.instance.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void DesativaPlayer()
    {
        PlayerController.instance.GetComponent<SpriteRenderer>().enabled = false;
        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = true;
        PlayerController.instance.GetComponent<Rigidbody2D>().gravityScale = 0;
        PlayerController.instance.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
