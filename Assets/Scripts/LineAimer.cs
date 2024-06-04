using UnityEngine;
using UnityEngine.Events;

public class LineAimer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    private bool isAiming;
    private Rigidbody rb;

    [SerializeField]
    private float stopVelocity = 0.05f;
    public float MaxLineLength = 10f;

    private bool isEnabled = true;

    public UnityEvent OnAimingStart = new UnityEvent();
    public UnityEvent<float, Vector3> OnAiming = new UnityEvent<float, Vector3>();
    public UnityEvent OnCancelAiming = new UnityEvent();
    public UnityEvent<float, Vector3> OnAimingEnd = new UnityEvent<float, Vector3>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        if (!isAiming && isEnabled)
        {
            isAiming = true;
            OnAimingStart.Invoke();
        }
    }

    private void PorcessAim()
    {
        Vector3? worldPoint = CastMouseClickRay();
        if (worldPoint == null)
            return;

        var wpValue = worldPoint.Value;
        Vector3 varticalWorldPoint = new Vector3(wpValue.x, wpValue.y, transform.position.z);
        Vector3 direction = (varticalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(varticalWorldPoint, transform.position);

        if (worldPoint.HasValue)
        {
            DrawLine((Vector3)worldPoint);
            OnAiming.Invoke(strength, direction);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ClearAim();
            OnAimingEnd.Invoke(strength, direction);
        }
    }

    private void ClearAim()
    {
        isAiming = false;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude < stopVelocity)
        {
            Stop();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            OnCancelAiming.Invoke();
            ClearAim();
        }
        if (isAiming)
        {
            PorcessAim();
        }
    }

    private void Stop()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3 endPosition = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
        Vector3 startPosition = transform.position;
        float CurrentLineLength = Vector3.Magnitude(endPosition - startPosition);
        Vector3 MaxLineLengthVector = (endPosition - startPosition).normalized * MaxLineLength;
        if (CurrentLineLength > MaxLineLength)
        {
            endPosition = startPosition + MaxLineLengthVector;
        }
        Vector3[] positions = new Vector3[] { startPosition, endPosition };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
        );
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
        );
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if (
            Physics.Raycast(
                worldMousePosNear,
                worldMousePosFar - worldMousePosNear,
                out hit,
                float.PositiveInfinity
            )
        )
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }
}
