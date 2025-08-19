using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public static PlayerPos instance;
    private AnimacaoCheckpoint animCheckpoint;
    private BoxCollider2D boxCol2D;
    public Dictionary<string, bool> checkpointState = new Dictionary<string, bool>();

    private string checkpointID;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        animCheckpoint = GetComponent<AnimacaoCheckpoint>();
        boxCol2D = GetComponent<BoxCollider2D>();
        checkpointID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + transform.name;
      
        Debug.Log(checkpointID);

        //Se caso existir a key e essa key já foi ativada, então sera mantida a animação ao iniciar a mesma fase
        if (SaveSystem.dados.itensAtivados.ContainsKey(checkpointID) && 
            SaveSystem.dados.itensAtivados[checkpointID] == false)
        {
            //ja foi ativado antes, desativa o trigger e a animação
            boxCol2D.enabled = false;
            if (animCheckpoint != null)
            {
                animCheckpoint.CheckPointAnimationPosLoading(true);
            }
        }
    }
    

    //salva a posição quando player chega na area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animCheckpoint.CheckPointAnimation(true);

            string cenaAtual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (!SaveSystem.dados.cenasVisitadas.Contains(cenaAtual))
            {
                SaveSystem.dados.cenasVisitadas.Add(cenaAtual);
            }
            SaveSystem.dados.cenaAtual = cenaAtual;
            SaveSystem.dados.posicaoJogador = SerializableVector3.FromVector3(collision.gameObject.transform.position);
            SaveSystem.dados.itensAtivados[checkpointID] = false;
            boxCol2D.enabled = false;
            SaveSystem.Salvar();
        }
    }

}
