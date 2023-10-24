using UnityEngine;

namespace HO
{
    public class CameraHandler : MonoBehaviour
    {

        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;
        private Vector3 cameraFollowVelocity = Vector3.zero;
        public static CameraHandler singleton;
        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;
        private float targetPosition;
        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35f;
        public float maximumPivot = 35f;
        public float cameraSphereRaduis = 0.2f;
        public float cameraCollisionOffSet = 0.2f;
        public float minimumCollisionOffSet = 0.2f;

        private void Awake()
        {
            CameraHandler.singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        public void FollowTarget(float delta)
        {
            myTransform.position = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
            HandleCameraCollisions(delta);
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            lookAngle += mouseXInput * lookSpeed / delta;
            pivotAngle -= mouseYInput * pivotSpeed / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vecotr3.zero;


            myTransform.rotation = Quaternion.Euler(Vector3.zero, lookAngle);
            cameraPivotTransform.localRotation = Quaternion.Euler(Vector3.zero, pivotAngle);
        }

        private void HandleCameraCollisions(float delta)
        {
            targetPosition = defaultPosition;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();
            RaycastHit hitInfo;
            if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRaduis, direction, out hitInfo, Mathf.Abs(targetPosition), (int)ignoreLayers))
                targetPosition = (float)-((double)Vector3.Distance(cameraPivotTransform.position, hitInfo.point) - (double)cameraCollisionOffSet);
            if ((double)Mathf.Abs(targetPosition) < (double)minimumCollisionOffSet)
                targetPosition = -minimumCollisionOffSet;
            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }
    }
}