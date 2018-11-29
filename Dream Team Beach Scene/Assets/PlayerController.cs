using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3;

    [SerializeField]
    private float thrustForce = 1000f;

    [Header("Spring Settings")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 10f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        setJointSettings(jointSpring);
    }

    void Update()
    {
        //calculate velocity as a 3D vector
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorz = transform.right * xMove; //(1,0,0) rotate about x
        Vector3 moveVert = transform.forward * zMove; //(0,0,1) rotate about z

        //final movement vector
        Vector3 velocity = (moveHorz + moveVert).normalized * speed;

        //apply movement
        motor.Move(velocity);

        //calculate rotation as 3D vector (turing around)
        float yRotate = Input.GetAxisRaw("Mouse X");

        //player only rotates about y-axis
        Vector3 rotation = new Vector3(0f, yRotate, 0f) * lookSensitivity;

        //apply roatation
        motor.Rotate(rotation);


        //calculate camera rotation 
        float xRotate = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRotate * lookSensitivity;

        //apply camera roatation
        motor.rotateCamera(cameraRotationX);

        //calculate thrust force
        Vector3 _thrustForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrustForce = Vector3.up * thrustForce;
            setJointSettings(0f);
        }
        else
        {
            setJointSettings(jointSpring);
        }

        //apply thrust force
        motor.ApplyThrust(_thrustForce);
    }

    private void setJointSettings(float jointSpringIN)
    {
        joint.yDrive = new JointDrive
        {
            mode = jointMode,
            positionSpring = jointSpringIN,
            maximumForce = jointMaxForce
        };
    }

}
