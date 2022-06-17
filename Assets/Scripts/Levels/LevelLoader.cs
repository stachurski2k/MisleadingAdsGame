using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader
{
    public const int mainSceneIndex=0;
    public static void LoadMainScene(){
        SceneManager.LoadScene(mainSceneIndex);
    }
    public static void LoadScene(int index){
        SceneManager.LoadScene(index);
    }
    public static void LoadActiveScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
