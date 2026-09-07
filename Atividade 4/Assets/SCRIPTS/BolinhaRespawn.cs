using System.Collections.Generic;
using UnityEngine;

public class BolinhaRespawn : MonoBehaviour
{
    public Transform inicioFase;

    private void Start()
    {
        if (PlayerPrefs.GetInt("CarregandoSave", 0) == 1)
        {
            CarregarEstadoSalvo();
        }
        else
        {
            transform.position = inicioFase.position;
        }
    }

    public void Morrer()
    {
        if (Checkpoint.checkpointAtivado)
        {
            transform.position = Checkpoint.posicaoCheckpoint;

            GerenciadorMoedas.quantidadeMoedas =
                Checkpoint.moedasNoCheckpoint;

            GerenciadorMoedas.moedasColetadas =
                new List<string>(
                    Checkpoint.moedasColetadasCheckpoint
                );

            RestaurarMoedas();

            GerenciadorMoedas.AtualizarInterface();
        }
        else
        {
            transform.position = inicioFase.position;

            GerenciadorMoedas.ResetarMoedas();

            RestaurarMoedas();
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.zero;
    }

    private void CarregarEstadoSalvo()
    {
        if (Checkpoint.checkpointAtivado)
        {
            transform.position = Checkpoint.posicaoCheckpoint;

            GerenciadorMoedas.quantidadeMoedas =
                Checkpoint.moedasNoCheckpoint;

            GerenciadorMoedas.moedasColetadas =
                new List<string>(
                    Checkpoint.moedasColetadasCheckpoint
                );
        }
        else
        {
            transform.position = inicioFase.position;

            GerenciadorMoedas.ResetarMoedas();
        }

        RestaurarMoedas();

        GerenciadorMoedas.AtualizarInterface();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.zero;

        PlayerPrefs.SetInt("CarregandoSave", 0);
    }

    private void RestaurarMoedas()
    {
        Moeda[] moedas = FindObjectsByType<Moeda>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (Moeda moeda in moedas)
        {
            bool foiColetada =
                GerenciadorMoedas.moedasColetadas.Contains(
                    moeda.idMoeda
                );

            moeda.gameObject.SetActive(!foiColetada);
        }
    }
}