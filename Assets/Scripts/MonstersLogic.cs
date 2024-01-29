using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonstersLogic : MonoBehaviour
{
    public static MonstersLogic Instance;

    [SerializeField] private GameObject _teethParent;
    [SerializeField] public int MonstersCounter = 0;

    [SerializeField] private GameObject monsterPrefab;

    private List<Teeth> _teeths = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _teeths = _teethParent.GetComponentsInChildren<Teeth>().ToList();
        Timer.Instance.startFirstRound();
        //RespawnLogic();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.monstersKilled == GameManager.Instance.maxMonsters)
        {
            GameManager.Instance.nextRound();
            //RespawnLogic();
            Timer.Instance.startRound();
        }
    }

    public void RespawnLogic()
    {
        if (GameManager.Instance.monstersKilled + Instance.MonstersCounter < GameManager.Instance.maxMonsters)
        {
            int index = CalculateFreeTeeth(Random.Range(0, _teeths.Count()), _teeths);

            if (index != -1)
            {
                Teeth teeth = _teeths[index];
                teeth.SpawnEnemy(monsterPrefab);
            }
            MonstersCounter++;
            Timer.Instance.setTime(GameManager.Instance.respawnTime);
        }
    }

    public void ChangeMonstserPlace(Monster monster)
    {
        int index = CalculateFreeTeeth(Random.Range(0, _teeths.Count()), _teeths);

        if(index != -1)
        {
            Teeth teeth = _teeths[index];
            if (teeth.NotOccupiedP && teeth._state != TeethState.DESTROYED && !teeth.topTeeth)
            {
                teeth.SetMonsterWithEffect(monster, 2.5f, 1.8f);
            }
            else
            {
                teeth.SetMonsterWithEffect(monster, -2f, 1.8f);
            }
        }
        else
        {
            Destroy(monster.gameObject);
        }
    }


    public int CalculateFreeTeeth(int index, List<Teeth> tempTeeths)
    {
        List<Teeth> temp = tempTeeths.ToList();

        if(temp.Count == 0)//if ((!temp[index].NotOccupiedP || temp[index]._state == TeethState.DESTROYED) && temp.Count > 0)
        {
            return -1;
        }
        else if (temp[index].NotOccupiedP && temp[index]._state != TeethState.DESTROYED)
        {
            return temp[index].transform.GetSiblingIndex();
        }
        else
        {
            temp.RemoveAt(index);
            return CalculateFreeTeeth(Random.Range(0, temp.Count()), temp);
        }
    }
}
