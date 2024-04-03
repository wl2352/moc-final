using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer;
    public Vector2 dir_to_player {  get; set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        dir_to_player = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance )
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
