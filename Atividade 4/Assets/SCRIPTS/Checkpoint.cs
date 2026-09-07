using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public Transform centroCheckpoint;

    private bool ativado;

    public static bool checkpointAtivado;
    public static int moedasNoCheckpoint;

    public static List<string> moedasColetadasCheckpoint =
        new List<string>();

    public static Vector3 posicaoCheckpoint;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("CarregandoSave", 0) == 1)
        {
            checkpointAtivado =
                PlayerPrefs.GetInt("CheckpointAtivado", 0) == 1;

            moedasNoCheckpoint =
                PlayerPrefs.GetInt("MoedasCheckpoint", 0);

            moedasColetadasCheckpoint.Clear();

            string moedas =
                PlayerPrefs.GetString(
                    "MoedasColetadasCheckpoint",
                    ""
                );

            if (!string.IsNullOrEmpty(moedas))
            {
                moedasColetadasCheckpoint.AddRange(
                    moedas.Split('|')
                );
            }
        }
    }

    private void Start()
    {
        posicaoCheckpoint = centroCheckpoint.position;

        if (PlayerPrefs.GetInt("CarregandoSave", 0) == 0)
        {
            checkpointAtivado = false;
            moedasNoCheckpoint = 0;
            moedasColetadasCheckpoint.Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || ativado)
            return;

        ativado = true;
        checkpointAtivado = true;

        moedasNoCheckpoint =
            GerenciadorMoedas.quantidadeMoedas;

        moedasColetadasCheckpoint =
            new List<string>(
                GerenciadorMoedas.moedasColetadas
            );

        posicaoCheckpoint =
            centroCheckpoint.position;

        Debug.Log(
            "Checkpoint ativado com " +
            moedasNoCheckpoint +
            " moedas."
        );

        DadosSave dados = new DadosSave();

        dados.nomeFase =
            SceneManager.GetActiveScene().name;

        dados.checkpointAtivado = true;

        dados.moedasNoCheckpoint =
            moedasNoCheckpoint;

        dados.moedasColetadasCheckpoint =
            new List<string>(
                moedasColetadasCheckpoint
            );

        GerenciadorSave.Instancia.Salvar(
            0,
            dados
        );
    }
}