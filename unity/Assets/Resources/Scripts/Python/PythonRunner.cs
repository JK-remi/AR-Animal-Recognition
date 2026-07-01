using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PythonRunner
{
    [System.Serializable]
    public class StringListWrapper
    {
        public List<string> Items;
    }

    public async Task<List<string>> RunPythonScriptAsync(string pythonScriptPath, string imgPath)
    {
        return await Task.Run(() =>
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = Application.dataPath + "/DetectAnimal/.venv/Scripts/python.exe";
            start.Arguments = string.Format("\"{0}\" {1}", pythonScriptPath, imgPath);
            start.UseShellExecute = false;
            start.CreateNoWindow = true;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd();
                    string result = reader.ReadToEnd();
                    StringListWrapper stringList = JsonUtility.FromJson<StringListWrapper>("{\"Items\":" + result + "}");
                    List<string> results = stringList.Items;
                    return results;
                }
            }
        });
    }
}
