using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public InputField nameField;
    private string playerName;
    private int terrainDigged;
    private int totalDamageDealt;

    private List<Transform> hseTransformList;
  
    private void Start()
    {
        entryContainer = transform.Find("EntryContainer");
        entryTemplate = entryContainer.Find("EntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        /*
        //addHSE(1000, 100, 20f, "CES");
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScore hs = JsonUtility.FromJson<HighScore>(jsonString);
        for (int i = 0; i < hs.hseList.Count; i++)
        {
            for(int j = i+1; j< hs.hseList.Count; j++)
            {
                if(hs.hseList[j].terrains > hs.hseList[i].terrains && hs.hseList[j].damageDealt > hs.hseList[i].damageDealt)
                {
                    HighScoreEntry temp = hs.hseList[i];
                    hs.hseList[i] = hs.hseList[j];
                    hs.hseList[j] = temp;
                }
                else if (hs.hseList[j].terrains >= hs.hseList[i].terrains && hs.hseList[j].damageDealt < hs.hseList[i].damageDealt)
                {
                    if(hs.hseList[j].timePassed < hs.hseList[i].timePassed)
                    {
                        HighScoreEntry temp = hs.hseList[i];
                        hs.hseList[i] = hs.hseList[j];
                        hs.hseList[j] = temp;
                    }
                }
                else if (hs.hseList[j].terrains <= hs.hseList[i].terrains && hs.hseList[j].damageDealt >= hs.hseList[i].damageDealt)
                {
                    if (hs.hseList[j].timePassed < hs.hseList[i].timePassed)
                    {
                        HighScoreEntry temp = hs.hseList[i];
                        hs.hseList[i] = hs.hseList[j];
                        hs.hseList[j] = temp;
                    }
                }
            }
        }
        hseTransformList = new List<Transform>();
        //foreach(HighScoreEntry hse in hs.hseList)
       // {
        //    CreateHighScoreEntryTransform(hse, entryContainer, hseTransformList);
        //}
        for(int i = 0; i < 5; i++)
        {
            CreateHighScoreEntryTransform(hs.hseList[i], entryContainer, hseTransformList);
        }
        /*
        HighScore highscores = new HighScore { highscoreEntrylist = hseList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        */
        
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry hse, Transform container, List<Transform> transformList)
    {
        float templateheight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateheight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("RankEntry").GetComponent<Text>().text = rankString;
        string nameEntry = hse.name;
        entryTransform.Find("NameEntry").GetComponent<Text>().text = nameEntry;

        int terrainsDigged = hse.terrains;
        entryTransform.Find("TerrainDiggedEntry").GetComponent<Text>().text = terrainsDigged.ToString();

        int dd = hse.damageDealt;
        entryTransform.Find("DamageDealtEntry").GetComponent<Text>().text = dd.ToString();
        float time = hse.timePassed;
        entryTransform.Find("TimeEntry").GetComponent<Text>().text = time.ToString();

        transformList.Add(entryTransform);
    }
    public void addHSE(int terr, int damage, float timeP, string name)
    {
        HighScoreEntry hsEntry = new HighScoreEntry { terrains = terr, damageDealt = damage, timePassed = timeP, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScore hs = JsonUtility.FromJson<HighScore>(jsonString);

        hs.hseList.Add(hsEntry);
        string json = JsonUtility.ToJson(hs);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        jsonString = PlayerPrefs.GetString("highscoreTable");
        hs = JsonUtility.FromJson<HighScore>(jsonString);
        for (int i = 0; i < hs.hseList.Count; i++)
        {
            for (int j = i + 1; j < hs.hseList.Count; j++)
            {
                if (hs.hseList[j].terrains > hs.hseList[i].terrains && hs.hseList[j].damageDealt > hs.hseList[i].damageDealt)
                {
                    HighScoreEntry temp = hs.hseList[i];
                    hs.hseList[i] = hs.hseList[j];
                    hs.hseList[j] = temp;
                }
                else if (hs.hseList[j].terrains >= hs.hseList[i].terrains && hs.hseList[j].damageDealt < hs.hseList[i].damageDealt)
                {
                    if (hs.hseList[j].timePassed < hs.hseList[i].timePassed)
                    {
                        HighScoreEntry temp = hs.hseList[i];
                        hs.hseList[i] = hs.hseList[j];
                        hs.hseList[j] = temp;
                    }
                }
                else if (hs.hseList[j].terrains <= hs.hseList[i].terrains && hs.hseList[j].damageDealt >= hs.hseList[i].damageDealt)
                {
                    if (hs.hseList[j].timePassed < hs.hseList[i].timePassed)
                    {
                        HighScoreEntry temp = hs.hseList[i];
                        hs.hseList[i] = hs.hseList[j];
                        hs.hseList[j] = temp;
                    }
                }
            }
        }
        hseTransformList = new List<Transform>();
        //foreach(HighScoreEntry hse in hs.hseList)
        // {
        //    CreateHighScoreEntryTransform(hse, entryContainer, hseTransformList);
        //}
        for (int i = 0; i < 5; i++)
        {
            CreateHighScoreEntryTransform(hs.hseList[i], entryContainer, hseTransformList);
        }
    }
    public void sendHSData(int terrain, int damagedealt)
    {
        terrainDigged = terrain;
        totalDamageDealt = damagedealt;
    }
    

    private class HighScore
    {
        public List<HighScoreEntry> hseList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int terrains;
        public int damageDealt;
        public float timePassed;
        public string name;
    }

    private void playAgain()
    {

    }
}
