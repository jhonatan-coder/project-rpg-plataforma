using System.Collections;
using UnityEngine;

[System.Serializable]
public class ParDePortais
{
    public Transform entrada;
    public Transform saida;
}


public class Portal : MonoBehaviour
{
    public static Portal instance;

    [SerializeField] private ParDePortais[] portais;
    [SerializeField] private Transform player;

    private bool noEntrada = false;
    private bool noSaida = false;

    private bool podeTeleportar = true;
    private bool controleAtivado = false;

    private ParDePortais portalAtual;

    public bool ControleAtivado { get => controleAtivado; set => controleAtivado = value; }


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        else
        {
            Debug.LogWarning("Player não encontrado na cena, selecione a tag 'Player'.");
        }
    }

    private void Update()
    {
        if (ControleAtivado == true && Input.GetKeyDown(KeyCode.E) && podeTeleportar == true && portalAtual != null)
        {
            if (portalAtual.entrada != null && portalAtual.saida != null)
            {
                if (Vector2.Distance(player.position, portalAtual.entrada.position) < 1f)
                {
                    
                    StartCoroutine(TeleportarSaida(player, portalAtual.saida));
                }
                else if (Vector2.Distance(player.position, portalAtual.saida.position) < 1f)
                {

                    StartCoroutine(TeleportarSaida(player, portalAtual.entrada));

                }
            }
        }
        
    }

    public void JogadorNoPortal(Transform portal)
    {
        
        foreach (var par in portais)
        {
            if (par.entrada == portal || par.saida == portal )
            {
                portalAtual = par;
                break;
            }
        }
    }

   
    private IEnumerator TeleportarSaida(Transform player, Transform destino)
    {
        podeTeleportar = false;
        yield return new WaitForSeconds(2f);
        player.position = destino.position;
        yield return new WaitForSeconds(1f);
        podeTeleportar = true;
    }
}
