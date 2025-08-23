using System.Collections.Generic;
using UnityEngine;

public class ControleSerra : MonoBehaviour
{
    [SerializeField] private Transform[] pontos;
    [SerializeField] private Transform serra;
    [SerializeField] private float velocidade = 0.5f;

    private int indexAtual = 0;
    private int direcao = 1;
    // StartFase is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var componentes = GetComponentsInChildren<Transform>();
        List<Transform> listaPontos = new List<Transform>();
        foreach(Transform t in componentes)
        {
            if (t.CompareTag("PontoCaminhoSerra"))
            {
                listaPontos.AddRange(t.GetComponentsInChildren<Transform>());
            }
        }
        pontos = listaPontos.ToArray();

        serra = GameObject.Find("Serra-controlada").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoSerra();
    }

    public void MovimentoSerra()
    {
        if (pontos.Length == 0)
        {
            return;
        }
        serra.transform.position = Vector2.MoveTowards(serra.transform.position, pontos[indexAtual].position, velocidade * Time.deltaTime);
        if (Vector2.Distance(serra.transform.position, pontos[indexAtual].position) < 0.01f)
        {
            indexAtual+=direcao;
            //Debug.LogError("Index atual é: "+indexAtual);
            if (indexAtual >= pontos.Length)
            {
                indexAtual = pontos.Length - 2; // volta para o penultimo ponto
                //Debug.LogError("Index atual após chegar no fim é: " + indexAtual);
                direcao = -1;
            }
            else if (indexAtual < 0)
            {
                indexAtual = 1;
                direcao = 1;
            }
        }
    }
}
