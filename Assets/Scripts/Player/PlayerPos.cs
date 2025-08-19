using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public static PlayerPos instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    

    //salva a posição quando player chega na area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SaveSystem.dados.posicaoJogador = SerializableVector3.FromVector3(collision.gameObject.transform.position);
            SaveSystem.Salvar();
        }
    }

}
