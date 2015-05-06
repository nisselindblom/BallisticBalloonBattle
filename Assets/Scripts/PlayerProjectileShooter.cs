using UnityEngine;
using System.Collections;

public class PlayerProjectileShooter : MonoBehaviour {

    // projectile
    public Rigidbody2D m_projectile = null;
    public float m_launchForce = 10.0f;

    // fire button
    public string m_fireButtonName = "";

    // collider exception
    public Collider2D[] m_colliderIgnore;

    // cooldown time
    public float m_cooldownTime = 0.5f;
    private float m_timer = 0.0f;

    // target
    public Transform m_target = null;

	void Start () {

	}

	void Update () {

        if (m_timer >= m_cooldownTime)
        {
            if (Input.GetButtonDown(m_fireButtonName))
            {
                Rigidbody2D projectile = Instantiate(m_projectile, transform.position, Quaternion.identity) as Rigidbody2D;

                foreach (Collider2D col in m_colliderIgnore) Physics2D.IgnoreCollision(col, projectile.GetComponent<Collider2D>());

                if (m_target.position.x > transform.position.x)     projectile.AddForce(transform.right * m_launchForce, ForceMode2D.Impulse);
                else                                                projectile.AddForce(-transform.right * m_launchForce, ForceMode2D.Impulse);

                m_timer = 0.0f;
            }
        }
        m_timer += Time.deltaTime;


		FaceSpriteTowardsTarget();
	}


	void FaceSpriteTowardsTarget()
	{
		if (m_target.position.x > transform.position.x)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		else
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
}
