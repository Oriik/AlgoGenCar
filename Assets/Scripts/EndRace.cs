using UnityEngine;

public class EndRace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Individual>())
        {
            collision.gameObject.GetComponentInParent<Individual>().ReachEndRace();
        }
    }
}
