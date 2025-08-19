using UnityEngine;

public class ControlePontos : MonoBehaviour
{
    public static ControlePontos instance;
    public int totalScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveSystem.DeletarSave();
            Debug.Log("Save deletado");
        }
    }

    /*public int SalvaScore(int value)
    {
        SaveSystem.dados.items += value;
        SaveSystem.Salvar();
        //PlayerPrefs.SetInt("totalScore", totalScore);

        return SaveSystem.dados.items;
    }*/

    
}
