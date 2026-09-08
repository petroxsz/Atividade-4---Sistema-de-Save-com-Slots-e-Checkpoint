using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorSave : MonoBehaviour
{
    public static GerenciadorSave Instancia;

    private string chave = "ChaveSave123";

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Salvar(int slot, DadosSave dados)
    {
        string json = JsonUtility.ToJson(dados);

        string textoCriptografado = Criptografar(json);

        string caminho = ObterCaminho(slot);

        File.WriteAllText(caminho, textoCriptografado);

        Debug.Log("Jogo salvo no slot " + slot);
        Debug.Log("Caminho: " + caminho);
    }

    public DadosSave Carregar(int slot)
    {
        string caminho = ObterCaminho(slot);

        if (!File.Exists(caminho))
        {
            Debug.Log("Slot " + slot + " vazio.");
            return null;
        }

        string textoCriptografado = File.ReadAllText(caminho);

        string json = Descriptografar(textoCriptografado);

        DadosSave dados = JsonUtility.FromJson<DadosSave>(json);

        return dados;
    }

    public void CarregarJogo(int slot)
{
    DadosSave dados = Carregar(slot);

    if (dados == null)
        return;

    if (slot != 0)
    {
        Salvar(0, dados);
    }

    PlayerPrefs.SetInt("CarregandoSave", 1);

    PlayerPrefs.SetInt(
        "CheckpointAtivado",
        dados.checkpointAtivado ? 1 : 0
    );

    PlayerPrefs.SetInt(
        "MoedasCheckpoint",
        dados.moedasNoCheckpoint
    );

    string moedas = string.Join(
        "|",
        dados.moedasColetadasCheckpoint
    );

    PlayerPrefs.SetString(
        "MoedasColetadasCheckpoint",
        moedas
    );

    SceneManager.LoadScene(dados.nomeFase);
}

public void ApagarSlot(int slot)
{
    string caminho = ObterCaminho(slot);

    if (File.Exists(caminho))
    {
        File.Delete(caminho);
        Debug.Log("Slot " + slot + " apagado.");
    }
}

    public bool SlotExiste(int slot)
    {
        return File.Exists(ObterCaminho(slot));
    }

    private string ObterCaminho(int slot)
    {
        return Path.Combine(
            Application.persistentDataPath,
            "save_slot_" + slot + ".dat"
        );
    }

    private string Criptografar(string texto)
    {
        byte[] dados = Encoding.UTF8.GetBytes(texto);
        byte[] chaveBytes = Encoding.UTF8.GetBytes(chave);

        for (int i = 0; i < dados.Length; i++)
        {
            dados[i] ^= chaveBytes[i % chaveBytes.Length];
        }

        return System.Convert.ToBase64String(dados);
    }

    private string Descriptografar(string texto)
    {
        byte[] dados = System.Convert.FromBase64String(texto);
        byte[] chaveBytes = Encoding.UTF8.GetBytes(chave);

        for (int i = 0; i < dados.Length; i++)
        {
            dados[i] ^= chaveBytes[i % chaveBytes.Length];
        }

        return Encoding.UTF8.GetString(dados);
    }
}