using System.Collections.Generic;
using UnityEngine;

public class AlgoGen : MonoBehaviour
{
    [SerializeField] private GameObject prefabCar;
    [SerializeField] private int populationSize = 10;
    [SerializeField] private int numGenMax = 50;

    private int numGen = 0;

    private bool carsMoving = false;

    private List<Individual> cars = new List<Individual>();
    private List<Individual> carsChildTemp = new List<Individual>();

    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity);
            Individual car = go.GetComponent<Individual>();
            car.FirstGeneration();
            cars.Add(car);
        }
    }

    private void Update()
    {
        carsMoving = false;
        foreach (Individual car in cars)
        {
            carsMoving = carsMoving || car.IsMoving;
        }

        if (!carsMoving)
        {
            NewGeneration();
        }
    }

    private void NewGeneration()
    {
        Selection();
        Hybridation();
        Mutation();
        numGen++;

        for (int i = 0; i < carsChildTemp.Count; i++)
        {
            GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity);
            SetCar(go.GetComponent<Individual>(), carsChildTemp[i]);
        }

        GameObject[] individual = GameObject.FindGameObjectsWithTag("Car");
        for (int i = 0; i < individual.Length; i++)
        {
            individual[i].GetComponent<CarController>().Restart();
        }
    }

    private void Selection()
    {
        List<Individual> bestCars = new List<Individual>();
        foreach (Individual c in cars)
        {
            bestCars.Add(c);
            if (bestCars.Count > populationSize / 2)
            {
                float minFitness = float.MaxValue;
                Individual worst = null;
                foreach (Individual temp in bestCars)
                {
                    if (temp.Fitness <= minFitness)
                    {
                        worst = temp;
                        minFitness = worst.Fitness;
                    }
                }
                bestCars.Remove(worst);
            }
        }
        cars = bestCars;

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Car"))
        {
            if (!cars.Contains(item.GetComponent<Individual>()))
            {
                DestroyImmediate(item);
            }

        }
    }

    private void Hybridation()
    {
        carsChildTemp.Clear();
        while (carsChildTemp.Count + cars.Count < populationSize)
        {
            Individual car = new Individual();
            int father = Random.Range(0, populationSize / 2);
            int mother;
            do
            {
                mother = Random.Range(0, populationSize / 2);
            } while (mother == father);
            car.Hybridation(cars[father], cars[mother]);

            carsChildTemp.Add(car);
        }
    }

    private void Mutation()
    {
        foreach (Individual c in cars)
        {
            if (Random.Range(0f, 100f) < 0.05f)
            {
                c.Mutation();
            }
        }
    }

    private void SetCar(Individual carToSet, Individual setter)
    {
        carToSet.xFront = setter.xFront;
        carToSet.xBack = setter.xBack;
        carToSet.scaleFront = setter.scaleFront;
        carToSet.scaleBack = setter.scaleBack;
        carToSet.scaleBodyX = setter.scaleBodyX;
        carToSet.scaleBodyY = setter.scaleBodyY;

        carToSet.NotFirstGeneration();

        cars.Add(carToSet);
    }
}
