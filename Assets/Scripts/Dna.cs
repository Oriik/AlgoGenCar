

using UnityEngine;

public class Dna
{
    public float xFront, xBack, scaleFront, scaleBack, scaleBodyX, scaleBodyY;

    public void Hybridation(Dna father, Dna mother)
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
}
