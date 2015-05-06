using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityLimiter : MonoBehaviour
{
    [SerializeField] Vector2 m_velocityLimit;
    [SerializeField] Vector2 m_limiterStrength;
    Rigidbody2D m_rigidbody;


    void Start()
    { 
        m_rigidbody = GetComponent<Rigidbody2D>(); 
    }

	void FixedUpdate()
    {
        Limit(ref m_rigidbody, m_velocityLimit, m_limiterStrength);
	}


    public static void Limit(ref Rigidbody2D rb2D, Vector2 velocityLimit, Vector2 limiterStrength)
    {
        Vector2 breakForce;
        breakForce.x = breakForce.y = 0f;

        if (rb2D.velocity.x > velocityLimit.x)// velocity right
        {
            breakForce.x = -limiterStrength.x;
        }
        else if (rb2D.velocity.x < -velocityLimit.x)// velocity left
        {
            breakForce.x = limiterStrength.x;
        }
        if (rb2D.velocity.y > velocityLimit.y)// velocity up
        {
            breakForce.y = -limiterStrength.y;
        }
        else if (rb2D.velocity.y < -velocityLimit.y)// velocity down
        {
            breakForce.y = limiterStrength.y;
        }

        rb2D.AddForce(breakForce * Time.fixedDeltaTime, ForceMode2D.Force);
    }
}
