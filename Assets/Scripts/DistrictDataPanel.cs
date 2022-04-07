using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistrictDataPanel : MonoBehaviour
{
    public TextMeshProUGUI header;

    public int activeDistrict;

    void Update()
    {
        
        header.text = "District No. "+ (activeDistrict+1).ToString();
    }




}
