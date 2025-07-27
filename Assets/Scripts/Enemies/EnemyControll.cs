using System.Collections;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public GameObject inimigo;
    public float velocidadeInimigo;
    public Transform[] posicoes;

    private int idTarget;
    private bool esperando;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //inimigo = GetComponent<Rigidbody2D>();
        idTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (!esperando)
        {
            MovimentoDoInimigo();
        }

    }
    public void MovimentoDoInimigo()
    {
        Vector2 direcao = (posicoes[idTarget].position - inimigo.transform.position).normalized;

        inimigo.GetComponent<Rigidbody2D>().linearVelocity = direcao * velocidadeInimigo;

        float distancia = Vector2.Distance(inimigo.transform.position, posicoes[idTarget].position);
        if (distancia < 0.1f)
        {
            StartCoroutine(PausaPatrulhaInimigo());
        }
    }

    public void Flip()
    {
        float x = inimigo.transform.localScale.x;

        x *= -1;

        inimigo.transform.localScale = new Vector3(x, inimigo.transform.localScale.y, inimigo.transform.localScale.z);
    }

    IEnumerator PausaPatrulhaInimigo()
    {
        esperando = true;
        inimigo.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(2);

        idTarget++;
        if (idTarget == posicoes.Length)
        {
            idTarget = 0;
        }
        Flip();
        esperando = false;
    }



    /*public void MovimentoInimigo()
    {
        //pega a posição que ira ir - a própria e desta forma ele irá até o local com linearVelocity
        Vector2 direcao = (posicoes[idTarget].position - transform.position).normalized;
        inimigo.linearVelocity = direcao * velocidadeInimigo;
        //Captura a distância entre o inimigo e o ponto que esta indo
        float distancia = Vector2.Distance(transform.position, posicoes[idTarget].position);
        if (distancia < 0.1f)
        {
            //se for menor que 0.1, então inicia a pausa da patrulha
            StartCoroutine(PausaPatrulhaInimigo());
            
        }
    }

    public void Flip()
    {
        float x = transform.localScale.x;

        x *= -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);        
    }  

    IEnumerator PausaPatrulhaInimigo()
    {
        //Ao chegar no ponto, ira esperar e sua velocidade sera zero.
        esperando = true;
        inimigo.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(2f);
        //depois irá adicionar o proximo ponto
        idTarget++;
        //se caso o proximo ponto foir igual a quantidade de pontos existentes, seu indice sera zerado.
        if (idTarget == posicoes.Length)
        {
            idTarget = 0;
        }
        ////troca a direção que o inimigo ira andar
        Flip();
        esperando = false;
    }*/
    
}
