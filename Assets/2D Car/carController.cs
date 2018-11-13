using UnityEngine;
using System.Collections;

public class carController : MonoBehaviour
{    
    public WheelJoint2D frontwheel;
    public WheelJoint2D backwheel;

    JointMotor2D motorFront;

    JointMotor2D motorBack;

    public float speedF;

    public float torqueF;
    public float torqueB;

    public float xFront, xBack, scaleFront, scaleBack, scaleBodyX, scaleBodyY;
    public float fitness =0;
    private float lastFitness;

    private float xStart;

    public bool isMoving = true;


    public void Start()
    {
        xStart = transform.position.x;
        lastFitness = fitness;
    }


    public void FirstGeneration()
    {
         xFront = Random.Range(-0.5f, 0.5f);
         xBack = Random.Range(-0.5f, 0.5f);
         scaleFront = Random.Range(0.5f, 1.2f);
         scaleBack = Random.Range(0.5f, 1.2f);

         scaleBodyX = Random.Range(1.0f, 10.0f);
         scaleBodyY = Random.Range(1.0f, 10.0f);

        transform.localScale = new Vector3(scaleBodyX, scaleBodyY);

        frontwheel.anchor = new Vector2(xFront, frontwheel.connectedBody.transform.localPosition.y);
        frontwheel.connectedBody.transform.localScale = new Vector3(scaleFront, scaleFront);

        backwheel.connectedBody.transform.localScale = new Vector3(scaleBack, scaleBack);
        backwheel.anchor = new Vector2(xBack, backwheel.connectedBody.transform.localPosition.y);
    }

    public void Hybridation(carController father, carController mother)
    {
        int rand = Random.Range(0, 1);
        if (rand == 0)
        {
            xFront = father.xFront;
        }
        else
        {
            xFront = mother.xFront;
        }

        rand = Random.Range(0, 1);
        if (rand == 0)
        {
            xBack = father.xBack;
        }
        else
        {
            xBack = mother.xBack;
        }

        rand = Random.Range(0, 1);
        if (rand == 0)
        {
            scaleFront = father.scaleFront;
        }
        else
        {
            scaleFront = mother.scaleFront;
        }

        rand = Random.Range(0, 1);
        if (rand == 0)
        {
            scaleBack = father.scaleBack;
        }
        else
        {
            scaleBack = mother.scaleBack;
        }

        rand = Random.Range(0, 1);
        if (rand == 0)
        {
            scaleBodyX = father.scaleBodyX;
        }
        else
        {
            scaleBodyX = mother.scaleBodyX;
        }

        rand = Random.Range(0, 1);
        if (rand == 0)
        {
            scaleBodyY = father.scaleBodyY;
        }
        else
        {
            scaleBodyY = mother.scaleBodyY;
        }
    }

    public void Mutation()
    {

    }
    // Update is called once per frame
    void Update()
    {
        motorFront.motorSpeed = speedF * -1;
        motorFront.maxMotorTorque = torqueF;
        frontwheel.motor = motorFront;


        motorBack.motorSpeed = speedF * -1;
        motorBack.maxMotorTorque = torqueF;
        backwheel.motor = motorBack;

        lastFitness = fitness;
        fitness = transform.position.x - xStart;

        if(lastFitness == fitness)
        {
            isMoving = false;
        }

    }
}
