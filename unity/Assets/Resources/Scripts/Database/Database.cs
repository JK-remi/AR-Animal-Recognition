using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class AnimalInfo
{
    string species;
    string name;
    string birthday;
    string attribute;
    string info;
    public string Species
    {  get { return species; } }
    public string Name
    { get { return name; } }
    public string Birthday
    { get { return birthday; } }
    public string Attribute
    { get { return attribute; } }
    public string Info
    { get { return info; } }

    public AnimalInfo(string species, string name, string birthday, string attribute, string info)
    {
        this.species = species;
        this.name = name;
        this.birthday = birthday;
        this.attribute = attribute;
        this.info = info;
    }
}

public class Database : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    const string fileName = "DB_animal";
    Dictionary<string, AnimalInfo> dicDB = new Dictionary<string, AnimalInfo>();

    private void Awake()
    {
        Load(fileName);
    }

    public AnimalInfo GetInfo(string name)
    {
        if(dicDB.ContainsKey(name) == false)
        {
            Debug.LogError("Have no Data >>   " + name);
            return null;
        }

        return dicDB[name];
    }

    private void Load(string file) 
    {
        var list = new List<Dictionary<string, object>>();
        string[] lines;

        if (File.Exists(SystemPath.GetPath() + file + ".csv"))
        {
            string source;
            StreamReader sr = new StreamReader(SystemPath.GetPath() + file + ".csv");
            source = sr.ReadToEnd();
            sr.Close();

            lines = Regex.Split(source, LINE_SPLIT_RE);

            Debug.Log("Load " + file + ".csv");
        }
        else
        {
            Debug.LogError("Load FAIL: " + file + ".csv");
            return;
        }

        if (lines.Length <= 1)
        {
            Debug.LogError("Load FAIL: have no datas");
            return;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            string[] values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0) continue;

            dicDB.Add(values[0], new AnimalInfo(values[1], values[2], values[3], values[4], values[5]));
        }
    }
}
