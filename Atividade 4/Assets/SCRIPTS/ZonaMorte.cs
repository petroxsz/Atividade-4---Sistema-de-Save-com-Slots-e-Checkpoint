using UnityEngine;

public class ZonaMorte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BolinhaRespawn respawn = other.GetComponent<BolinhaRespawn>();

            if (respawn != null)
            {
                respawn.Morrer();
            }
        }
    }
}