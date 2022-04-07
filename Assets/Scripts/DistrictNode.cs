using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictNode : MonoBehaviour
{

    public float ActiveValue;
    public float ActiveValueMaxScale;

    public int ActiveTimeIndex;
    public int ActiveVariableIndex;

    public int districtNum;

    public Material tempColor;
    public Material highlightColor;
    Renderer rend;

    public float[,] districtData;
    public int numTimeSteps;
    public int numVariables;

    public bool isHighlighted;
    public bool canBeSelected;

    public MapManager mapManager;
    public MapUI mapUI;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        tempColor = rend.material;
        rend.material.EnableKeyword("_EMISSION");

        ActiveValueMaxScale = 10;
        ActiveTimeIndex = 0;
        ActiveVariableIndex = 0;

        isHighlighted = false;
        canBeSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveValue = districtData[ActiveTimeIndex, ActiveVariableIndex];

        //--------- !!!! Colour Update Script !!!! -----------
        rend.material.color = Color32.Lerp(Color.blue, Color.red, ActiveValue / (ActiveValueMaxScale));

        if(isHighlighted)
        {
            rend.material = highlightColor;
        }
        else
        {
            rend.material = tempColor;
            //rend.material.SetColor("_EmissionColor", tempColor.color);
        }
    }

    public void InitializeDataArray(int TimeSteps, int Variables)
    {
        numTimeSteps = TimeSteps;
        numVariables = Variables;

        districtData = new float[numTimeSteps, numVariables];
    }


    // ---------- Functions called by interactions with Mouse ---------
    private void OnMouseEnter()
    {
        //Debug.Log("Mouse over object");

        if(canBeSelected)
        {
            isHighlighted = true;
        }
        
    }

    private void OnMouseExit()
    {
        if(canBeSelected)
        {
            isHighlighted = false;
        }
    }

    public void OnMouseDown()
    {
        if (canBeSelected)
        {
            //Debug.Log("Clicking on district: " + districtNum);
            mapUI.OpenDataPanel(districtNum);
        }
    }




    //------- Functions called by MapManager --------

    public void ToggleSelectability()
    {
        canBeSelected = !canBeSelected;
    }

    public void UpdateActiveTimestep(int newTimestep)
    {
        ActiveTimeIndex = newTimestep;
    }

    public void UpdateActiveVariable(int newVariable)
    {
        ActiveVariableIndex = newVariable;
    }

}
