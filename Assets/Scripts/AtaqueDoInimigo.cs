using System.Collections;
using UnityEngine;

public class AtaqueDoInimigo : MonoBehaviour
{//Acerto ataque prefabDoInimigo
    public Transform areaAtaque;
    public float tamanhoAreaAtaque;
    public LayerMask layerPlayer;
    private bool isAtacou;
    private void Start()
    {
        if (areaAtaque == null)
        {
            areaAtaque = GameObject.Find("AreaDeAcerto").GetComponent<Transform>();
        }
        isAtacou = false;
    }

    private void Update()
    {
        AreaAtaque();
    }

    public void AreaAtaque()
    {

        bool hit = Physics2D.OverlapCircle(areaAtaque.position, tamanhoAreaAtaque, layerPlayer);

        if (hit)
        {
            Debug.Log("Player detectado");
            StartCoroutine(TempoDeAtaque());
        }
    }

    IEnumerator TempoDeAtaque()
    {
        if (isAtacou == false)
        {
            ControleDeVidaDoPlayer.instance.DanoNoPlayer();
            isAtacou = true;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        isAtacou = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(areaAtaque.position, tamanhoAreaAtaque);
    }
}
