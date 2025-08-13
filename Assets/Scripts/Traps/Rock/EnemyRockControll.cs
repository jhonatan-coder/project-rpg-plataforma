using System.Collections;
using UnityEngine;

public class EnemyRockControll : MonoBehaviour
{
    private Rigidbody2D rig2D;
    //detecta o chão para ter a parada
    [SerializeField] private Transform collisionDown;
    //detecta o teto para ter a parada
    [SerializeField] private Transform collisionUp;
    [SerializeField] private GameObject areaDeAtaque;
    //quanto menor mais rápido sera a queda
    [SerializeField] private float gravidadeDeQuedaDaPedra;
    //quanto maior mais rápido sera a subida
    [SerializeField] private float gravidadeDeSubidaDaPedra;
    //tempo de queda e de subida
    [SerializeField] private float tempoDeQueda = 2f;
    [SerializeField] private float tempoDeSubida = 2f;

    public LayerMask layerMaskChao;
    public LayerMask layerMaskTeto;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Collision();
    }

    IEnumerator QuedaPedra()
    {
        yield return new WaitForSeconds(tempoDeQueda);
        areaDeAtaque.gameObject.SetActive(false);
        rig2D.gravityScale = gravidadeDeQuedaDaPedra;

    }
    IEnumerator SubidaPedra()
    {
        yield return new WaitForSeconds(tempoDeSubida);
        areaDeAtaque.gameObject.SetActive(true);
        rig2D.gravityScale = gravidadeDeSubidaDaPedra;

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
            StopCoroutine(SubidaPedra());

        }
        else if (hit02.collider != null && ((1 << hit02.collider.gameObject.layer) & layerMaskTeto) != 0)
        {
            StartCoroutine(SubidaPedra());
            StopCoroutine(QuedaPedra());
        }
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;

        Debug.DrawLine(collisionDown.position, collisionDown.position + Vector3.down * 0.05f, color);
        Debug.DrawLine(collisionUp.position, collisionUp.position + Vector3.up * 0.05f, color);

    }

}