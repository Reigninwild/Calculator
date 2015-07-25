using UnityEngine;
using System.Collections;

//Скрипт выполняет инициализацию настроек и других параметров 
//которые в данный момент не активны, но должны быть проинициализированы 
public class Initialization : MonoBehaviour {

    public Settings settings;

	void Start () {
        settings.Load();   
	}
}
