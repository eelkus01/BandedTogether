using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    public GameObject[] attackPrefabs;
    public float[] cooldownDurations = { 0.5f, 2f, 4f };

    private float[] lastSpawnTimes;
    
    public GameObject gameHandler;

    private void Start()
    {
        lastSpawnTimes = new float[cooldownDurations.Length];

        for (int i = 0; i < cooldownDurations.Length; i++)
        {
            lastSpawnTimes[i] = -cooldownDurations[i]; // Negate each element and store it in the new array
        }
    }

    private void Update()
    {
        // Check if the Space key is pressed and if enough time has passed since the last spawn.
        if (Input.GetKeyDown(KeyCode.Space)) {
            // check which instrument is selected, than calculate if that instrument is ready to fire again
            var selectedInstrument = gameHandler.GetComponent<GameHandler>().activeInstrumentID - 1;

            Debug.Log("Selected instrument is "+selectedInstrument);

            
            if(Time.time - lastSpawnTimes[selectedInstrument] >= cooldownDurations[selectedInstrument]) {
                Instantiate(attackPrefabs[selectedInstrument], transform.position, Quaternion.identity);
                lastSpawnTimes[selectedInstrument] = Time.time;
            }
        }
    }
}
