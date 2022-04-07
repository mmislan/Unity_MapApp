using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class MapManager : MonoBehaviour
{
    public DistrictNode[] districts;

    public MapManager instance;
    public MapUI mapUI;

    public int numTimesteps;
    private int numDistricts;

    public int currentTimestep;
    public int currentActiveVariable;

    public float ScaleMax_Var1;
    public float ScaleMax_Var2;

    public float remainingCooldown;
    public float timeBetweenSteps;
    public bool runningTimeSeries;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTimestep = 0;
        numDistricts = 19;
        numTimesteps = 30;
        runningTimeSeries = false;
        remainingCooldown = 0f;
        timeBetweenSteps = 0.1f;

        //---- Can call Node initialization functions from here ----
        InitializeDistrictNumbers();
        InjectMapManager();
        InjectMapUI();
    }

    void Update()
    {

        //----- It seems necessary to setup the Timeseries code in Update, to have a delay without a coroutine
        if (remainingCooldown < 0)
            remainingCooldown = 0;
        else
        {
            remainingCooldown -= Time.deltaTime;
        }

        if(runningTimeSeries)
        {
            if(remainingCooldown <= 0)
            {
                currentTimestep++;
                UpdateDistrictTimesteps();
                remainingCooldown = timeBetweenSteps;
                if(currentTimestep == (numTimesteps-1))
                {
                    runningTimeSeries = false;
                }
            }
        }


    }


    public void ImportFromCSV()
    {

        //----- Initialize District Nodes -------
        for (int j = 0; j < districts.Length; j++)
        {
            districts[j].InitializeDataArray(30, 2);
        }

        StreamReader strReader = new StreamReader("E:\\! Unity Games\\DistrictMap\\LoadCSV\\DistrictData1.csv");
        StreamReader strReader2 = new StreamReader("E:\\! Unity Games\\DistrictMap\\LoadCSV\\DistrictData2.csv");


        //------ Currently performing this manually for each variable ---------
        bool endOfFile = false;
        int tempNumTimesteps = 0;

        while(!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if(data_String == null)
            {
                endOfFile = true;
                break;
            }

            var data_values = data_String.Split(',');

            for(int i = 1; i < data_values.Length; i++)
            {
                //Debug.Log("Value:" + i.ToString() + " " + data_values[i].ToString());
                districts[i - 1].districtData[tempNumTimesteps, 0] = float.Parse(data_values[i]); //---- This converts String to Float!! ---
            }

            tempNumTimesteps++;
        }


        //I'm doing this as brute force as possible!!
        endOfFile = false;
        tempNumTimesteps = 0;

        while (!endOfFile)
        {
            string data_String = strReader2.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }

            var data_values = data_String.Split(',');

            for (int i = 1; i < data_values.Length; i++)
            {
                //Debug.Log("Value:" + i.ToString() + " " + data_values[i].ToString());
                districts[i - 1].districtData[tempNumTimesteps, 1] = float.Parse(data_values[i]); //---- This converts String to Float!! ---
            }

            tempNumTimesteps++;
        }




    }

    //------- Run Timeseries -------

    public void RunTimeSeries()
    {
        runningTimeSeries = true;

        if(currentTimestep == (numTimesteps-1))
        {
            currentTimestep = 0;
        }
    }
    /*
    public int tempTimestepCount;
    public float timeseriesAnimTime = 0.1f;

    public void RunTimeSeries()
    {
        tempTimestepCount = 0;

        while(tempTimestepCount < numTimesteps)
        {
            if(runningTimeCount >= timeseriesAnimTime)
            {
                currentTimestep = tempTimestepCount;
                UpdateDistrictTimesteps();

                tempTimestepCount++;
                runningTimeCount = 0f; //!!Make sure to re-initialize the running time count!!
            }
        }
    }
    */

    /*
    public void RunTimeSeries()
    {

    }

    IEnumerator Wait(float secondsToWait)
    {
        UnityEngine.Debug.Log("Coroutine is triggering");
        yield return new WaitForSeconds(secondsToWait);
    }
    */
  

    //--------- Change Active Variable -> Currently a toggle!!! ----
    public void ToggleActiveVariable()
    {
        if(currentActiveVariable == 0)
        {
            currentActiveVariable = 1;
        }
        else
        {
            currentActiveVariable = 0;
        }

        UpdateDistrictActiveVariable();
    }




    //---- Iterating over each DistrictNode
    public void InitializeDistrictNumbers()
    {
        for (int i = 0; i < districts.Length; i++)
        {
            districts[i].districtNum = i;
        }
    }

    public void UpdateDistrictTimesteps()
    {
        for (int i = 0; i < districts.Length; i++)
        {
            districts[i].UpdateActiveTimestep(currentTimestep);
        }
    }

    public void UpdateDistrictActiveVariable()
    {
        for (int i = 0; i < districts.Length; i++)
        {
            districts[i].UpdateActiveVariable(currentActiveVariable);
        }
    }

    //------- This turns the ability to select districts On/Off for when one is selected
    public void ToggleDistrictSelectability()
    {
        for (int i = 0; i < districts.Length; i++)
        {
            districts[i].ToggleSelectability();
        }
    }

    public void DeselectAllDistricts()
    {
        for (int i = 0; i < districts.Length; i++)
        {
            districts[i].isHighlighted = false;
        }
    }

    //---- For some reason, these don't work if I try to bundle them with toggling district selectability -----
    public void InjectMapManager()
    {

        for (int i = 0; i < districts.Length; i++)
        {
            districts[i].mapManager = this;
        }
        
    }

    public void InjectMapUI()
    {

        for (int i = 0; i < districts.Length; i++)
        {
            districts[i].mapUI = mapUI;
        }

    }


}
