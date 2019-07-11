using UnityEngine;

public class CarController : MonoBehaviour
{

    public JointMotor2D motorFront;
    public JointMotor2D motorBack;
    public WheelJoint2D frontwheel;
    public WheelJoint2D backwheel;

    public float speedF;
    public float torqueF;
    private Vector3 initPos;

    public Vector3 InitPos
    {
        get
        {
            return initPos;
        }     
    }

    private void Start()
    {
        initPos = transform.position;
        InitMotors();
    }

    private void InitMotors()
    {
        motorFront.motorSpeed = speedF * -1;
        motorFront.maxMotorTorque = torqueF;
        frontwheel.motor = motorFront;

        motorBack.motorSpeed = speedF * -1;
        motorBack.maxMotorTorque = torqueF;
        backwheel.motor = motorBack;
    }

    public void Restart()
    {
        gameObject.SetActive(true);
        transform.position = initPos;
        InitMotors();
    }    
}
