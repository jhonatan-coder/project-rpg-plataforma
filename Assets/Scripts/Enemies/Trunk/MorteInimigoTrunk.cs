using System.Collections;
using UnityEngine;

public class MorteInimigoTrunk : MonoBehaviour
{
    private int vidaInimigo;
    private bool levaDano;

    private EnemyAnimations _enemyAnimation;
    [SerializeField] private EnemyTrunkController _enemyControll;

    private void Start()
    {
        _enemyControll = GetComponentInParent<EnemyTrunkController>();
        _enemyAnimation = GetComponentInParent<EnemyAnimations>();
        levaDano = false;
        vidaInimigo = 2;
        _enemyControll.vidaInimigo = Mathf.Max(vidaInimigo, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector2();
                rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);

            }

            StartCoroutine(TempoDeDano());
            if (vidaInimigo <= 0)
            {
                vidaInimigo = 0;
                Destroy(_enemyControll.gameObject);


            }
        }
    }


    IEnumerator TempoDeDano()
    {
        if (levaDano) { yield break; }

        levaDano = true;
        vidaInimigo--;
        _enemyAnimation.AnimacaoDeHit("isHit");
        Debug.Log("prefabDoInimigo levou dano");
        yield return new WaitForSeconds(2f);

        levaDano = false;
    }

}


