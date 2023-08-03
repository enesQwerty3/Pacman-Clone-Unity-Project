using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{   
    public List<PlayerData> playerDataList = new List<PlayerData>();
    string txtPath;  //path of text file
    int playerCount = 0;      //calculate existing players counts or players that shown in select player field
    public Text[] playerButtonText;  //displayed player select buttons 
    public PlayerData CurrentPlayer = new PlayerData();   //create object for current player data
    public InputField playerInput;

    void Awake()
    {
        txtPath = Application.dataPath + "/Players.txt";
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateFile();         //create Players.txt
        UpdatePlayerData();   //Update player data 
        LoadFromFile();        //load payer names and scores from Players.txt to playerDataList  
    }

    // Update is called once per frame
    /*
    void Update()
    {
    }
    */
    public void CreateFile()
    {
        if(!File.Exists(txtPath))
            File.Create(txtPath);

        else
            Debug.Log("File already exists!");
    }

    public void UpdateFile()
    {
        string newLine;        //string variable which will be inserted to text file
        if(CurrentPlayer.playerName != null /*&& playerDataList.Find(x => x.playerName == CurrentPlayer.playerName) == null*/) 
        {       
            if(playerDataList.Find(x => x.playerName == CurrentPlayer.playerName) == null) //check if CurrentPlayer object is in the list 
            {
                playerDataList.Add(CurrentPlayer);
                PlayerPrefs.SetString("playerName", playerDataList.Find(x => x.playerName == CurrentPlayer.playerName).playerName);
                PlayerPrefs.SetInt("highscore", playerDataList.Find(x => x.playerName == CurrentPlayer.playerName).score);
                Debug.Log("Displayed before second loadFromFile()");
            }
            else // if player that tried to create is in the list then select player don't create new player
            {
                string lastPlayerName = playerDataList.Find(x => x.playerName == CurrentPlayer.playerName).playerName;
                int lastPlayerScore = playerDataList.Find(x => x.playerName == CurrentPlayer.playerName).score;
                Debug.Log("currentPlayerName:" + " " + lastPlayerName + "  " + "currentPlayerScore: " + lastPlayerScore);
                PlayerPrefs.SetString("playerName", lastPlayerName);
                PlayerPrefs.SetInt("highscore", lastPlayerScore);
            }                                                                             //

        }

        playerDataList.Sort((x, y) => y.score.CompareTo(x.score));     //sort playerDataList (score descending)
        File.WriteAllText(txtPath, "");   //clear text file
        foreach(PlayerData player in playerDataList)       //insert new player data and scores to text file
        {
            newLine = (player.playerName + " " + player.score.ToString() +"\n");
            File.AppendAllText(txtPath, newLine);
        }                                                     //delete sixth player's data?
        playerDataList.Clear();  //delete all list items after loading them from txt 
        //when called again this function each time list has to be cleared to avoid multiplying data  
    }

    public void LoadFromFile()         //read player data from text file (player name, score)
    {
        if(File.Exists(txtPath) && File.ReadAllLines(txtPath) != null)
        {
            int i = 0;
            int score = 0;
            string playerName;
            string[] strings = File.ReadAllLines(txtPath); 
            
            foreach(string line in strings)  //read text file and add player names and scores to the playerDataList
            {
                string[] words = line.Split(" ", 2);       //split words in line string and add to the words array
                playerName = words[i];       //player name = words[0]
                bool _score = int.TryParse(words[i + 1], out score);  //player score = words[i + 1]
                
                if(_score)    //if score exists
                    playerDataList.Add(new PlayerData(playerName, score));
            }

            playerDataList.Sort((x, y) => y.score.CompareTo(x.score)); //sort list descending before displaying  //change position of x and y on the right hend side of =>

            i = 0;
            foreach(PlayerData player in playerDataList)     //write player data from playerDataList to select player screen buttons
            {
                if(i < 5)         //just display firs five items in playerDataList
                    playerButtonText[i].text = player.playerName + " " + player.score;
                     
                i++;
                Debug.Log("Player count: " + i);           
            } 
        }

        else
        {
            Debug.Log("File doesn't exists!");
            //playerCount = 0;       //if there is no line this mean there isn't any player created before so set player count to 0
        }    
    }

    public void CreatePlayer()
    {
        CurrentPlayer.playerName = playerInput.text;     //check if player already exist in the text file (check via list)
        CurrentPlayer.score = 0;
        UpdateFile();  //update text file data
        LoadFromFile(); //load data from file to the buttons and player list
        CurrentPlayer.playerName = null;
        CurrentPlayer.score = 0;
    }

    public void _SelectPlayer(Text buttonText)  //send selected players highscore to other scene by PlayerPrefs!!
    {
        string[] _playerData = buttonText.text.Split(" ", 2);  //get player name and score from button
        string playerName = _playerData[0];
        int score = int.Parse(_playerData[1]);

        PlayerPrefs.SetString("playerName", playerName);      //send selected players name with PlayerPrefs to other scene 
        PlayerPrefs.SetInt("highscore", score);     //send selected players score with PlayerPrefs to other scene 
        Debug.Log("Player Score: " + score);
    }
    
    public void UpdatePlayerData()      
    {                   //when player turn back to the menu update text file and clear player last score from PlayerPrefs
        string _playerName = PlayerPrefs.GetString("playerName");
        int highscore = PlayerPrefs.GetInt("highscore");
        LoadFromFile();
        Debug.Log("UpdatePlayerData playerName - score: " + _playerName + " - " + highscore);
        
        if(playerDataList.Find(player => player.playerName == _playerName).score != highscore)  //if high score of selected player is changed then change it list data
        {         
            Debug.Log("Change player highscore.");                                                                              //and text file
            playerDataList.Find(player => player.playerName == _playerName).score = highscore;
            UpdateFile();
        }
        playerDataList.Clear();
    }  
}   

public class PlayerData
{
    public string playerName;
    public int score;
    public PlayerData(string _playerName = null, int _score = 0)
    {
        this.playerName = _playerName;
        this.score = _score;
    }
}
