using UnityEngine;

namespace Components.Level
{
    public class BridgeComponent : MonoBehaviour
    {
        public Rigidbody2D rigidBody;
        public HingeJoint2D leftJoint;
        public HingeJoint2D rightJoint;

        public Vector3 StartPoint()
        {
            return (Vector3)leftJoint.anchor + transform.position;
        }

        public Vector3 EndPoint()
        {
            return (Vector3)rightJoint.anchor + transform.position;
        }
    }
}