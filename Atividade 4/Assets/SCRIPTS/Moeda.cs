using UnityEngine;

public class Moeda : MonoBehaviour
{
    public string idMoeda;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GerenciadorMoedas.AdicionarMoeda(idMoeda);

            gameObject.SetActive(false);
        }
    }
}