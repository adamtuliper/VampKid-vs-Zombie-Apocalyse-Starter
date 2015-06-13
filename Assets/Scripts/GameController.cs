using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {

    private int _coinScore;
    private int _points;

    //The textboxes on the screen we'll update
    public Text PointsText;
    public Text CoinScoreText;
    public Text LevelMessageText;
    public string LevelNameToLoad;

    //The portal so we can enable it when the user picks up the key.
    public PortalController PortalControllerFromCrypt;
   
    //PlayerDied Goes Here

    void Start()
    {
        //Sanity checks

        if (PointsText == null) Debug.LogError("PointsText hasn't been set to a Text element in your scene yet. We can't update screen text without this.");
        if (CoinScoreText == null) Debug.LogError("CoinScoreText hasn't been set to a Text element in your scene yet. We can't update coin counter text without this.");

        if (string.IsNullOrEmpty(LevelNameToLoad))
        {
            Debug.LogError("No scene name has been set to load. Set this in the Editor on the PortalController component on the Portal");
        }


        if (PortalControllerFromCrypt == null)
        {
            Debug.LogError("There's no PortalController reference drag/dropped into the hierarchy window for the GameController. We can't enable the portal (crypt) without this reference being set.");
        }
    }
    //Enables the 'crypt' exit portal once we've picked up the key in a scene
    public void EnableExitPortal()
    {

        //Find portal and enable it.
        PortalControllerFromCrypt.ActivatePortal();

    }
    internal static GameController GetGameControllerInScene()
    {
        var gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc == null)
        {
            Debug.LogError("Could not find an object tagged GameController in your scene!");
            return null;
        } 

        return gc.GetComponent<GameController>();
    }

    internal void LoadNextLevel()
    {
       
        //TODO: display a 'success' sign, loading scene, etc.
        Application.LoadLevel(LevelNameToLoad);

    }

   //Increment scores here

   //ShowLevelMessage here
}
