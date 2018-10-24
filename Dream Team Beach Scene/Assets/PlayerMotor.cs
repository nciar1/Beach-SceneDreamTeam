using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotX = 0f;
    private Vector3 thrustForce = Vector3.zero;

    [SerializeField]
    private float camRotationLimit = 85f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    //gets movement vector
    public void Move(Vector3 velocityIN)
    {
        velocity = velocityIN;
    }

    //gets rotation vector
    public void Rotate(Vector3 rotationIN)
    {
        rotation = rotationIN;
    }

    //get vector for camera
    public void rotateCamera(float cameraRotationINX)
    {
        cameraRotationX = cameraRotationINX;
    }

    public void ApplyThrust(Vector3 thurstForceIN)
    {
        thrustForce = thurstForceIN;
    }

    void performMove()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (thrustForce != Vector3.zero)
        {
            rb.AddForce(thrustForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void performRotation()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            //set rotation and clamp 
            currentCameraRotX -= cameraRotationX;
            currentCameraRotX = Mathf.Clamp(currentCameraRotX, -camRotationLimit, camRotationLimit);

            //apply rotation to transform of camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotX, 0, 0);
        }
    }

    //Run every physics iteration 
    void FixedUpdate()
    {
        performMove();
        performRotation();
    }
}
