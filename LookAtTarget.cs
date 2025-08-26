using UnityEngine;
using UnityEngine.Events;

public class LookAtTarget : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Transform target;
    [Range(0.1f, 100f)][SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private bool continuousRotation = true;

    [Header("Axis Constraints")]
    [SerializeField] private bool lockX = false;
    [SerializeField] private bool lockY = false;
    [SerializeField] private bool lockZ = false;

    [Header("Rotation Limits (Degrees)")]
    [SerializeField] private bool useXLimits = false;
    [SerializeField] private float minX = -45f, maxX = 45f;
    [SerializeField] private bool useYLimits = false;
    [SerializeField] private float minY = -90f, maxY = 90f;
    [SerializeField] private bool useZLimits = false;
    [SerializeField] private float minZ = -30f, maxZ = 30f;

    [Header("Events")]
    public UnityEvent onRotationStart;
    public UnityEvent onRotationStop;

    public bool IsRotating { get; private set; }
    private Transform thisTransform;
    private const float stopThreshold = 0.1f; // degrees

    private void Awake()
    {
        thisTransform = transform;
    }

    private void OnEnable()
    {
        if (target != null && !IsRotating)
            StartRotation();
    }

    private void OnDisable()
    {
        StopRotation();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null) StartRotation();
        else StopRotation();
    }

    public void StartRotation()
    {
        if (!IsRotating)
        {
            IsRotating = true;
            onRotationStart?.Invoke();
            LookAtManager.Register(this);
        }
    }

    public void StopRotation()
    {
        if (IsRotating)
        {
            IsRotating = false;
            onRotationStop?.Invoke();
            LookAtManager.Unregister(this);
        }
    }

    public void TickRotation(float deltaTime)
    {
        if (!IsRotating || target == null || rotationSpeed <= 0f)
            return;

        Vector3 direction = target.position - thisTransform.position;
        if (direction.sqrMagnitude < 0.0001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 targetEuler = NormalizeAngle(targetRotation.eulerAngles);
        Vector3 currentEuler = NormalizeAngle(thisTransform.eulerAngles);

        // Axis locks
        if (lockX) targetEuler.x = currentEuler.x;
        if (lockY) targetEuler.y = currentEuler.y;
        if (lockZ) targetEuler.z = currentEuler.z;

        // Axis limits
        if (useXLimits) targetEuler.x = Mathf.Clamp(targetEuler.x, minX, maxX);
        if (useYLimits) targetEuler.y = Mathf.Clamp(targetEuler.y, minY, maxY);
        if (useZLimits) targetEuler.z = Mathf.Clamp(targetEuler.z, minZ, maxZ);

        // Smooth rotation
        Quaternion finalRotation = Quaternion.Euler(targetEuler);
        float step = rotationSpeed * deltaTime;
        thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, finalRotation, step);

        // Stop when close enough (only if NOT continuous rotation)
        if (!continuousRotation && Quaternion.Angle(thisTransform.rotation, finalRotation) < stopThreshold)
        {
            thisTransform.rotation = finalRotation;
            StopRotation();
        }
    }

    private float NormalizeAngle(float angle)
    {
        return Mathf.Repeat(angle + 180f, 360f) - 180f;
    }

    private Vector3 NormalizeAngle(Vector3 angle)
    {
        angle.x = NormalizeAngle(angle.x);
        angle.y = NormalizeAngle(angle.y);
        angle.z = NormalizeAngle(angle.z);
        return angle;
    }
}
