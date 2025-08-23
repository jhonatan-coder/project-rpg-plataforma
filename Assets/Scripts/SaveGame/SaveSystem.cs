using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/save.json";
    public static SaveData dados = new SaveData();

    public static void Salvar()
    {
        string json = JsonUtility.ToJson(dados, true);
        File.WriteAllText(savePath, json);
    }

    public static void Carregar()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            dados = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            dados = new SaveData();
            dados.vidasExtras = 3;
            Salvar();
            Debug.Log("[SaveSystem] Novo save criado.");
        }
    }

    public static void DeletarSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            dados = new SaveData();
        }
    }
}
