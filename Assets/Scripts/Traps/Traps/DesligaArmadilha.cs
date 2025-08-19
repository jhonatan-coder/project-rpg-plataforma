using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DesligaArmadilha : MonoBehaviour
{

    public SpriteRenderer[] spriteFogo;
    public CapsuleCollider2D[] coll2D;
    public GameObject acionadorDeArmadilha;
    [SerializeField] private Animator animTrap;

    public float areaDeContado;

    public LayerMask layerPlayer;

    private bool ativado;
    private bool isClicado = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var fogos = GetComponentsInChildren<Transform>(true);

        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        List<CapsuleCollider2D> colliders = new List<CapsuleCollider2D>();

        foreach (Transform t in fogos)
        {
            if (t.name == "Fogo")
            {
                sprites.AddRange(t.GetComponentsInChildren<SpriteRenderer>(true));
                colliders.AddRange(t.GetComponentsInChildren<CapsuleCollider2D>(true));
            }
        }

        spriteFogo = sprites.ToArray();
        coll2D = colliders.ToArray();

        animTrap = GameObject.Find("DesligaArmadilha").GetComponentInChildren<Animator>(true);
        ativado = true;
    }

    // Update is called once per frame
    void Update()
    {
        AreaDeAtivação();
        LigaDesligaArmadilhaFogo(ativado);
        ControleDeAnimação(ativado);
    }

    public void AreaDeAtivação()
    {
        bool hit = Physics2D.OverlapCircle(acionadorDeArmadilha.transform.position, areaDeContado, layerPlayer);

        if (hit == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && isClicado == true)
            {
                StartCoroutine(AtivandoDesativandoArmadilha());
            }
        }
    }
    IEnumerator AtivandoDesativandoArmadilha()
    {
        ativado = !ativado;
        isClicado = false;
        yield return new WaitForSeconds(4f);
        isClicado = true;
        ativado = !ativado;
    }
    public void ControleDeAnimação(bool value)
    {
        animTrap.SetBool("Ativado", value);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(acionadorDeArmadilha.transform.position, areaDeContado);
    }
    public void LigaDesligaArmadilhaFogo(bool ativado)
    {
        foreach (SpriteRenderer sprite in spriteFogo)
        {
            sprite.enabled = ativado;
        }
        foreach (Collider2D col in coll2D)
        {
            col.enabled = ativado;
        }
    }
}
