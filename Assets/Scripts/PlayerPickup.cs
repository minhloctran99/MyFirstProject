using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem"))
        {
            Debug.Log("Gem collected");
            Destroy(collision.gameObject);
        }
        else if (collision.name == "HazardZone" || collision.CompareTag("Hazard"))
        {
            Debug.Log("Player entered hazard");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "HazardZone" || collision.CompareTag("Hazard"))
        {
            Debug.Log("Player left hazard");
        }
    }
}