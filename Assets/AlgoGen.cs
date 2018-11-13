using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlgoGen : MonoBehaviour {

    private int numGen = 0;
    
    public int populationSize = 10;
    public int numGenMax = 50;

    public GameObject prefabCar;

    private bool carsMoving = false;

    private List<carController> cars = new List<carController>();
    // Use this for initialization
    void Start () {

        for (int i = 0; i < populationSize; i++)
        {
            GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity, transform);
            carController car =  go.GetComponent<carController>();
            car.FirstGeneration();
            cars.Add(car);
        }
    }

    void Mutation()
    {
        foreach(carController c in cars)
        {
            if (Random.Range(0f, 100f) < 0.05f)
            {
                c.Mutation();
            }
        }
    }

    void Hybridation()
    {
        while(cars.Count < populationSize)
        {
            GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity, transform);
            carController car = go.GetComponent<carController>();
            int father = Random.Range(0, populationSize / 2);
            int mother;
            do
            {
                mother = Random.Range(0, populationSize / 2);
            } while (mother == father);
            car.Hybridation(cars[father], cars[mother]);
            
            cars.Add(car);
        }
    }

    void Selection()
    {
        List<carController> bestCars = new List<carController>();
        foreach(carController c in cars)
        {
            bestCars.Add(c);
            if(bestCars.Count > populationSize / 2)
            {
                float minFitness = float.MaxValue;
                carController worst = null;
                foreach (carController temp in bestCars)
                {
                    if (temp.fitness < minFitness)
                    {
                        worst = temp;
                        minFitness = worst.fitness;
                    }

                }
                bestCars.Remove(worst);
            }
        }

        cars = bestCars;
    }
	
	// Update is called once per frame
	void Update () {

        
        foreach(carController car in cars)
        {
            carsMoving = carsMoving || car.isMoving;
        }

		while(!carsMoving && numGen < numGenMax)
        {
            Selection();
            Hybridation();
            Mutation();
            numGen++;

            for(int i =0; i< cars.Count; i++)
            {
                GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity, transform);
               
            }
            

        }
	}
}
