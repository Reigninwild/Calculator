using UnityEngine;
using System.Collections;

//Скрипт выполняет инициализацию настроек и других параметров 
//которые в данный момент не активны, но должны быть проинициализированы 
public class Initialization : MonoBehaviour {

    public Settings settings;

#if UNITY_IOS
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
#endif

    void Start () {
        settings.Load();   
	}
}
