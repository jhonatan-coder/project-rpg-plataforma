using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public enum TipoPortal 
    { 
        Entrada,
        Saida
    }

    public TipoPortal tipoPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Portal.instance.JogadorNoPortal(tipoPortal, true);
            Portal.instance.ControleAtivado = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Portal.instance.JogadorNoPortal(tipoPortal, true);
            Portal.instance.ControleAtivado = false;
        }
    }
}
