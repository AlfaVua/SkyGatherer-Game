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

        private List<BridgeComponent> _joints;

        private void Generate()
        {
            jointsContainer.ClearChildren();
            _joints = new List<BridgeComponent> {startJoint};

            if (jointsCount != 0) GenerateJoints();
            ConnectJoints();
        }

        private void GenerateJoints()
        {
            var startJointPoint = startJoint.EndPoint();
            var endJointPoint = endJoint.StartPoint();
            var distanceBetweenPoints = Vector3.Distance(startJointPoint, endJointPoint);
            var distanceBetweenJoints = jointsCount == 0 ? 0 : distanceBetweenPoints / jointsCount;
            var bridgeDirection = (endJointPoint - startJointPoint).normalized;
            
            while (_joints.Count <= jointsCount)
                GenerateJoint(distanceBetweenJoints, bridgeDirection);
            _joints.Add(endJoint);
        }

        private void GenerateJoint(float distanceBetween, Vector3 direction)
        {
            var joint = Instantiate(jointPrefab, jointsContainer);
            joint.transform.position = startJoint.EndPoint() + (_joints.Count - .5f) * distanceBetween * direction;
            _joints.Add(joint);
        }

        private void ConnectJoints()
        {
            if (_joints.Count < 2) return;
            _joints[0].rightJoint.connectedBody = _joints[1].rigidBody;
            for (var i = 1; i < _joints.Count - 1; i++)
            {
                _joints[i].leftJoint.connectedBody = _joints[i - 1].rigidBody;
                _joints[i].rightJoint.connectedBody = _joints[i + 1].rigidBody;
            }
            _joints[^1].leftJoint.connectedBody = _joints[^2].rigidBody;
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