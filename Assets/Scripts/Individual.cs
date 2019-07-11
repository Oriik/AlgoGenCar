using UnityEngine;

[RequireComponent(typeof(CarController))]
public class Individual : MonoBehaviour
{
    [SerializeField] private float timeBeforeStop = 5f;
    [SerializeField] private GameObject body;
    [SerializeField] private WheelJoint2D frontwheel;
    [SerializeField] private WheelJoint2D backwheel;

    private float fitness;
    public float Fitness
    {
        get
        {
            return fitness;
        }
    }
    private float lastFitness;

    private CarController carController;
    private float xStart;

    private bool isMoving;
    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
    }

    private bool timerStarted;
    private float timer;

    [HideInInspector] public float xFront, xBack, scaleFront, scaleBack, scaleBodyX, scaleBodyY;

    public void Start()
    {
        ResetFitness();
        ResetTimer();
        isMoving = true;
        carController = GetComponent<CarController>();
        xStart = carController.InitPos.x;
    }

    private void Update()
    {
        if (!isMoving)
        {
            return;
        }

        lastFitness = fitness;
        fitness = transform.position.x - xStart;

        if (Mathf.Abs(lastFitness - fitness) < 0.01f)
        {
            if (!timerStarted)
            {
                timerStarted = true;
            }

            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                isMoving = false;
                carController.StopMotors();
            }
        }
        else
        {
            if (timerStarted)
            {
                ResetTimer();
            }
        }
    }

    private void ResetTimer()
    {
        timerStarted = false;
        timer = timeBeforeStop;
    }

    private void ResetFitness()
    {
        fitness = 0;
        lastFitness = fitness;
    }

    public void FirstGeneration()
    {
        xFront = Random.Range(-0.5f, 0.5f);
        xBack = Random.Range(-0.5f, 0.5f);
        scaleFront = Random.Range(0.05f, 0.8f);
        scaleBack = Random.Range(0.05f, 0.8f);

        scaleBodyX = Random.Range(5.0f, 10.0f);
        scaleBodyY = Random.Range(0.5f, 15.0f);

        body.transform.localScale = new Vector3(scaleBodyX, scaleBodyY);

        frontwheel.anchor = new Vector2(xFront, frontwheel.connectedBody.transform.localPosition.y);
        frontwheel.connectedBody.transform.localScale = new Vector3(scaleFront, scaleFront);

        backwheel.connectedBody.transform.localScale = new Vector3(scaleBack, scaleBack);
        backwheel.anchor = new Vector2(xBack, backwheel.connectedBody.transform.localPosition.y);
    }

    public void NotFirstGeneration()
    {
        carController = GetComponent<CarController>();
        carController.Restart();

        body.transform.localScale = new Vector3(scaleBodyX, scaleBodyY);

        frontwheel.anchor = new Vector2(xFront, frontwheel.connectedBody.transform.localPosition.y);
        frontwheel.connectedBody.transform.localScale = new Vector3(scaleFront, scaleFront);

        backwheel.connectedBody.transform.localScale = new Vector3(scaleBack, scaleBack);
        backwheel.anchor = new Vector2(xBack, backwheel.connectedBody.transform.localPosition.y);
    }

    public void Hybridation(Individual father, Individual mother)
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
}
