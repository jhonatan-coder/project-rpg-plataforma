using UnityEngine;
using TMPro;
using System.Collections;

public class Creditos : MonoBehaviour
{

    public float delayTempo;

    private int idFrase;
    public string[] frases;
    public TMP_Text campoDeTexto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TempoDasFrases());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TempoDasFrases()
    {
        for (int idF = 0; idF < frases.Length; idF++)
        {
            campoDeTexto.text = "";
            for (int letra = 0; letra < frases[idF].Length; letra++)
            {
                campoDeTexto.text += frases[idF][letra];
                yield return new WaitForSeconds(delayTempo);
            }
            yield return new WaitForSeconds(0.6f);
        }
        
    }

}
