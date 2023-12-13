using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialExit : MonoBehaviour
{
    // NOTE: This script depends on the GameHandler having a public int "thePieces"
    // that is updated with each pickup collected.
    public GameHandler gameHandler;
    public string NextLevel = "Milo_Workspace";
    public GameObject door;

    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        door.SetActive(true);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(NextLevel);
        }
    }
}