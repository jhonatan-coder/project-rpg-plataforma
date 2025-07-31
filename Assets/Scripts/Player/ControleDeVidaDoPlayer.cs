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
        PlayerPos.instance.LoadPositionPlayer();
    }

    IEnumerator ResetaPlayer()
    {
        if (_playerController == null)
        {
            _playerController = FindFirstObjectByType<PlayerController>();
            if (_playerController == null)
            {
                Debug.LogError("PlayerController não encontrado");
                yield break;
            }
        }
        print("Player desativado");
        _playerController.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);        
        print("Player ativado");
        _playerController.gameObject.SetActive(true);

    }

}
