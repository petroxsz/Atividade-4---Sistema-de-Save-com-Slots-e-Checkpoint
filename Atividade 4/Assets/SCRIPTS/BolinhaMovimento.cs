using UnityEngine;
using UnityEngine.InputSystem;

public class BolinhaMovimento : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 7f;

    private Rigidbody2D rb;
    private Vector2 movimento;
    private bool estaNoChao;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimento.x * velocidade,
            rb.linearVelocity.y
        );
    }

    public void OnMovimento(InputValue valor)
    {
        movimento = valor.Get<Vector2>();
    }

    public void OnPular()
    {
        if (estaNoChao)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                forcaPulo
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = false;
        }
    }
}