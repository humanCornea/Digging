using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerScript : MonoBehaviour
{
    public SaveDataManager SaveData;

    public void Load_GameMenuScene(){   SceneManager.   LoadScene( "GameMenuScene"  ); }
    public void Load_GameScene()    {   SceneManager.   LoadScene( "GameScene"      ); }
    public void Load_StatusScene()  {   SceneManager.   LoadScene( "StatusScene"    ); }

    public void Save() { SaveData.Save(); }
}
