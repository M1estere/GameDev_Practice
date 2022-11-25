using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class MGMT : MonoBehaviour
{
    [SerializeField] private string filename;

    private List<GameObject> children= new List<GameObject>();
    private List<string> childrenNames = new List<string>();
    private List<string> firstChildrenNames = new List<string>();

    private string path = "F:\\_1_UnityProjects\\_3_Laby\\Hierarchy_Reproduce\\Assets";

    private void Start()
    {
        if (File.Exists(path + filename + ".txt")) File.Delete(path + filename + ".txt");
        children = AllChilds(gameObject);

        GetChildrenNames();
        SaveInFile();
        ReadFile();
    }

    private void GetChildrenNames()
    {
        foreach (Transform child in transform)
            if (child.gameObject.GetComponent<ObjectStatus>().CanBeSaved == true) firstChildrenNames.Add(child.gameObject.name);
    }

    private void SaveInFile()
    {
        StreamWriter writer = new StreamWriter(path + filename+".txt",true);
        writer.WriteLine(gameObject.name);

        foreach (string name in firstChildrenNames)
            CheckChildren(writer,name,1);

        writer.Close(); 
    }
    private void CheckChildren(StreamWriter writer, string name, int koef)
    {
        int index = 0;
        string spaces = "";
        for (int i = 0; i < koef; i++)
            spaces += "   ";

        writer.WriteLine(spaces + name);
        foreach (GameObject obj in children)
            if (obj.name == name) index = children.IndexOf(obj);
            
        int a = koef;
        foreach (Transform child2 in children[index].transform)
        {
            koef = a;
            if (child2.GetComponent<ObjectStatus>().CanBeSaved == true) CheckChildren(writer, child2.gameObject.name, ++koef);
        }
    }

    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
            foreach (Transform child in root.transform)
                Searcher(result, child.gameObject);

        return result;
    }

    private void Searcher(List<GameObject> result, GameObject root)
    {
        if (root.GetComponent<ObjectStatus>().CanBeSaved == true)
        {
            result.Add(root);
            childrenNames.Add(root.name);
            if (root.transform.childCount > 0)
                foreach (Transform VARIABLE in root.transform)
                    Searcher(result, VARIABLE.gameObject);
        }
    }

    private void ReadFile()
    {
        FileInfo source1 = new FileInfo(path + filename + ".txt");
        StreamReader reader1 = source1.OpenText();

        string text1 = reader1.ReadLine();

        int counter1 = 0;
        int countOfWhitespaces = 0; ;
        while (text1 != null)
        {
            counter1 = 0;
            while (Char.IsWhiteSpace(text1[0]))
            {
                counter1++;
                text1=text1.Remove(0, 1);
            }
            if (countOfWhitespaces < counter1) countOfWhitespaces = counter1;

            text1 = reader1.ReadLine();
        }

        reader1.Close();

        List<int> distance = new List<int>();
        for (int i = 0; i <= countOfWhitespaces / 3; i++) distance.Add(0);

        FileInfo source = new FileInfo(path + filename + ".txt");
        StreamReader reader = source.OpenText();

        string text=reader.ReadLine();

        List<GameObject> namesFromFile = new List<GameObject>();
        int generation = 0;
        while (text != null)
        {
            if (generation == 0)
            {
                GameObject tmp = new GameObject(text);
                namesFromFile.Add(tmp);
                distance[generation] = namesFromFile.Count - 1;
                generation += 1;
            }
            else
            {
                if (!Char.IsWhiteSpace(text[3]))
                {
                    generation = 1;
                    text=text.Remove(0, 3);
                    GameObject tmp = new GameObject(text);
                    tmp.transform.parent = namesFromFile[distance[generation-1]].transform;
                    namesFromFile.Add(tmp);
                    distance[1] = namesFromFile.Count - 1;
                }
                else
                {
                    int c = 0;
                    while (Char.IsWhiteSpace(text[0]))
                    {
                        c++;
                        text=text.Remove(0, 1);
                    }
                    generation = c / 3;
                    GameObject tmp = new GameObject(text);
                    tmp.transform.parent = namesFromFile[distance[generation-1]].transform;
                    namesFromFile.Add(tmp);
                    distance[generation] = namesFromFile.Count - 1;
                }
            }

            text = reader.ReadLine();
        }
        
        reader.Close();
    }
}
