using System.Collections;
using UnityEngine;

public class AtaqueNoJogador : MonoBehaviour
{
    private bool ataqueOn = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ataqueOn)
        {

            StartCoroutine(AtaqueArmadilhas());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //ControleDeVidaDoPlayer.instance.DanoNoPlayer();
            StartCoroutine(AtaqueArmadilhas());
        }
    }

    IEnumerator AtaqueArmadilhas()
    {
        ControleDeVidaDoPlayer.instance.DanoNoPlayer();
        ataqueOn = false;
        yield return new WaitForSeconds(0.05f);
        ataqueOn = true;
    }
}
