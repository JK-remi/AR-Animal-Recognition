using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Info : MonoBehaviour
{
    public TextMeshProUGUI textSpecies;
    public TextMeshProUGUI textInfo;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textBirth;
    public TextMeshProUGUI textAttribute;

    public string Init(string response)
    {
        string tag = "";
        if (textSpecies == null) return tag;
        if (textInfo == null) return tag;
        if (textName == null) return tag;
        if (textBirth == null) return tag;
        if (textAttribute == null) return tag;

        List<string> animals = new List<string>(response.Split(','));
        if (animals == null || animals.Count == 0)
        {
            // notice "Have NO results......";
            Debug.LogWarning("Have NO results......");
            return tag;
        }
        else
        {
            tag = animals[0];
            textSpecies.text = animals[1];
            textName.text = animals[2];
            textBirth.text = animals[3];
            textAttribute.text = animals[4];
            textInfo.text = animals[5];
        }

        return tag;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
