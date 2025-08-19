using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal instance;

    [SerializeField] private Transform portalSaida;
    [SerializeField] private Transform portalEntrada;
    [SerializeField] private Transform player;

    private bool noEntrada = false;
    private bool noSaida = false;
    private bool podeTeleportar = true;
    private bool controleAtivado = false;

    public bool ControleAtivado { get => controleAtivado; set => controleAtivado = value; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        portalSaida = GameObject.Find("Portal_Saida").GetComponentInChildren<Transform>();
        portalEntrada = GameObject.Find("Portal_Entrada").GetComponentInChildren<Transform>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (ControleAtivado == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && podeTeleportar == true)
            {
                if (noEntrada)
                {
                    StartCoroutine(TeleportarSaida(player, portalSaida));

                }
                if (noSaida)
                {
                    StartCoroutine(TeleportarSaida(player, portalEntrada));

                }
            }
        }
        
    }

    public void JogadorNoPortal(PortalTrigger.TipoPortal tipo, bool entrou)
    {
        if (tipo == PortalTrigger.TipoPortal.Entrada)
        {
            noEntrada = entrou;
        }
        else if (tipo == PortalTrigger.TipoPortal.Saida)
        {
            noSaida = entrou;
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
