using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //the current local user playerObject. Is in every scene (needed for testing) and needs to be Destroyed when loading
    public GameObject localPlayer;

    private GameObject playerPrefab; //TODO: figure out how players are saved
    private List<GameObject> playerList;

    public void InitializeLocalPlayerStart()
    {
        localPlayer.transform.SetParent(null);
        DontDestroyOnLoad(localPlayer);
    }

    public void SpawnPlayer(GameObject spawnPoint)
    {
        Vector3 spawnPosition = CorrectSpawnHeight(spawnPoint.transform.position);
        GameObject player = Instantiate(playerPrefab, spawnPosition, spawnPoint.transform.rotation);
        playerList.Add(player);
    }

    public void DestroyPlayer(GameObject player)
    {
        playerList.Remove(player);
        Destroy(player);
    }

    //Get a height, where the player is exactly standing on the ground.
    private Vector3 CorrectSpawnHeight(Vector3 spawnPosition) //TODO: Figure this method out with a real example
    {
        return spawnPosition;
    }
}
