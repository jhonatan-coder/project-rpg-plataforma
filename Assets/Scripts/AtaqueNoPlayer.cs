using UnityEngine;

public class AtaqueNoPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ControleDeVidaDoPlayer.instance.DanoNoPlayer();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ControleDeVidaDoPlayer.instance.DanoNoPlayer();

        }
    }

}
