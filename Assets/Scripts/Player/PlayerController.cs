using System.Collections;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Referências internas")]
    private  PlayerAnimationController _playerAnimationController;
    private Rigidbody2D playerRig2D;

    [Header("Detecção do chão")]
    public LayerMask whatIsGrounded;
    public Transform chaoA;
    public Transform chaoB;
    [SerializeField] private float tamanhoRayCast = 0.05f;

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
        ColidindoComChao();
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

        //Correndo();

        if (horizontal < 0 && olhandoDireita == false)
        {
            Flip();
            //_playerAnimationController.AnimacaoCorrer(isRun);
        }
        else if(horizontal > 0 && olhandoDireita == true)
        {
            Flip();
        }

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

    //verifica se pode pular e utilizar o pulo duplo
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
            StartCoroutine(PuloDuplo());
            isDoubleJumping = false;
        }
    }
    
    IEnumerator PuloDuplo()
    {
        yield return new WaitForEndOfFrame();
        _playerAnimationController.AnimacaoPuloDuplo();

    }

    //verifica se esta caindo para ativar a animação de cair
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

    //verifica se esta se movimentando para ativar a animação de correr
    //controla o lado que o player ira virar
    public void Flip()
    {
        olhandoDireita = !olhandoDireita;

        float x = transform.localScale.x;

        x *= -1;
        

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    public void ColidindoComChao()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(chaoA.position, Vector2.down, tamanhoRayCast, whatIsGrounded);
        RaycastHit2D hit2 = Physics2D.Raycast(chaoB.position, Vector2.down, tamanhoRayCast, whatIsGrounded);
        
        // 1 << layer - verifica se o bit existe na minha layerMask
        // & whatIsGrounded verifica se esse bit existe na minha mascara
        // se for diferente de 0, então existe e é considerado chão
        if (hit1.collider != null && ((1 << hit1.collider.gameObject.layer) & whatIsGrounded) != 0 || 
            hit2.collider != null && ((1 << hit2.collider.gameObject.layer) & whatIsGrounded) != 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        Debug.DrawLine(chaoA.position, chaoA.position+Vector3.down * tamanhoRayCast, color);
        Debug.DrawLine(chaoB.position, chaoB.position+Vector3.down * tamanhoRayCast, color);
    }
}
