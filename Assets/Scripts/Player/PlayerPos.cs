using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public static PlayerPos instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform positionPlayer;
    private Vector3 salvaPosicaoPlayer; 
    void Start()
    {
        instance = this;
        positionPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //retorna o player a posição que foi salva
    public void LoadPositionPlayer()
    {
        positionPlayer.position = salvaPosicaoPlayer;
    }

    //salva a posição quando player chega na area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            salvaPosicaoPlayer = transform.position;
            print(salvaPosicaoPlayer);
        }
    }

}
