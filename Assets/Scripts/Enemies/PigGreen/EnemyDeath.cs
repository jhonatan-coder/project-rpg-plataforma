using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject objectHit;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int life = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TomarDano()
    {
        StartCoroutine(PausaEntreDanos());
        if (life <= 0)
        {
            //morte
        }
    }

    IEnumerator PausaEntreDanos()
    {
        life--;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(2f);
    }

    

}
