using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public static PlayerPos instance;
    [SerializeField]private AnimacaoCheckpoint animCheckpoint;
    private BoxCollider2D boxCol2D;
    [SerializeField] private GameObject player;
    private string checkpointID;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (transform.name.Contains("Checkpoint"))
        {
            animCheckpoint = GetComponent<AnimacaoCheckpoint>();
        }
        boxCol2D = GetComponent<BoxCollider2D>();
        checkpointID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + transform.name;
        
        Debug.Log(checkpointID);
        if (transform.name == "PosicaoInicialPlayer")
        {
            SaveSystem.dados.posicaoJogador = SerializableVector3.FromVector3(transform.position);
            player.transform.position = SaveSystem.dados.posicaoJogador.ToVector3();
        }
        else
        {
            //Se caso existir a key e essa key já foi ativada, então sera mantida a animação ao iniciar a mesma fase
            if (SaveSystem.dados.checkpointsAtivados.Contains(checkpointID))
            {
                //ja foi ativado antes, desativa o trigger e a animação
                boxCol2D.enabled = false;
                animCheckpoint.CheckPointAnimationPosLoading(true);
                /*if (animCheckpoint != null)
                {
                }*/
            }
        }

    }
    

    //salva a posição quando player chega na area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (transform.name.Contains("Checkpoint") && animCheckpoint != null)
        {
            animCheckpoint.CheckPointAnimation(true);
        }

        string cenaAtual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (!SaveSystem.dados.cenasVisitadas.Contains(cenaAtual))
        {
            SaveSystem.dados.cenasVisitadas.Add(cenaAtual);
        }

        SaveSystem.dados.cenaAtual = cenaAtual;
        SaveSystem.dados.posicaoJogador = SerializableVector3.FromVector3(collision.transform.position);
        if (!SaveSystem.dados.checkpointsAtivados.Contains(checkpointID))
        {
            SaveSystem.dados.checkpointsAtivados.Add(checkpointID);
        }

        boxCol2D.enabled = false;
        SaveSystem.Salvar();

    }

}
