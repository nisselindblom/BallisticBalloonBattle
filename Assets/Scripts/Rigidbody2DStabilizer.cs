using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class Rigidbody2DStabilizer : MonoBehaviour 
{
    [System.Serializable]
    struct Rotation
    {
        public bool enabled;
        public float torque;
        public float damping;
    }
    [System.Serializable]
    struct Position
    {
        public bool enabled;
        public Vector2 force;
        public Vector2 damping;
    }

    [SerializeField] Rotation m_rotation = new Rotation() { enabled = true, torque = 1f, damping = 0.2f };
    [SerializeField] Position m_position = new Position() { enabled = true, force = new Vector2(500f, 500f), damping = new Vector2(100f, 100f) };
    float m_targetRotation;
    Vector2 m_targetPosition;
    Rigidbody2D m_rb2D;

    void Start()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_targetRotation = m_rb2D.rotation;
        m_targetPosition = m_rb2D.position;
    }

    void FixedUpdate()
    {
        // rotation
        if (m_rotation.enabled)
        {
            MoveRotation(m_rb2D, m_targetRotation, m_rotation.torque, m_rotation.damping);
        }

        // position
        if (m_position.enabled)
        {
            MovePosition(m_rb2D, m_targetPosition, m_position.force, m_position.damping);
        }
    }

    public void SetTargetRotation(float degrees) { m_targetRotation = degrees; }
    public void SetTargetPosition(Vector2 point) { m_targetPosition = point; }

    public static void MoveRotation(Rigidbody2D rb2D, float targetAngle, float force, float damping)
    {
        // the difference in the current rotation and the target rotation
        float deltaAngle = Mathf.DeltaAngle(rb2D.rotation, targetAngle);
        // torque strength
        float torque = deltaAngle * force - rb2D.angularVelocity * damping;
        // adding new torque
        rb2D.AddTorque(torque / 100f, ForceMode2D.Force);
    }
    public static void MovePosition(Rigidbody2D rb2D, Vector2 targetPosition, Vector2 force, Vector2 damping)
    {
        // distance between the current position and target position
        Vector2 deltaDistance = targetPosition - rb2D.position;
        // force amount
        Vector2 fixedForce = Vector2.Scale(deltaDistance, force) - Vector2.Scale(rb2D.velocity, damping);
        // adding new force
        rb2D.AddForce(fixedForce, ForceMode2D.Force);
    }
}