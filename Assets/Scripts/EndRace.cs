using UnityEngine;

public class EndRace : MonoBehaviour
{   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Individual>())
        {
            collision.gameObject.GetComponentInParent<Individual>().ReachEndRace(collision.GetContact(0).point);
        }        
    }
}
