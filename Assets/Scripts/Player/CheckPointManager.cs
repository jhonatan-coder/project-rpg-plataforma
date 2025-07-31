using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    private Vector3 ultimaPosicaoSalva;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void SalvarPosicao(Vector3 novaPosicao)
    {
        ultimaPosicaoSalva = novaPosicao;
        Debug.Log("Checkpoint atualizado para: " + novaPosicao);
    }

    public void CarregarPosicao(Transform player)
    {
        player.position = ultimaPosicaoSalva;
    }
}
