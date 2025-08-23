using UnityEngine;

public class PortalTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Portal.instance.JogadorNoPortal(this.transform);
            Portal.instance.ControleAtivado = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Portal.instance.JogadorNoPortal(null);
            Portal.instance.ControleAtivado = false;
        }
    }
}
