using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    //-- set start time
    public int minutes = 0;
    public int hour = 0;
    public int seconds = 0;
    public bool realTime = true;

    public GameObject pointerSeconds;
    public GameObject pointerMinutes;
    public GameObject pointerHours;

    //-- OPTIONAL UI (drag your "Time Up" text here)
    public GameObject timeUpText;

    //-- time speed factor
    public float clockSpeed = 100.0f;

    //-- internal vars
    float msecs = 0;
    bool hasTriggered = false;

    void Start()
    {
        if (realTime)
        {
            hour = System.DateTime.Now.Hour;
            minutes = System.DateTime.Now.Minute;
            seconds = System.DateTime.Now.Second;
        }

        // hide text at start
        if (timeUpText != null)
        {
            timeUpText.SetActive(false);
        }
    }

    void Update()
    {
        //-- calculate time
        msecs += Time.deltaTime * clockSpeed;

        while (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            seconds++;

            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;

                if (minutes >= 60)
                {
                    minutes = 0;
                    hour++;

                    if (hour >= 24)
                        hour = 0;
                }
            }
        }

        //-- check for 8 PM (20:00:00)
        if (!hasTriggered && hour >= 8)
        {
            hasTriggered = true;
            TimeUp();
        }

        //-- calculate pointer angles
        float rotationSeconds = (360.0f / 60.0f) * seconds;
        float rotationMinutes = (360.0f / 60.0f) * minutes;
        float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

        //-- draw pointers
        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);
    }

    void TimeUp()
    {
        Debug.Log("You ran out of time!");

        // show UI if assigned
        if (timeUpText != null)
        {
            timeUpText.SetActive(true);
        }

        // optional dramatic effect
        clockSpeed = 500f;

        // restart after 2 seconds
        Invoke("RestartScene", 2f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}