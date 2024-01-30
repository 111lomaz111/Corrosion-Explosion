using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject occupiedTile;
    public Teeth occupiedTeeth;
    private float _timeRemaining;
    private bool _isRunning = false;

    public Animation deathAnim;

    public void Start()
    {
        _timeRemaining = 2f;
        _isRunning = true;
        occupiedTeeth = occupiedTile.GetComponent<Teeth>();
    }

    void Update()
    {
        if (_isRunning && occupiedTeeth._state != TeethState.DESTROYED)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                Attack();
                //_isRunning = false;
            }
        }
        else if (occupiedTeeth._state == TeethState.DESTROYED)
        {
            MonstersLogic.Instance.ChangeMonstserPlace(this);
            _timeRemaining = 2f;
            _isRunning = true;
        }
    }

    public void Attack()
    {
        _timeRemaining = 2f;
        GameManager.Instance.incrementCorruption();
        occupiedTeeth.CheckTeethState(occupiedTeeth, StateOption.DAMAGE);
    }

    public void DestroyMonster()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.monstersKilled++;
        MonstersLogic.Instance.MonstersCounter--;
    }
}
