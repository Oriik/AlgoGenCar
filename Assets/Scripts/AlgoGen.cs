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
    private List<Dna> childDna = new List<Dna>();

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

        if (!carsMoving && numGenMax > numGen)
        {
            NewGeneration();
        }
    }

    private void NewGeneration()
    {
        numGen++;
        Selection();
        Hybridation();
        Mutation();

        foreach (Individual car in cars)
        {
            car.GetComponent<CarController>().Restart();
        }

        for (int i = 0; i < childDna.Count; i++)
        {

            GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity);
            go.name = "Child " + i + "_gen " + numGen;
            go.GetComponent<Individual>().NotFirstGeneration(childDna[i]);
            cars.Add(go.GetComponent<Individual>());
        }

    }

    private void Selection()
    {
        cars.Sort();
        
        cars.RemoveRange(0, cars.Count / 2);

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
        childDna.Clear();
        while (childDna.Count + cars.Count < populationSize)
        {
            Dna dna = new Dna();
            int father = Random.Range(0, cars.Count);
            int mother;
            do
            {
                mother = Random.Range(0, cars.Count);
            } while (mother == father);
            dna.Hybridation(cars[father].Dna, cars[mother].Dna);

            childDna.Add(dna);
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
}
