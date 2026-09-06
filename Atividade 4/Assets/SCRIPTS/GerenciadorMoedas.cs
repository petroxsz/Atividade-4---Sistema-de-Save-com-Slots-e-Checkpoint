using System;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorMoedas : MonoBehaviour
{
    public static int quantidadeMoedas;

    public static List<string> moedasColetadas = new List<string>();

    public static event Action<int> AoAlterarMoedas;

    private void Start()
    {
        ResetarMoedas();
    }

    public static void AdicionarMoeda(string idMoeda)
    {
        quantidadeMoedas++;

        if (!moedasColetadas.Contains(idMoeda))
        {
            moedasColetadas.Add(idMoeda);
        }

        AoAlterarMoedas?.Invoke(quantidadeMoedas);
    }

    public static void ResetarMoedas()
    {
        quantidadeMoedas = 0;
        moedasColetadas.Clear();

        AoAlterarMoedas?.Invoke(quantidadeMoedas);
    }

    public static void AtualizarInterface()
{
    AoAlterarMoedas?.Invoke(quantidadeMoedas);
}
}