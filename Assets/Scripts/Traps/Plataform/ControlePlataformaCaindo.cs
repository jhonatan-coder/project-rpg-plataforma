using System.Collections;
using UnityEngine;


public class ControlePlataformaCaindo : MonoBehaviour
{
    [SerializeField] private Transform plataforma;
    private Rigidbody2D rig2D;
    public LayerMask layerPlataforma;

    private Vector3 posicaoAtual;

    private Coroutine corrotinaAtual;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        plataforma = GetComponent<Transform>();

        rig2D.gravityScale = 0;
        rig2D.linearVelocity = Vector2.zero;
        posicaoAtual = plataforma.position;
        rig2D.bodyType = RigidbodyType2D.Kinematic;
    }

  
    public void QuedaPlataforma()
    {
        if (corrotinaAtual == null)
        {
            corrotinaAtual = StartCoroutine(Queda());
        }
        
    }

    IEnumerator Queda()
    {
        Debug.LogError("Esperando a queda");

        yield return new WaitForSeconds(1f);
        Debug.LogError("Começou a queda");
        rig2D.bodyType = RigidbodyType2D.Dynamic;
        rig2D.gravityScale = 1;
        yield return new WaitForSeconds(1f);
        Debug.LogError("Parou a queda");
        rig2D.gravityScale = 0;
        rig2D.linearVelocity = Vector2.zero;
        rig2D.bodyType = RigidbodyType2D.Kinematic;

        yield return StartCoroutine(Subida());
        
    }

    //corroutina responsável em mover a plataforma até sua posição inicial
    IEnumerator Subida()
    {
        Debug.LogError("Começou a Subir");

        //desativa a renderização e o collider um trigger para evitar colisões ao subir
        plataforma.GetComponent<SpriteRenderer>().enabled = false;
        plataforma.GetComponent<BoxCollider2D>().isTrigger = true;

        yield return new WaitForSeconds(1f);
        //posição inicial é reeutilizada
        Vector3 destino = posicaoAtual;
        //Sobe a plataforma até atingir a altura desejada
        while (Vector3.Distance(plataforma.position, destino) > 0.01f)
        {
            //nova posição é a posição inicial
            plataforma.position = Vector3.MoveTowards(plataforma.position, destino, 2f* Time.deltaTime);
            yield return null;
        }
        plataforma.GetComponent<SpriteRenderer>().enabled = true;
        plataforma.GetComponent<BoxCollider2D>().isTrigger = false;
        plataforma.position = destino;
        corrotinaAtual = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            QuedaPlataforma();
        }
    }
}
