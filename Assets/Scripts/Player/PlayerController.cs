using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Referências internas")]
    private  PlayerAnimationController _playerAnimationController;
    private Rigidbody2D playerRig2D;

    [Header("Detecção do chão")]
    public Transform detectaChao;
    public LayerMask whatIsGrounded;

    //objetos para detectar se esta na parede correta para grudar
    [Header("Configuração de movimentos")]
    public float velocidadePlayer;
    public float forcaPuloPlayer;
    [Header("Controle de movimentos")]
    private float horizontal;
    private bool olhandoDireita;

    //estados do jogador
    private bool isGrounded;
    private bool isJumping;
    private bool isDoubleJumping;
    private bool isFalling;
    private bool isRun;

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsFalling { get => isFalling; set => isFalling = value; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRig2D = GetComponent<Rigidbody2D>();
        _playerAnimationController = FindFirstObjectByType<PlayerAnimationController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Movimentar();
        if (Input.GetButtonDown("Jump"))
        {
            Pulando();
        }

        Caindo();
        
    }

    //movimentação do personagem
    public void Movimentar()
    {
        horizontal = Input.GetAxis("Horizontal");
        playerRig2D.linearVelocityX = horizontal * velocidadePlayer * Time.deltaTime;
        Correndo();
        if (horizontal < 0 && olhandoDireita == false)
        {
            Flip();
            //_playerAnimationController.AnimacaoCorrer(isRun);
        }
        else if(horizontal > 0 && olhandoDireita == true)
        {
            Flip();
        }
    }
    public void Pulando()
    {
        if (IsGrounded == true && IsJumping == false)
        {

            playerRig2D.AddForce(Vector2.up * forcaPuloPlayer, ForceMode2D.Impulse);
            _playerAnimationController.AnimacaoPulo();
            IsJumping = true;
            isDoubleJumping = true;

        }
        else if (isDoubleJumping == true)
        {
            playerRig2D.AddForce(Vector2.up * forcaPuloPlayer, ForceMode2D.Impulse);
            _playerAnimationController.AnimacaoPuloDuplo();
            isDoubleJumping = false;
        }
    }

    public void Caindo()
    {
        IsFalling = !IsGrounded && playerRig2D.linearVelocityY < 0.01f;
        if (IsFalling)
        {
            _playerAnimationController.AnimacaoCaindo(true);

        }
        else if (IsGrounded)
        {
            _playerAnimationController.AnimacaoCaindo(false);
        }
    }
    public void Correndo()
    {
        if (playerRig2D.linearVelocityX != 0)
        {
            isRun = true;

        }
        else
        {
            isRun = false;
        }
        _playerAnimationController.AnimacaoCorrer(isRun);
    }
    //controla o lado que o player ira virar
    public void Flip()
    {
        olhandoDireita = !olhandoDireita;

        float x = transform.localScale.x;

        x *= -1;
        

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            print("Colidindo com o chão");
            IsJumping = false;
            IsFalling = false;
            _playerAnimationController.AnimacaoCaindo(IsFalling);
        }
      
    }
}
