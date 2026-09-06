using TMPro;
using UnityEngine;

public class InterfaceMoedas : MonoBehaviour
{
    private TMP_Text textoMoedas;

    private void Awake()
    {
        textoMoedas = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GerenciadorMoedas.AoAlterarMoedas += AtualizarTexto;
    }

    private void OnDisable()
    {
        GerenciadorMoedas.AoAlterarMoedas -= AtualizarTexto;
    }

    private void Start()
    {
        AtualizarTexto(GerenciadorMoedas.quantidadeMoedas);
    }

    private void AtualizarTexto(int quantidade)
    {
        textoMoedas.text = "Moedas: " + quantidade;
    }
}