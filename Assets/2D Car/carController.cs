using UnityEngine;


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

    public float xStart;

    public bool isMoving = true;

    public bool timerStarted = false;
    public float timer;


    public void Start()
    {
        xStart = transform.position.x;
        lastFitness = fitness;
    }

    public void NotFirstGeneration()
    {
        ResetPosition();
        Body body = GetComponentInChildren<Body>();

        body.transform.localScale = new Vector3(scaleBodyX, scaleBodyY);

        frontwheel.anchor = new Vector2(xFront, frontwheel.connectedBody.transform.localPosition.y);
        frontwheel.connectedBody.transform.localScale = new Vector3(scaleFront, scaleFront);

        backwheel.connectedBody.transform.localScale = new Vector3(scaleBack, scaleBack);
        backwheel.anchor = new Vector2(xBack, backwheel.connectedBody.transform.localPosition.y);
    }

    public void FirstGeneration()
    {
         xFront = Random.Range(-0.5f, 0.5f);
         xBack = Random.Range(-0.5f, 0.5f);
         scaleFront = Random.Range(0.5f, 1.2f);
         scaleBack = Random.Range(0.5f, 1.2f);

         scaleBodyX = Random.Range(5.0f, 10.0f);
         scaleBodyY = Random.Range(0.5f, 15.0f);

        Body body = GetComponentInChildren<Body>();
        body.transform.localScale = new Vector3(scaleBodyX, scaleBodyY);

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
        fitness = GetComponentInChildren<Body>().transform.localPosition.x - xStart;

        if(lastFitness >= fitness)
        {
            if (!timerStarted)
            {
                timerStarted = true;
                timer = 5.0f;
            }
            else
            {
                timer -= Time.deltaTime;
                if(timer <= 0.0f)
                {
                    isMoving = false;
                }
            }

        }
        else
        {
            isMoving = true;
        }

    }

    internal void ResetPosition()
    {
        GetComponentInChildren<Body>().transform.localPosition = Vector3.zero;

    }
}
