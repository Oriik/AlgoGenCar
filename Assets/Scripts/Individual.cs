﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CarController))]
public class Individual : MonoBehaviour, IComparable
{
    [SerializeField] private float timeBeforeStop = 5f;
    [SerializeField] private GameObject body;
    [SerializeField] private WheelJoint2D frontwheel;
    [SerializeField] private WheelJoint2D backwheel;

    private float fitness;
    private int bestFitness;
    public int BestFitness
    {
        get
        {
            return bestFitness;
        }
    }
    private float bestFitnessTime;
    public float BestFitnessTime
    {
        get
        {
            return bestFitnessTime;
        }
    }
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
        if (Mathf.RoundToInt(fitness) > bestFitness)
        {
            bestFitness = Mathf.RoundToInt(fitness);
            bestFitnessTime = Time.time;
        }
        fitness = transform.position.x;

        if (Mathf.RoundToInt(fitness) <= bestFitness)
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
        bestFitness = 0;
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

        body.transform.localScale = new Vector3(dna.scaleBodyX, dna.scaleBodyY);

        frontwheel.anchor = new Vector2(dna.xFront, frontwheel.connectedBody.transform.localPosition.y);
        frontwheel.connectedBody.transform.localScale = new Vector3(dna.scaleFront, dna.scaleFront);

        backwheel.connectedBody.transform.localScale = new Vector3(dna.scaleBack, dna.scaleBack);
        backwheel.anchor = new Vector2(dna.xBack, backwheel.connectedBody.transform.localPosition.y);
    }

    public void Mutation()
    {

    }

    public void ReachEndRace(Vector2 endPoint)
    {
        if (isMoving)
        {
            isMoving = false;
            bestFitness = Mathf.RoundToInt(endPoint.x);
            gameObject.SetActive(false);
        }
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        Individual indiv = obj as Individual;
        if(indiv != null)
        {         
            if(bestFitness == indiv.bestFitness)
            {
                return indiv.BestFitnessTime.CompareTo(bestFitnessTime);
            }            
            return bestFitness.CompareTo(indiv.BestFitness);
        }
        else
        {
            throw new ArgumentException("Object is not an Individual");
        }
    }
}
