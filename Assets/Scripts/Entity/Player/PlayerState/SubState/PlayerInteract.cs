using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Player player;
    private PlayerCurrentState currentState;
    private PlayerDat playerData;
    private string v;

    public PlayerInteract(Player player, PlayerCurrentState currentState, PlayerDat playerData, string v)
    {
        this.player = player;
        this.currentState = currentState;
        this.playerData = playerData;
        this.v = v;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
