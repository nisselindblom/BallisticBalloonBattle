using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    struct Layer
    {
        public string name;
        public Transform transform;
        [Range(0f, 1f)]
        public float distance;
    }
    [System.Serializable]
    struct Basis
    {
        public Vector3 position;
        public Vector3 scale;
    }

    [Header("Settings")]
    [SerializeField] Camera m_camera = null;
    [SerializeField][Range(0f, 0.2f)] float m_depth = 0.1f;
    [Header("Background")]
    [SerializeField] List<Layer> m_layers = null;
    Basis m_cameraBasis;
    Basis[] m_layersBasis;


    void Start()
    {
        // save start position/size for each layer as basis for the parallax effect
        m_layersBasis = new Basis[m_layers.Count];
        for (int i = 0; i < m_layersBasis.Length; i++)
        {
            m_layersBasis[i].position = m_layers[i].transform.position;
            m_layersBasis[i].scale = m_layers[i].transform.localScale;
        }
        // ... same with the camera
        m_cameraBasis.position = m_camera.transform.position;
        m_cameraBasis.scale = new Vector3(m_camera.orthographicSize, m_camera.orthographicSize, m_camera.orthographicSize);
    }


    void FixedUpdate()
    {
        Vector3 currentCameraScale = new Vector3(m_camera.orthographicSize, m_camera.orthographicSize, m_camera.orthographicSize);

        for (int i = 0; i < m_layers.Count; i++)
        {
            Vector3 newPos = m_layersBasis[i].position + (m_camera.transform.position - m_cameraBasis.position) * m_layers[i].distance;
            Vector3 newScale = m_layersBasis[i].scale + (currentCameraScale - m_cameraBasis.scale) * (m_layers[i].distance * m_depth);

            m_layers[i].transform.position = new Vector3(newPos.x, newPos.y, m_layers[i].transform.position.z);
            m_layers[i].transform.localScale = newScale;
        }
    }
}