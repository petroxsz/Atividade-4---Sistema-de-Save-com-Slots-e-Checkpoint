using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform centroCheckpoint;

    private bool ativado;

    public static bool checkpointAtivado;
    public static int moedasNoCheckpoint;
    public static List<string> moedasColetadasCheckpoint = new List<string>();
    public static Vector3 posicaoCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || ativado)
            return;

        ativado = true;
        checkpointAtivado = true;

        moedasNoCheckpoint = GerenciadorMoedas.quantidadeMoedas;

        moedasColetadasCheckpoint =
            new List<string>(GerenciadorMoedas.moedasColetadas);

        posicaoCheckpoint = centroCheckpoint.position;

        Debug.Log("Checkpoint ativado com " + moedasNoCheckpoint + " moedas.");
    }
}