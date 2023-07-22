using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameController : MonoBehaviour
{

    public string MiniGameName
    {
        get { return gameObject.scene.name; }
    }

    public Cowboy Owner;
    public GameObject MiniGameRoot;
    public UnityAction OnGameStart;

    public delegate void FinishedGameHandler(MiniGameController miniGameController);
    private event FinishedGameHandler OnGameFinish;

    /// <summary>
    /// Plays the current minigame.
    /// </summary>
    /// <param name="finishedGameHandler">The function to be called when the minigame has finished playing.</param>
    public void Play(FinishedGameHandler finishedGameHandler)
    {
        MiniGameRoot.SetActive(true);
        if (OnGameStart != null) OnGameStart.Invoke();
        OnGameFinish = finishedGameHandler;
    }

    public void Stop()
    {
        MiniGameRoot.SetActive(false);
        OnGameFinish.Invoke(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        MiniGameManager miniGameManager = GameObject.Find("Minigame Manager").GetComponent<MiniGameManager>();
        Debug.Log(miniGameManager.AddMiniGame(this));
        if (Owner = null) Owner = new Cowboy("placeholder");
        // TODO: Hide all gameobjects except self.
        MiniGameRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Stop();
    }
}
