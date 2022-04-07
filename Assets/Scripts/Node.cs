using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float ActiveValue;
    public float ActiveValueMaxScale;

    public float[] Concentrations;
    public float[] ConcMaxScale;
    public float[] DiffusionCoefficients;
    public int numOfTotalComponents; //Total number of variables iterated over, in this case compositions
    public int ActiveValueIndex;



    public Material tempColor;
    public Material boundaryColor;
    Renderer rend;

    public bool isBoundary;
    public bool isTransparent;






    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        isBoundary = false;
        isTransparent = false;


        numOfTotalComponents = 2;
        Concentrations = new float[numOfTotalComponents];
        ConcMaxScale = new float[numOfTotalComponents];

        DiffusionCoefficients = new float[numOfTotalComponents];
        ActiveValueIndex = 0;
        InitializeConcentrations();


    }

// Update is called once per frame
void Update()
    {
        //----- Determine which value is Active Variable -----
        ActiveValue = Concentrations[ActiveValueIndex];
        ActiveValueMaxScale = ConcMaxScale[ActiveValueIndex];

        //--------- !!!! Colour Update Script !!!! -----------
        rend.material.color = Color32.Lerp(Color.blue, Color.red, ActiveValue / (ActiveValueMaxScale));

        if (isBoundary)
        {
            rend.material = boundaryColor;
        }

        if (isTransparent)
        {
            rend.material = tempColor;
        }

    }

    public void SetBoundary()
    {
        isBoundary = true;
    }


    //-------- Calculations ------------

    public void InitializeConcentrations()
    {
        for (int i = 0; i < Concentrations.Length; i++)
        {
            ConcMaxScale[i] = 1;
            Concentrations[i] = Random.Range(0f, ConcMaxScale[i]);
            //DiffusionCoefficients[i] = Random.Range(0f, 0.5f);
            //DiffusionCoefficients[i] = 1;
        }
        //--------- Override Concentrations for Gray-Scott-----
        Concentrations[0] = Random.Range(0.5f, 1f);
        Concentrations[1] = Random.Range(0f, 0.25f);

        DiffusionCoefficients[0] = 0.01f;
        DiffusionCoefficients[0] = 0.005f;
    }


    //------- Updating Node Values after calculating finite differences -------
    public void UpdateConcentrations()
    {
        for (int i = 0; i < Concentrations.Length; i++)
        {

            
        }
    }





}


