using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public string[] Minigames;
    public bool isPlaying
    {
        get { return _isPlaying; }
    }

    private List<MiniGameController> _MiniGames = new List<MiniGameController>();
    private Queue<MiniGameController> _MiniGameQueue = new Queue<MiniGameController>();
    private bool _isPlaying = false;


    public void Play(MiniGameController miniGame)
    {
        if (isPlaying)
        {
            _MiniGameQueue.Enqueue(miniGame);
            return;
        }
        miniGame.Play(OnGameFinish);
        this._isPlaying = true;
    }

    /// <summary>
    /// Plays the minigame with the given name.
    /// </summary>
    /// <param name="cowboyName"></param>
    public void Play(string miniGameName)
    {
        MiniGameController miniGame = _MiniGames.Find(o => o.MiniGameName.Equals(miniGameName));
        if (miniGame == null) throw new System.NullReferenceException("Couldn't find minigame with given name.");
        Play(miniGame);
    }

    /// <summary>
    /// Starts playing the games in the queue.
    /// </summary>
    public bool Play()
    {
        if (isPlaying || _MiniGameQueue.Count <= 0) return false;
        Play(_MiniGameQueue.Dequeue());
        return true;
    }

    /// <summary>
    /// Plays all minigames owned by a given cowboy.
    /// </summary>
    /// <param name="cowboyName"></param>
    public void PlayAllGamesFromCowboy(Cowboy cowboy)
    {
        PlayAllGamesFromCowboy(cowboy.cowboyName);
    }

    /// <summary>
    /// Plays all minigames owned by a given cowboy.
    /// </summary>
    /// <param name="cowboyName"></param>
    public void PlayAllGamesFromCowboy(string cowboyName)
    {
        List<MiniGameController> miniGames = _MiniGames.FindAll(o => o.Owner.cowboyName.Equals(cowboyName));
        foreach (MiniGameController miniGame in miniGames) _MiniGameQueue.Enqueue(miniGame);
        Play();
    }

    public void OnGameFinish(MiniGameController miniGameController)
    {
        this._isPlaying = false;
            if (_MiniGameQueue.Count > 0)
            {
                MiniGameController miniGame = _MiniGameQueue.Dequeue();
                Play(miniGame);
                return;
            }

        // Do clean-up.
    }

    public bool AddMiniGame(MiniGameController miniGameController)
    {
        MiniGameController existing = _MiniGames.Find(o => o.MiniGameName.Equals(miniGameController.name));
        if (existing != null) return false;
        _MiniGames.Add(miniGameController);
        return true;
    }

    public bool RemoveMiniGame(MiniGameController miniGameController)
    {
        return _MiniGames.Remove(miniGameController);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (string minigame in Minigames) SceneManager.LoadSceneAsync(minigame, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Play("HorseRacing");
    }
}
