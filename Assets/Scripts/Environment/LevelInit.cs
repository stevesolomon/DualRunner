using UnityEngine;
using System.Collections;

public class LevelInit : MonoBehaviour {

    public LevelGenerator[] levelGenerators;
    
	// Use this for initialization
	void Start () {
        foreach (var levelGenerator in levelGenerators)
        {
            levelGenerator.GenerateStartingRooms();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
