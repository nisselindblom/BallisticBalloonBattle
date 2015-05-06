using UnityEngine;
using System.Collections;


public class PlayerAirBalloon : MonoBehaviour
{
	[System.Serializable]
	struct ArduinoUNO
	{
		public Arduino arduino;
		[Range(0, 5)]
		public int pin;
		[Range(0, 254)]
		public int trim;
	}

	public enum InputMode
	{
		MicrophoneAndArduino,
		StandardUnityAxes
	}

    [Header("The Balloon")]
    [SerializeField] JellyBalloon m_jellyBalloon;
    [SerializeField] Rigidbody2D m_rigidBasket;
	[Header("Inputs")]
	[SerializeField] MicInput m_microphone;
	[SerializeField] ArduinoUNO m_arduino;
	[Header("Movement")]
	[SerializeField] float m_pumpForce;
	[SerializeField] float m_steeringSpeed;
	[SerializeField] float m_steeringDamping;
	[SerializeField] Vector2 m_speedLimits;
    [SerializeField] Vector2 m_limiterStrength;

	public float m_rotateForce;
	public float m_rotateDamping;

    [Header("Projectile")]
    [SerializeField] Rigidbody2D m_projectilePrefab;
    [SerializeField] float m_projectileForce;

    Rigidbody2D m_balloonRb2D;


    //...
    int m_hitpoints = 5;

	void Start()
    {
		//if (m_inputMode == InputMode.MicrophoneAndArduino)
		//{
		//	m_microphone.Start();
		//	//Arduino.Start(m_arduino.COM);
		//}
	}
	
	
	void FixedUpdate()
    {
		if (m_jellyBalloon.GetRigidbody2D())
		{
			Vector2 force = new Vector2(0, 0);

			//if (m_inputMode == InputMode.StandardUnityAxes)
			//{
			//	force.x = Input.GetAxis(m_steeringAxis);
			//	force.y = Mathf.Abs(Input.GetAxis(m_liftAxis));
			//}
			//if (m_inputMode == InputMode.MicrophoneAndArduino)
			//{
			//	//force.x = Arduino.Read() - m_arduino.zeroPosition;
			//	force.y = m_microphone.OutputVolume();
			//}

			force.x *= m_steeringSpeed;
			force.y *= m_pumpForce;


			// limit velocity
			if (Mathf.Abs(m_jellyBalloon.GetRigidbody2D().velocity.x) > m_speedLimits.x)
			{
				force.x -= (m_jellyBalloon.GetRigidbody2D().velocity.x - m_speedLimits.x) * m_limiterStrength.x;
			}
			if (m_jellyBalloon.GetRigidbody2D().velocity.y > m_speedLimits.y)
			{
				force.y -= (m_jellyBalloon.GetRigidbody2D().velocity.y - m_speedLimits.y) * m_limiterStrength.y;
			}

			// basket
			m_rigidBasket.GetComponent<Rigidbody2DStabilizer>().SetTargetRotation(m_jellyBalloon.GetRigidbody2D().rotation);
			m_rigidBasket.GetComponent<Rigidbody2DStabilizer>().SetTargetPosition(m_jellyBalloon.GetRigidbody2D().position + new Vector2(-.1f, -1.4f));

			Rigidbody2DStabilizer.MoveRotation(m_jellyBalloon.GetRigidbody2D(), -force.x*2, m_rotateForce, m_rotateDamping);

			m_jellyBalloon.GetRigidbody2D().AddForce(force, ForceMode2D.Force);

			// Hp
			UpdateHitpoints();
			if (m_hitpoints == 0) gameObject.SetActive(false);
		}
	}


	void UpdateHitpoints()
	{
		m_hitpoints = 5 - m_jellyBalloon.HoleCount();
		//Debug.Log(m_hitpoints);
	}


    public int Hitpoints() { return m_hitpoints; }
}
