using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class Arduino : MonoBehaviour
{
	[SerializeField] string m_COM = "COM#";
	SerialPort m_port;
	byte[] m_bytes;

	const int NUMBER_OF_PINS = 6;

	void Start()
	{
		m_port = new SerialPort(m_COM, 9600);
		m_port.ReadTimeout = 1;
		m_bytes = new byte[NUMBER_OF_PINS];
	}
	void OnApplicationQuit()
	{
		m_port.Close();
	}


	void Update()
	{
		if (!m_port.IsOpen) 
			m_port.Open();
		else 
			ReadData();

		Debug.Log(m_bytes[0] + " | " + m_bytes[1]);
	}


	void ReadData()
	{
		try
		{
			m_port.Read(m_bytes, 0, NUMBER_OF_PINS);
		}
		catch (System.Exception) {};
	}

	public byte[] GetData()
	{
		return m_bytes;
	}
}