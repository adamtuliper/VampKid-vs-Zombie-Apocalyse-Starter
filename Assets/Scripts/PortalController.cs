using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour
{
    #region variables
    //Holds a reference to our GameController object
    private GameController _gameController;

    //Has the player activated the portal, can they leave this level?
    private bool _portalActivated;

    //This is the Lock game object (icon) that we'll show when we are locked, and hide when unlocked
    //Use [SerializeField] to show in the Editor but still remain a private variable for best OO practices
    [SerializeField]
    private GameObject _lock;

    //The closed door image, we'll hide this when the player picks up the key
    [SerializeField]
    private GameObject _closedDoor;

    //The open door image to show when the player picks up the key
    [SerializeField]
    private GameObject _openDoor;

    //Have we shown the user a helper message when they move over the portal?
    //Don't want to tell the user every time they pass the portal that they need to find the key
    private bool _displayedHelperMessage;
    
    #endregion

    // Use this for initialization
    void Start()
    {
        _gameController = GameController.GetGameControllerInScene();

        //Initialize our game object states (hide/show gameobjects)
        _lock.SetActive(true);
        _closedDoor.SetActive(true);
        _openDoor.SetActive(false);
    }

    public void ActivatePortal()
    {
        //enable this portal so we can leave this level and hide/show gameobjects
        _portalActivated = true;
        _lock.SetActive(false);
        _closedDoor.SetActive(false);
        _openDoor.SetActive(true);
    }

    /// <summary>
    /// This will only get called when IsTrigger =true on the collider 
    /// and the component is enabled. We disable it when the scene starts
    /// because we must find the key first.
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_portalActivated)
            {
                //Ask the game controller to load the next level up.
                _gameController.LoadNextLevel();
            }
            else if (!_displayedHelperMessage)
            {
                //portal isn't activated yet! need to find the key
                _displayedHelperMessage = true;

                //Show helper message
            }
        }
    }
}
