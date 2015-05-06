using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class DynamicCamera : MonoBehaviour
{
    [System.Serializable]
    struct Bounds
    {
        public Vector2 max;
        public Vector2 min;
    }

    [SerializeField] Transform[] m_targets = new Transform[2];
    [SerializeField] Bounds m_cameraBounds = new Bounds { max = new Vector2(40f, 20f), min = new Vector2(0f, 0f) };
    [SerializeField] float m_minCameraSize = 1f;
    [SerializeField][Range(0f, 1f)] float m_speed = .1f;


    void FixedUpdate()
    {
        // Zoom
        float maxCamSize = MinCameraSize(m_cameraBounds.min, m_cameraBounds.max);
        float targetsCamSize = MaxCameraSize(PointMax(m_targets), PointMin(m_targets));
        float sizePercentage = targetsCamSize / maxCamSize;
        float newCamSize = m_minCameraSize + (maxCamSize - m_minCameraSize) * sizePercentage;
        //
        newCamSize = Mathf.Clamp(newCamSize, m_minCameraSize, maxCamSize);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, newCamSize, Time.time * (m_speed / 50f));

        // Move
        Vector2 camPos = MidPoint(m_targets);
        Vector2 maxPos;
        maxPos.x = m_cameraBounds.max.x - newCamSize * GetComponent<Camera>().aspect;
        maxPos.y = m_cameraBounds.max.y - newCamSize;
        Vector2 minPos;
        minPos.x = m_cameraBounds.min.x + newCamSize * GetComponent<Camera>().aspect;
        minPos.y = m_cameraBounds.min.y + newCamSize;

        camPos.x = Mathf.Clamp(camPos.x, minPos.x, maxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, minPos.y, maxPos.y);

        Vector3 newCamPos = new Vector3(camPos.x, camPos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newCamPos, Time.time * (m_speed / 50f));
    }


    public static Vector2 PointMax(Transform[] targets)
    {
        Vector2 max = targets[0].transform.position;
        for (int i = 1; i < targets.Length; i++)
        {
            if (max.x < targets[i].transform.position.x) max.x = targets[i].transform.position.x;
            if (max.y < targets[i].transform.position.y) max.y = targets[i].transform.position.y;
        }
        return max;
    }
    public static Vector2 PointMin(Transform[] targets)
    {
        Vector2 min = targets[0].transform.position;
        for (int i = 1; i < targets.Length; i++)
        {
            if (min.x > targets[i].transform.position.x) min.x = targets[i].transform.position.x;
            if (min.y > targets[i].transform.position.y) min.y = targets[i].transform.position.y;
        }
        return min;
    }
    public static Vector3 MidPoint(Transform[] targets)
    {
        Vector3 midpoint = targets[0].position;
        for (int i = 1; i < targets.Length; i++)
        {
            midpoint += targets[i].position;
        }
        midpoint /= targets.Length;

        return midpoint;
    }
    float MaxCameraSize(Vector2 pointA, Vector2 pointB)
    {
        float distanceX = Mathf.Abs(pointA.x - pointB.x);
        float distanceY = Mathf.Abs(pointA.y - pointB.y);

        float sizeX = (distanceX / 2) / GetComponent<Camera>().aspect;
        float sizeY = distanceY / 2;

        float maxSize;
        if (sizeX > sizeY) maxSize = sizeX;
        else maxSize = sizeY;

        return maxSize;
    }
    float MinCameraSize(Vector2 pointA, Vector2 pointB)
    {
        float distanceX = Mathf.Abs(pointA.x - pointB.x);
        float distanceY = Mathf.Abs(pointA.y - pointB.y);

        float sizeX = (distanceX / 2) / GetComponent<Camera>().aspect;
        float sizeY = distanceY / 2;

        float maxSize;
        if (sizeX > sizeY) maxSize = sizeY;
        else maxSize = sizeX;

        return maxSize;
    }
}