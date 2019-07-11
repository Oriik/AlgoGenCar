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
    private float bestFitness;

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

    private Dna dna;
    public Dna Dna
    {
        get
        {
            return dna;
        }
    }

    public void Start()
    {
        isMoving = true;
        ResetFitness();
        ResetTimer();
        carController = GetComponent<CarController>();
        xStart = carController.InitPos.x;
    }

    private void OnEnable()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving)
        {
            return;
        }
        if (fitness > bestFitness)
        {
            bestFitness = fitness;
        }
        fitness = transform.position.x;

        if (fitness <= bestFitness)
        {
            if (!timerStarted)
            {
                timerStarted = true;
            }

            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                isMoving = false;
                gameObject.SetActive(false);
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
        bestFitness = fitness;
    }

    public void FirstGeneration()
    {
        dna = new Dna
        {
            xFront = Random.Range(-0.5f, 0.5f),
            xBack = Random.Range(-0.5f, 0.5f),
            scaleFront = Random.Range(0.05f, 0.8f),
            scaleBack = Random.Range(0.05f, 0.8f),

            scaleBodyX = Random.Range(5.0f, 10.0f),
            scaleBodyY = Random.Range(0.5f, 15.0f)
        };

        body.transform.localScale = new Vector3(dna.scaleBodyX, dna.scaleBodyY);

        frontwheel.anchor = new Vector2(dna.xFront, frontwheel.connectedBody.transform.localPosition.y);
        frontwheel.connectedBody.transform.localScale = new Vector3(dna.scaleFront, dna.scaleFront);

        backwheel.connectedBody.transform.localScale = new Vector3(dna.scaleBack, dna.scaleBack);
        backwheel.anchor = new Vector2(dna.xBack, backwheel.connectedBody.transform.localPosition.y);
    }

    public void NotFirstGeneration(Dna newDna)
    {
        dna = newDna;
        carController = GetComponent<CarController>();

        body.transform.localScale = new Vector3(dna.scaleBodyX, dna.scaleBodyY);

        frontwheel.anchor = new Vector2(dna.xFront, frontwheel.connectedBody.transform.localPosition.y);
        frontwheel.connectedBody.transform.localScale = new Vector3(dna.scaleFront, dna.scaleFront);

        backwheel.connectedBody.transform.localScale = new Vector3(dna.scaleBack, dna.scaleBack);
        backwheel.anchor = new Vector2(dna.xBack, backwheel.connectedBody.transform.localPosition.y);
    }

    public void Mutation()
    {

    }

    public void ReachEndRace()
    {
        if (isMoving)
        {
            isMoving = false;
            gameObject.SetActive(false);
        }
    }
}
