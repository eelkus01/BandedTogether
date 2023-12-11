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

    private void FixedUpdate()
    {
        // Check if the Space key is pressed and if enough time has passed since the last spawn.
        if (Input.GetKeyDown(KeyCode.Space)) {
            // check which instrument is selected, than calculate if that instrument is ready to fire again
            var selectedInstrument = gameHandler.GetComponent<GameHandler>().activeInstrumentID - 1;

            Quaternion orientation;
            
            if(Time.time - lastSpawnTimes[selectedInstrument] >= cooldownDurations[selectedInstrument]) {
                if(selectedInstrument == 2) {
                    int direction = GetOrientation();
                    int[] targetDirections = new int[] { direction - 15, direction, direction + 15 };
                    foreach (int targetDirection in targetDirections)
                    {
                        Instantiate(attackPrefabs[selectedInstrument], transform.position, Quaternion.Euler(0f, 0f, targetDirection));
                    }  
                }
                else {
                    Instantiate(attackPrefabs[selectedInstrument], transform.position, Quaternion.identity);
                }
                lastSpawnTimes[selectedInstrument] = Time.time;
            }
        }
    }

    // for use in spawning ice spikes in the correct direction
    public static int GetOrientation()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Calculate the angle in radians
        float angle = Mathf.Atan2(y, x);

        // Convert the angle to degrees
        float degrees = angle * Mathf.Rad2Deg;

        // Normalize the angle to be within 0 to 360 degrees
        if (degrees < 0) degrees += 360;

        // Round the angle to the nearest 45 degrees
        int orientation = Mathf.RoundToInt(degrees / 45) * 45;

        // Wrap around if the orientation is 360 degrees to make it 0 degrees
        if (orientation == 360) orientation = 0;

        return orientation;
    }
}
