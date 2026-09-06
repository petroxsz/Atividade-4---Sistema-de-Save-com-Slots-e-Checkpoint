using UnityEngine;

public class CameraSeguir : MonoBehaviour
{
    public Transform alvo;
    public float tempoSuavizacao = 0.2f;

    private Vector3 deslocamento;
    private Vector3 velocidadeAtual;

    private void Start()
    {
        deslocamento = transform.position - alvo.position;
    }

    private void LateUpdate()
    {
        Vector3 posicaoDesejada = alvo.position + deslocamento;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            posicaoDesejada,
            ref velocidadeAtual,
            tempoSuavizacao
        );
    }
}