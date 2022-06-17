using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void LoadScene(int index){
        LevelLoader.LoadScene(index);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
