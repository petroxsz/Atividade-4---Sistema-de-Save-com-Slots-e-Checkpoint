using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ZonaVitoria : MonoBehaviour
{
    public GameObject painelVitoria;
    public TMP_Text textoVitoria;
    public string proximaFase;

    private bool venceu;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || venceu)
            return;

        venceu = true;

        painelVitoria.SetActive(true);

        int totalMoedas = FindObjectsByType<Moeda>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        ).Length;

        textoVitoria.text =
            "VITÓRIA!\nMoedas: " +
            GerenciadorMoedas.quantidadeMoedas +
            " / " +
            totalMoedas;

        FazerAutosave();
    }

    private void FazerAutosave()
    {
        DadosSave dados = new DadosSave();

        dados.nomeFase =
            string.IsNullOrEmpty(proximaFase)
            ? SceneManager.GetActiveScene().name
            : proximaFase;

        dados.checkpointAtivado = false;
        dados.moedasNoCheckpoint = 0;

        dados.moedasColetadasCheckpoint =
            new List<string>();

        GerenciadorSave.Instancia.Salvar(0, dados);

        Debug.Log("Autosave realizado ao concluir a fase.");
    }

    private void Update()
    {
        if (!venceu)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.enterKey.wasPressedThisFrame)
        {
            GerenciadorMoedas.ResetarMoedas();

            PlayerPrefs.SetInt("CarregandoSave", 0);

            if (!string.IsNullOrEmpty(proximaFase))
            {
                SceneManager.LoadScene(proximaFase);
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}