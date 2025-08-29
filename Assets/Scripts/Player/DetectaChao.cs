using UnityEngine;

public class DetectaChao : MonoBehaviour
{
    // PrimeiraVezJogando is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerController _playerController;
    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _playerController.IsGrounded = true;
            _playerController.IsFalling = false;
            _playerController.IsJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _playerController.IsGrounded = false; 
            _playerController.IsFalling = true;
            _playerController.IsJumping = true;
        }
    }


}
