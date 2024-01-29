using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToothBrush : Weapon
{
    public Animator PlayerAnimator;

    private SpriteRenderer toothBrushSpriteRenderer;
    private bool isCleaning;
    [SerializeField]
    private List<Teeth> currentlyTouchingTeeths;
    private readonly float cleaningTimeSeconds = 1;
    private readonly int checkInCleanTimeCount = 20;

    public void Start()
    {
        UpdateAction = new System.Action(PseudoUpdate);
        toothBrushSpriteRenderer = WeaponMain.GetComponent<SpriteRenderer>();
    }

    private void PseudoUpdate()
    {
        RotateToothBrushToMouseCursor();
    }

    public override void Fire()
    {
        if (!isCleaning)
        {
            if (currentlyTouchingTeeths.Any(x => x._state != TeethState.HEALTHY))
            {
                isCleaning = true;
                PlayerAnimator.SetBool("IsCleaning", isCleaning);
				AudioManager.Instance.PlayOneShot(AudioManager.Instance.cleaningSound, transform.position);
                foreach (Teeth t in currentlyTouchingTeeths) //fixme leczy zdrowe zeby
                {
                    StartCoroutine(CheckIfElementIsStill(cleaningTimeSeconds, t));
                }
            }
            else
            {
                PlayerAnimator.Play("CannotClean");
            }
        }
    }

    private void OnDisable()
    {
        FireStop();
        currentlyTouchingTeeths.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == TagsConstants.TeethTag)
        {
            Teeth t = go.GetComponent<Teeth>();
            currentlyTouchingTeeths.Add(t);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == TagsConstants.TeethTag)
        {
            Teeth t = go.GetComponent<Teeth>();
            currentlyTouchingTeeths.Remove(t);
        }
    }

    private IEnumerator CheckIfElementIsStill(float checkForSeconds, Teeth t)
    {
        float checkWaitSeconds = cleaningTimeSeconds / checkInCleanTimeCount;

        yield return new WaitForSecondsRealtime(checkWaitSeconds);
        bool isElementOnList = currentlyTouchingTeeths.Contains(t);

        if (checkForSeconds > 0 && isElementOnList && isCleaning)
        {
            StartCoroutine(CheckIfElementIsStill(checkForSeconds - checkWaitSeconds, t));
        }
        else
        {
            if (isElementOnList && checkForSeconds <= 0)
            {
                t.HealTeeth();
                GameManager.Instance.decrementCorruption();
            }
            FireStop();
        }
    }

    public override void FireStop()
    {
        isCleaning = false;
        PlayerAnimator.SetBool("IsCleaning", isCleaning);
    }

    private void RotateToothBrushToMouseCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = WeaponMain.transform.position;

        toothBrushSpriteRenderer.flipY = !(mousePosition.x > playerPosition.x);
    }
}
