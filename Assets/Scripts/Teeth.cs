using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : MonoBehaviour
{
    [SerializeField] private Sprite[] _teethSprites;
    [SerializeField] private bool _notOccupied = true;
    [SerializeField] public GameObject OccupiedUnit;

    public bool topTeeth;

    private Teeth Instance = null;

    public TeethState _state = TeethState.HEALTHY;

    public bool NotOccupiedP => _notOccupied && OccupiedUnit == null;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Instance = this;
    }

    public void ChangeTeethState(TeethState state)
    {
        switch (state)
        {
            case TeethState.HEALTHY:
                _state = TeethState.HEALTHY;
                _spriteRenderer.sprite = _teethSprites[0];
                //_spriteRenderer.color = Color.green;
                break;
            case TeethState.CORRUPTED:
                _state = TeethState.CORRUPTED;
                _spriteRenderer.sprite = _teethSprites[1];
                //_spriteRenderer.color = Color.yellow;
                break;
            case TeethState.DESTROYED:
                _state = TeethState.DESTROYED;
                _spriteRenderer.sprite = _teethSprites[2];
                //_spriteRenderer.color = Color.red;
                break;
            case TeethState.DYING:
                _state = TeethState.DYING;
                //_spriteRenderer.sprite = _teethSprites[3];
                //_spriteRenderer.color = Color.black;
                break;
        }
    }

    public void CheckTeethState(Teeth teeth, StateOption option)
    {
        TeethState state = TeethState.HEALTHY;

        if (_state == TeethState.HEALTHY && option == StateOption.DAMAGE && !GameManager.Instance.Ending)
        {
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.corruptTeeth, transform.position);
            state = TeethState.CORRUPTED;
        }
        else if (_state == TeethState.CORRUPTED && option == StateOption.DAMAGE)
        {
            state = TeethState.DESTROYED;
        }
        else if (_state == TeethState.DESTROYED && option == StateOption.DAMAGE)
        {
            state = TeethState.DYING;
        }
        else if (_state == TeethState.CORRUPTED && option == StateOption.HEAL)
        {
            state = TeethState.HEALTHY;
        }
        else if (_state == TeethState.DESTROYED && option == StateOption.HEAL)
        {
            state = TeethState.CORRUPTED;
        }

        ChangeTeethState(state);
    }

    public void SpawnEnemy(GameObject monsterPrefab)
    {
        Vector3 offset = transform.position;
        if (!topTeeth)
        {
            offset.y += 2.5f;
        }
        else
        {
            offset.y += -2f;
        }
        GameObject monster = Instantiate(monsterPrefab, offset, Quaternion.identity);
        OccupiedUnit = monster;
        monster.GetComponent<Monster>().occupiedTile = this.gameObject;
    }

    public void ChangePositionOfEnemy(GameObject monster, Teeth teeth)
    {
        if (teeth.OccupiedUnit == null && teeth.NotOccupiedP)
        {
            Vector3 offset = teeth.transform.position;
            offset.y += 1.8f;
            monster.transform.position = offset;
            teeth.OccupiedUnit = monster;
            OccupiedUnit = null;
            monster.GetComponent<Monster>().occupiedTile = teeth.gameObject;
            monster.GetComponent<Monster>().occupiedTeeth = teeth;
        }
    }

    public void SetMonsterWithEffect(Monster monster, float offsetValue, float moveDuration)
    {
        if (OccupiedUnit == null && NotOccupiedP && monster.occupiedTeeth != this)
        {
            StartCoroutine(MoveUnitWithEffect(monster, offsetValue, moveDuration));
        }
    }

    public void HealTeeth()
    {
        ChangeTeethState(TeethState.HEALTHY);
    }

    private IEnumerator MoveUnitWithEffect(Monster monster, float offsetValue, float moveDuration)
    {
        if (monster.occupiedTeeth != null)
        {
            monster.occupiedTeeth.OccupiedUnit = null;
        }

        OccupiedUnit = monster.gameObject;
        monster.occupiedTeeth = this;
        monster.occupiedTile = this.gameObject;

        Vector3 startPosition = monster.transform.position;
        Vector3 targetPosition = transform.position + new Vector3(0f, offsetValue, 0f);

        float elapsedTime = 0f;

        if(monster == null || monster.gameObject == null)
        {
            yield break;
        }

        while (elapsedTime < moveDuration)
        {
            if (monster == null || monster.gameObject == null)
            {
                yield break;
            }
            monster.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (monster == null || monster.gameObject == null)
        {
            yield break;
        }

        if (monster.gameObject)
        {
            monster.transform.position = targetPosition;
        }

    }
}

public enum TeethState
{
    HEALTHY,
    CORRUPTED,
    DYING,
    DESTROYED
}

public enum StateOption
{
    HEAL,
    DAMAGE
}

