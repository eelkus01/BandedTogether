using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    public GameObject[] attackPrefabs;
    public float[] cooldownDurations = { 0.5f, 2f, 2f };

    private float[] lastSpawnTimes;
    
    public GameObject gameHandler;

    private Rigidbody2D rb;

    private void Start()
    {
        lastSpawnTimes = new float[cooldownDurations.Length];

        for (int i = 0; i < cooldownDurations.Length; i++)
        {
            lastSpawnTimes[i] = -cooldownDurations[i]; // Negate each element and store it in the new array
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
        }
    }

    private void Update()
    {
        // Check if the Space key is pressed and if enough time has passed since the last spawn.
        if (Input.GetKeyDown(KeyCode.Space)) {
            // check which instrument is selected, than calculate if that instrument is ready to fire again
            var selectedInstrument = gameHandler.GetComponent<GameHandler>().activeInstrumentID - 1;
            
            if(Time.time - lastSpawnTimes[selectedInstrument] >= cooldownDurations[selectedInstrument]) {
                GameObject instance = Instantiate(attackPrefabs[selectedInstrument], transform.position, Quaternion.identity);
                lastSpawnTimes[selectedInstrument] = Time.time;
            }
        }
    }

    // for use in spawning ice spikes in the correct direction
    public int GetPredominantDirection()
    {
        if (rb == null)
        {
            return -1; // Indicates an error or no movement
        }

        Vector2 velocity = rb.velocity;

        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            // Horizontal movement is predominant
            return velocity.x > 0 ? 0 : 180; // 0 for right, 180 for left
        }
        else
        {
            // Vertical movement is predominant
            return velocity.y > 0 ? 90 : 270; // 90 for up, 270 for down
        }
    }
}
