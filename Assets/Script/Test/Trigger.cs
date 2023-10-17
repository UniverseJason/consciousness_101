using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject Block;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Block.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Block.SetActive(true);
        }
    }
}
