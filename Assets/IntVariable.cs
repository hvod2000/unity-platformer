using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "number", menuName = "IntVariable", order = 51)]
public class IntVariable : ScriptableObject
{
    [SerializeField] private bool isSavedBetweenSessions = false;
    public int value = 0;

    public void Awake()
    {
        if (isSavedBetweenSessions)
        {
            LoadValue();
        }
    }

    public void OnDisable()
    {
        if (isSavedBetweenSessions)
        {
            SaveValue();
        }
    }

    private string TouchStorageFile()
    {
        var path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);

        path += name;
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        return path;
    }

    private void LoadValue()
    {
        var path = TouchStorageFile();

        if (Int32.TryParse(File.ReadAllText(path), out int loaded))
        {
            value = loaded;
        }
    }

    private void SaveValue()
    {
        var path = TouchStorageFile();
        File.WriteAllText(path, "{value.ToString()}\n");
    }
}
