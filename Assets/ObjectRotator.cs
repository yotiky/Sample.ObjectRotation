using UnityEngine;

public enum RotatableAxis
{
    None,
    X,
    Y,
    XY,
}

public class ObjectRotator : MonoBehaviour
{
    public GameObject targetObject;
    public Vector2 rotationSpeed = new Vector2(0.1f, 0.2f);
    public RotatableAxis rotatableAxis = RotatableAxis.XY;
    public bool reverse;
    public float zoomSpeed = 1;

    private Camera mainCamera;
    private Vector2 lastMousePosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Rotation
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!reverse)
            {
                var x = (Input.mousePosition.y - lastMousePosition.y);
                var y = (lastMousePosition.x - Input.mousePosition.x);

                {
                    // 回転方向を制限する場合
                    if (rotatableAxis == RotatableAxis.None)
                        x = y = 0;
                    else if (rotatableAxis == RotatableAxis.X)
                        y = 0;
                    else if (rotatableAxis == RotatableAxis.Y)
                        x = 0;
                    else if (Mathf.Abs(x) < Mathf.Abs(y))
                        x = 0;
                    else
                        y = 0;
                }

                var newAngle = Vector3.zero;
                newAngle.x = x * rotationSpeed.x;
                newAngle.y = y * rotationSpeed.y;

                targetObject.transform.Rotate(newAngle);
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                var x = (lastMousePosition.y - Input.mousePosition.y);
                var y = (Input.mousePosition.x - lastMousePosition.x);

                {
                    // 回転方向を制限する場合
                    if (rotatableAxis == RotatableAxis.None)
                        x = y = 0;
                    else if (rotatableAxis == RotatableAxis.X)
                        y = 0;
                    else if (rotatableAxis == RotatableAxis.Y)
                        x = 0;
                    else if (Mathf.Abs(x) < Mathf.Abs(y))
                        x = 0;
                    else
                        y = 0;
                }

                var newAngle = Vector3.zero;
                newAngle.x = x * rotationSpeed.x;
                newAngle.y = y * rotationSpeed.y;

                targetObject.transform.Rotate(newAngle);
                lastMousePosition = Input.mousePosition;
            }
        }

        {
            // Zoom する場合
            var scroll = Input.mouseScrollDelta.y;
            targetObject.transform.position += -mainCamera.transform.forward * scroll * zoomSpeed;
        }
    }
}