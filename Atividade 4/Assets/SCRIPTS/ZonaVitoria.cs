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
    }

    private void Update()
    {
        if (!venceu)
            return;

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            GerenciadorMoedas.ResetarMoedas();

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