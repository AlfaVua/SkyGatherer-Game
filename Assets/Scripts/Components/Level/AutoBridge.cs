using System.Collections.Generic;
using UnityEngine;
using Utils.TransformExtenstion;

namespace Components.Level
{
    public class AutoBridge : MonoBehaviour
    {
        [Header("Important")]
        [SerializeField] private BridgeComponent startJoint;
        [SerializeField] private BridgeComponent endJoint;
        [SerializeField] private Transform jointsContainer;
        [SerializeField] private BridgeComponent jointPrefab;
        [Space]
        [SerializeField][Min(0)] private int jointsCount;

        [SerializeField] private bool generate;

        private void Generate()
        {
            jointsContainer.ClearChildren();
            var joints = new List<BridgeComponent> { startJoint };
            if (jointsCount != 0) GenerateJoints(joints);
            joints.Add(endJoint);
            ConnectJoints(joints);
        }

        private void GenerateJoints(ICollection<BridgeComponent> joints)
        {
            var startJointPoint = startJoint.EndPoint();
            var endJointPoint = endJoint.StartPoint();
            var distanceBetweenPoints = Vector3.Distance(startJointPoint, endJointPoint);
            var distanceBetweenJoints = jointsCount == 0 ? 0 : distanceBetweenPoints / jointsCount;
            var bridgeDirection = (endJointPoint - startJointPoint).normalized;
            
            while (joints.Count <= jointsCount)
                GenerateJoint(distanceBetweenJoints, bridgeDirection, joints);
        }

        private void GenerateJoint(float distanceBetween, Vector3 direction, ICollection<BridgeComponent> jointsList)
        {
            var angle = Vector3.Angle(Vector3.right, direction);
            var joint = Instantiate(jointPrefab, jointsContainer);
            joint.transform.position = startJoint.EndPoint() + (jointsList.Count - .5f) * distanceBetween * direction;
            joint.transform.rotation = Quaternion.AngleAxis(angle, Mathf.Sign(direction.y) * Vector3.forward);
            jointsList.Add(joint);
        }

        private void ConnectJoints(in List<BridgeComponent> joints)
        {
            if (joints.Count < 2) return;
            joints[0].rightJoint.connectedBody = joints[1].rigidBody;
            for (var i = 1; i < joints.Count - 1; i++)
            {
                joints[i].leftJoint.connectedBody = joints[i - 1].rigidBody;
                joints[i].rightJoint.connectedBody = joints[i + 1].rigidBody;
            }
            joints[^1].leftJoint.connectedBody = joints[^2].rigidBody;
        }

        private void OnDrawGizmos()
        {
            DrawGizmos();
            if (!generate) return;
            generate = false;
            Generate();
        }

        private void DrawGizmos()
        {
            var startJointPoint = (Vector3)startJoint.rightJoint.anchor + startJoint.transform.position;
            var endJointPoint = (Vector3)endJoint.leftJoint.anchor + endJoint.transform.position;
            Gizmos.DrawLine(startJointPoint, endJointPoint);
            var distanceBetweenPoints = Vector3.Distance(startJointPoint, endJointPoint);
            var distanceBetweenJoints = jointsCount == 0 ? 0 : distanceBetweenPoints / jointsCount;
            var bridgeDirection = (endJointPoint - startJointPoint).normalized;
            for (var i = 0; i < jointsCount; i++)
            {
                Gizmos.DrawSphere(startJoint.EndPoint() + (i + .5f) * distanceBetweenJoints * bridgeDirection, .1f);
            }
        }
    }
}