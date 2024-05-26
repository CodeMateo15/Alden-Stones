using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameSettings", menuName = "My Game/GameSettings")]
public class GameSettings : ScriptableObject
{

    #region Singleton

    private static GameSettings _instance;

    public static GameSettings Instance { get { return _instance; } }

    public static bool Initialized { get { return _instance != null; } }

    public static void Init()
    {
        _instance = Resources.Load("GameSettings") as GameSettings;
        _instance.OnInitialized();
    }

    #endregion

    #region Fields

    public int GlobalSeed = -1;
    public IRandom GlobalRNG { get; private set; }
    public SceneState CurrentSceneState { get; private set; }
    public int SceneCount { get { return _previousScenes.Count; } }

    private List<SceneState> _previousScenes = new List<SceneState>();

    #endregion



    #region Methods

    private void OnInitialized()
    {
        _previousScenes.Clear();
        GlobalRNG = RandomUtil.CreateDeterministicRNG(GlobalSeed);
    }

    public void LoadNextScene()
    {
        //see if the 'next' scene is an existing scene
        if (_previousScenes.Count > 0 && (this.CurrentSceneState.Id + 1) < _previousScenes.Count)
        {
            this.LoadExistingScene(this.CurrentSceneState.Id + 1);
            return;
        }

        //if 'next' scene doesn't exist... create a new scene
        CurrentSceneState = new SceneState()
        {
            Id = _previousScenes.Count,
            Seed = GlobalRNG.Next(int.MaxValue)
        };
        _previousScenes.Add(CurrentSceneState);
    }

    public void LoadExistingScene(int id)
    {
        if (id < 0 || id >= _previousScenes.Count) throw new System.ArgumentException("Unknown Scene Id");

        this.CurrentSceneState = _previousScenes[id];
    }

    #endregion



    public struct SceneState
    {
        public int Id;
        public int Seed;
        //other stuff to save between scenes
    }
}