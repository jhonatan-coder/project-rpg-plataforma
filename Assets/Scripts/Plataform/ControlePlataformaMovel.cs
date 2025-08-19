using System.Collections;
using UnityEngine;

public class ControlePlataformaMovel : MonoBehaviour
{

    [SerializeField] private Transform pontoPosicaoCima;
    [SerializeField] private Transform pontoPosicaoBaixo;
    [SerializeField] private float velocidadePlataforma;
    [SerializeField] private float tempoParado = 2f;

    private Rigidbody2D rig2D;

    private bool subindo = true;
    private bool parado = false;

    private void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(parado)
        {
            return;
        }

        Vector2 destino = subindo ? pontoPosicaoCima.position : pontoPosicaoBaixo.position;
        rig2D.MovePosition(Vector2.MoveTowards(rig2D.position, destino, velocidadePlataforma * Time.fixedDeltaTime));

        if ((Vector2)rig2D.position == destino)
        {
            parado = true;
            Invoke(nameof(MudarDirecao), tempoParado);
        }


    }
    private void MudarDirecao()
    {
        subindo = !subindo;
        parado = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }

}
