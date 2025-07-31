using System.Collections;
using UnityEngine;

public class EnemyRockControll : MonoBehaviour
{
    private Rigidbody2D rig2D;
    [SerializeField] private Transform collisionDown;
    [SerializeField] private Transform collisionUp;
    [SerializeField] private GameObject areaDeAtaque;

    private float flutuacaoDaPedra = -0.05f;
    private float quedaDaPedra = 5f;

    public LayerMask layerMaskChao;
    public LayerMask layerMaskTeto;

    //private bool colidiu = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Collision();
        Ataque();
    }

    IEnumerator QuedaPedra()
    {
        StopCoroutine(SubidaPedra());
        yield return new WaitForSeconds(2f);
        rig2D.gravityScale = flutuacaoDaPedra;
    }
    IEnumerator SubidaPedra()
    {
        StopCoroutine(QuedaPedra());
        yield return new WaitForSeconds(2f);
        rig2D.gravityScale = quedaDaPedra;
    }


    public void Collision()
    {
        RaycastHit2D hit01 = Physics2D.Raycast(collisionDown.position, Vector2.down, 0.05f, layerMaskChao);
        RaycastHit2D hit02 = Physics2D.Raycast(collisionUp.position, Vector2.up, 0.05f, layerMaskTeto);

        //1 << layer - Se existe um bit na layerMask
        //& layerMask - Verifica se o bit existente é o mesmo da layermask
        // Se for diferente de 0, então o chao existe

        if (hit01.collider != null && ((1 << hit01.collider.gameObject.layer) & layerMaskChao) != 0)
        {
            StartCoroutine(QuedaPedra());
        }
        else if (hit02.collider != null && ((1 << hit02.collider.gameObject.layer) & layerMaskTeto) != 0)
        {
            StartCoroutine(SubidaPedra());
        }
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;

        Debug.DrawLine(collisionDown.position, collisionDown.position + Vector3.down * 0.05f, color);
        Debug.DrawLine(collisionUp.position, collisionUp.position + Vector3.up * 0.05f, color);

    }

    public void Ataque()
    {
        if (areaDeAtaque.gameObject.CompareTag("Player"))
        {
            print("Acertou o player");
        }
    }

    


}
