using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityJellySprite))]
public class JellyBalloon : MonoBehaviour
{
    [SerializeField] string[] m_dangerousTags = null;
    [SerializeField] int m_holeCount = 0;

    void Start()
    {
	
	}
	
	void FixedUpdate()
    {
	
	}

    void OnJellyCollisionEnter2D(JellySprite.JellyCollision2D col)
    {
        foreach(string tag in m_dangerousTags)
        {
            if (col.Collision2D.gameObject.tag == tag)
            {
				m_holeCount++;
				Debug.Log(m_holeCount);
            }
        }
    }

    public int HoleCount() { return m_holeCount; }
    public Rigidbody2D GetRigidbody2D()
    {
        try
        {
            return GetComponent<UnityJellySprite>().ReferencePoints[0].transform.GetComponent<Rigidbody2D>();
        }
        catch (System.Exception)
        {
            return null;
        };
    }
}
