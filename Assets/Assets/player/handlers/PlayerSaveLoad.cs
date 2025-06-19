using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveLoad
{
    public struct DataEntry
    {
        public int score, health;
    }
    public static void Save(DataEntry data)
    {
        Debug.Log("Saving PlayerPrefs");
        PlayerPrefs.SetInt("score", data.score);
        PlayerPrefs.SetInt("health", data.health);
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("hasPlayed", 1);
        Debug.Log(data.score);
        Debug.Log(data.health);
        PlayerPrefs.Save();
    }
    public static DataEntry Load()
    {
        Debug.Log("Loading PlayerPrefs");
        DataEntry data;
        data.score = PlayerPrefs.GetInt("score");
        int health = PlayerPrefs.GetInt("health");
        Debug.Log(data.score);
        Debug.Log(health); // loaded data pre-processing
        if(health <= 0)
        {
            health = 30;
        }
        data.health = health;
        return data;
    }
    public static int HasPlayerPlayed()
    {
        return PlayerPrefs.GetInt("hasPlayed");
    }
    public static int GetLastLevel()
    {
        return PlayerPrefs.GetInt("level");
    }
}
