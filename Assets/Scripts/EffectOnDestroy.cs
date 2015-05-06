using UnityEngine;
using System.Collections;

public class EffectOnDestroy : MonoBehaviour
{
	[SerializeField] Transform m_effectPrefab = null;

	void OnDestroy()
	{
		Instantiate(m_effectPrefab, transform.position, Quaternion.identity);
	}
}
