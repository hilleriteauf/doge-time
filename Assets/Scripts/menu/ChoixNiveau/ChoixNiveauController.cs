using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChoixNiveauController : MonoBehaviour
{
    private List<string> listNameMusic;
    private FileInfo[] tabFile;

    private const int nbNivParLig = 6;


    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo infoFolder = new DirectoryInfo("Assets/Resources/Midis");
        tabFile = infoFolder.GetFiles();
        
        
        //foreach (FileInfo file in tabFile)
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
