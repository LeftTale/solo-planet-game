using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            Debug.Log("Rocket Prox");
    }
}
