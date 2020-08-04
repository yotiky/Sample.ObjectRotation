using UnityEngine;

public class CameraRotator3rdPersonPov : MonoBehaviour
{
    public GameObject targetObject;
    public Vector2 rotationSpeed = new Vector2(0.1f, 0.2f);
    public RotatableAxis rotatableAxis = RotatableAxis.XY;
    public bool reverse;
    public float zoomSpeed = 1;
    public bool following;

    private Camera mainCamera;
    private Vector2 lastMousePosition;
    private Vector3 lastTargetPosition;

    // Start is called before the first frame update
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
                var x = (lastMousePosition.x - Input.mousePosition.x);
                var y = (Input.mousePosition.y - lastMousePosition.y);

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

                var newAngleX = x * rotationSpeed.x;
                var newAngleY = y * rotationSpeed.y;

                mainCamera.transform.RotateAround(targetObject.transform.position, Vector3.up, newAngleX);
                mainCamera.transform.RotateAround(targetObject.transform.position, transform.right, newAngleY);
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                var x = (Input.mousePosition.x - lastMousePosition.x);
                var y = (lastMousePosition.y - Input.mousePosition.y);

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

                var newAngleX = x * rotationSpeed.x;
                var newAngleY = y * rotationSpeed.y;

                mainCamera.transform.RotateAround(targetObject.transform.position, Vector3.up, newAngleX);
                mainCamera.transform.RotateAround(targetObject.transform.position, transform.right, newAngleY);
                lastMousePosition = Input.mousePosition;
            }
        }

        {
            // Zoom
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position += mainCamera.transform.forward * scroll * zoomSpeed;
        }
        if (following)
        {
            // 移動するオブジェクトに追従する場合
            mainCamera.transform.position += targetObject.transform.position - lastTargetPosition;
            lastTargetPosition = targetObject.transform.position;
        }
    }
}
