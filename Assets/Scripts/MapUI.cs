using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    public MapUI instance;

    public DistrictDataPanel dataPanel;
    public MapManager mapManager;

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

    //-------- Open and Close Data Panel --------
    public void OpenDataPanel(int districtNum)
    {
        //Debug.Log("MapUI is being called by district: " + districtNum);
        dataPanel.gameObject.SetActive(true);

        mapManager.ToggleDistrictSelectability(); //Turn off ability to select districts

        dataPanel.activeDistrict = districtNum;
        //dataPanel.header.text = districtNum.ToString();
    }

    public void CloseDataPanel()
    {
        dataPanel.gameObject.SetActive(false);
        mapManager.ToggleDistrictSelectability(); //Turn on ability to select districts
        mapManager.DeselectAllDistricts(); //This removes the selection when tab closed
    }

}
