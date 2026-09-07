using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject botaoContinuar;

    public GameObject painelMenu;
    public GameObject painelSlots;

    private void Start()
    {
        botaoContinuar.SetActive(
            GerenciadorSave.Instancia.SlotExiste(0)
        );

        painelMenu.SetActive(true);
        painelSlots.SetActive(false);
    }

    public void ContinuarJogo()
    {
        GerenciadorSave.Instancia.CarregarJogo(0);
    }

    public void NovoJogo()
    {
        PlayerPrefs.SetInt("CarregandoSave", 0);

        GerenciadorMoedas.ResetarMoedas();

        SceneManager.LoadScene("Fase1");
    }

    public void CarregarJogo()
    {
        painelMenu.SetActive(false);
        painelSlots.SetActive(true);
    }

    public void CarregarSlot1()
    {
        GerenciadorSave.Instancia.CarregarJogo(1);
    }

    public void CarregarSlot2()
    {
        GerenciadorSave.Instancia.CarregarJogo(2);
    }

    public void CarregarSlot3()
    {
        GerenciadorSave.Instancia.CarregarJogo(3);
    }

    public void Voltar()
    {
        painelSlots.SetActive(false);
        painelMenu.SetActive(true);
    }

    public void SairDoJogo()
    {
        Application.Quit();

        Debug.Log("Jogo encerrado.");
    }
}