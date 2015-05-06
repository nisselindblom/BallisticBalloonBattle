using UnityEngine;
using System.Collections;

public class LimitedLifetime : MonoBehaviour
{
	[SerializeField] float m_lifetime = 2f;
	float m_timer;

	void Start()
	{
		m_timer = 0f;
	}

	void Update()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= m_lifetime)
			DestroyObject(gameObject);
	}
}
