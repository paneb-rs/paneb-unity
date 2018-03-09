using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatasetClassification : MonoBehaviour {

    public TextAsset csv;

	// Use this for initialization
	void Start () {
        
        string[,] csvInputs = CSVReader.SplitCsvGrid(csv.text);

        CSVReader.DebugOutputGrid(csvInputs);	
	}
}
