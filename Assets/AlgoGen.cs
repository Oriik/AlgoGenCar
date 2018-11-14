using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlgoGen : MonoBehaviour {

    public int numGen = 0;
    
    public int populationSize = 10;
    public int numGenMax = 50;

    public GameObject prefabCar;

    private bool carsMoving = false;

    private List<carController> cars = new List<carController>();
    private List<carController> carsChildTemp = new List<carController>();
    // Use this for initialization
    void Start () {

        for (int i = 0; i < populationSize; i++)
        {
            GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity);
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
        carsChildTemp.Clear();
        while(carsChildTemp.Count < populationSize/2)
        {

            carController car = new carController();
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
                    if (temp.fitness <= minFitness)
                    {
                        worst = temp;
                        minFitness = worst.fitness;
                    }

                }

                bestCars.Remove(worst);
            }
        }
        Debug.Log(cars.Count);
        Debug.Log(bestCars.Count);
        cars = bestCars;

        Debug.Log(GameObject.FindGameObjectsWithTag("Car").Length);


        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Car"))
        {
            if (!cars.Contains(item.GetComponent<carController>()))
            {
                Debug.Log("DESTROY");
                DestroyImmediate(item);                
            }
            
        }

        Debug.Log(GameObject.FindGameObjectsWithTag("Car").Length);

    }
	
	// Update is called once per frame
	void Update () {

        carsMoving = false;
        foreach(carController car in cars)
        {
            carsMoving = carsMoving || car.isMoving;
        }
        
       
      
		 while(!carsMoving && numGen < numGenMax)
        {
            carsMoving = true;
            Selection();
            Hybridation();
            Mutation();
            numGen++;
            

            for (int i =0; i< carsChildTemp.Count; i++)
            {
               GameObject go = Instantiate(prefabCar, transform.position, Quaternion.identity);
                SetCar(go.GetComponent<carController>(), carsChildTemp[i]);
               
            }
           

            GameObject[] individual = GameObject.FindGameObjectsWithTag("Car");
            for (int i = 0; i < individual.Length; i++)
            {

                individual[i].GetComponent<carController>().ResetPosition();
            }
            

        }
	}

    private void SetCar(carController carToSet, carController setter)
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
