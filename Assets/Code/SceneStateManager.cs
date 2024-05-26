using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{

    public int SceneId;
    public List<RoomSpawner> Rooms;

    void Awake()
    {
        var state = GameSettings.Instance.CurrentSceneState;
        SceneId = state.Id;

        var rng = RandomUtil.CreateDeterministicRNG(state.Seed);
        //this gives each room a seed based on the state's seed.
        foreach (var room in Rooms)
        {
            room.seed = rng.Next(int.MaxValue);
        }
    }

}
