using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class PythonAccess : MonoBehaviour
{
    public TextMeshProUGUI uiText;

    PythonRunner runner = new PythonRunner();
    string dirPython =  "/DetectAnimal/detect_animal.py";
    string dirImage = "/images.jpg";

    bool isClicked = false;
    private void Start()
    {
        dirPython = Application.dataPath + dirPython;
        dirImage = Application.dataPath + dirImage; 
    }

    public async void ProcessClassification()
    {
        Debug.Log(Application.dataPath);

        if(isClicked)
        {
            uiText.text += "It's already clicked";
            return;
        }

        isClicked = true;

        Debug.Log("ProcessClassification");
        uiText.text = "ProcessClassification\n";

        Task<List<string>> task = runner.RunPythonScriptAsync(dirPython, dirImage);
        await task;

        uiText.text = "";
        List<string> result = task.Result;
        if(result == null)
        {
            uiText.text = "Have NO results......";
            Debug.LogWarning("Have NO results......");
        }
        else
        {
            for (int i = 0; i < result.Count; i++)
                uiText.text += result[i] + "\n";
        }
        isClicked = false;
    }
}
