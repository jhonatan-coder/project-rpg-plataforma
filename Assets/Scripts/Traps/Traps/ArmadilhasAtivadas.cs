using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilhasAtivadas : MonoBehaviour
{
    public SpriteRenderer[] spriteFogo;
    public CapsuleCollider2D[] colisoresFogo;

    private float time;
    // StartFase is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var fogos = GetComponentsInChildren<Transform>();

        List<SpriteRenderer> armadilhaSprite = new List<SpriteRenderer>();
        List<CapsuleCollider2D> armadilhaCollider = new List<CapsuleCollider2D>();

        foreach (Transform t in fogos)
        {
            if (t.name == "Fogo")
            {
                armadilhaSprite.AddRange(t.GetComponentsInChildren<SpriteRenderer>(true));
                armadilhaCollider.AddRange(t.GetComponentsInChildren<CapsuleCollider2D>(true));
            }
        }
        spriteFogo = armadilhaSprite.ToArray();
        colisoresFogo = armadilhaCollider.ToArray();

        StartCoroutine(CicloInfinito());
    }

    public void ArmadilhaAtivada()
    {
        for (int i = 0; i < spriteFogo.Length; i++)
        {
            spriteFogo[i].enabled = true;
            colisoresFogo[i].enabled = true;
        }
    }
    public void ArmadilhaDesativada()
    {

        for (int i = 0; i < spriteFogo.Length; i++)
        {
            spriteFogo[i].enabled = false;
            colisoresFogo[i].enabled = false;
        }
    }

    IEnumerator CicloInfinito()
    {
        while (true)
        {
            ArmadilhaAtivada();
            yield return new WaitForSeconds(5f);
            ArmadilhaDesativada();
            yield return new WaitForSeconds(5f);
        }        
    }

}
