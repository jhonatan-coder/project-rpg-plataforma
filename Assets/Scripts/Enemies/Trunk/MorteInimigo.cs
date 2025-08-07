using UnityEngine;

public class MorteInimigo : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TempoDeDano());

            if (vidaInimigo < 0)
            {
                vidaInimigo = 0;
                _morteInimigos.Morreu();
                isMorreu = true;
            }
        }
    }

    IEnumerator TempoDeDano()
    {
        if (levandoDano) { yield break; }

        levandoDano = true;

        _enemyAnimations.AnimacaoDeHit("isHit");
        vidaInimigo--;
        yield return new WaitForSeconds(2f);
        levandoDano = false;
    }*/
}
