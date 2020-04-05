using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FattoCatto : MonoBehaviour
{
    public enum FattoAction
    {
        none, left, right, down, up
    }

    public ContactFilter2D filter;
    public float speed = 1;
    [SerializeField]
    RandomSound attackSound, dieSound, fallSound, happySound, idleSound, moveSound, flipSound;

    void PlaySound(RandomSound sounds)
    {
        if (sounds) sounds.PlayRandom();
    }

    public void PlayHappySound()
    {
        PlaySound(happySound);
    }

    public void PlayIdleSound()
    {
        PlaySound(happySound);
    }



    void Fall()
    {
        if (IsCattoAlive())
        {
            lives = 0;
            actionList.Clear();
            currentAction = FattoAction.none;
            animator.SetTrigger("Fall");
            PlaySound(fallSound);
            EndMessage.ShowMessage("You've fallen from table!");
        }
    }

    List<FattoAction> actionList = new List<FattoAction>();
    FattoAction currentAction = FattoAction.none;
    new Collider2D collider;
    bool isRushing = false;
    Animator animator = null;
    GameTransform gameTransform;
    int lives = 1;
    static FattoCatto fattoCatto;

    private void Awake()
    {
        fattoCatto = this;
        animator = GetComponent<Animator>();
        gameTransform = GetComponentInParent<GameTransform>();
        collider = GetComponent<Collider2D>();
    }

    Collider2D[] collisions = new Collider2D[100];

    private void Update()
    {
        UpdateInput();
        UpdateActionList();
        UpdateAttackReturn();
        UpdateGettingHit();
    }

    void UpdateGettingHit()
    {
        if (!IsCattoAlive()) return;
        var collisionAmount = collider.OverlapCollider(filter, collisions);
        for (int i = 0; i < collisionAmount; i++)
        {
            var c = collisions[i];
            var enemy = c.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TryAttack();
            }
        }
    }

    void UpdateAttackReturn()
    {
        switch (currentAction)
        {
            case FattoAction.left:
            case FattoAction.right:
                if (isRushing)
                {
                    gameTransform.x = Mathf.MoveTowards(gameTransform.x, -100, Time.deltaTime * speed * 1.5f);
                    if (gameTransform.x < -8) Fall();
                    var collisionAmount = collider.OverlapCollider(filter, collisions);
                    for (int i = 0; i < collisionAmount; i++)
                    {
                        var c = collisions[i];
                        var enemy = c.GetComponent<Enemy>();
                        if (enemy)
                        {
                            var enemyGTransform = enemy.GetComponentInParent<GameTransform>();
                            if (enemyGTransform && enemyGTransform.facingRight != gameTransform.facingRight
                                && GameLines.GetLineIndex(enemyGTransform) == GetLineIndex())
                            {
                                if (enemy.TryToKill())
                                {
                                    isRushing = false;
                                    animator.SetTrigger("Return");
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    gameTransform.x = Mathf.MoveTowards(gameTransform.x, 0, Time.deltaTime * speed);
                }
                break;
        }
        animator.SetBool("Idle", gameTransform.x == 0);
    }

    void UpdateActionList()
    {
        if (actionList.Count > 0 && IsIdle())
        {
            switch (currentAction = actionList[0])
            {
                case FattoAction.up:
                    gameTransform.MoveUp();
                    PlaySound(moveSound);
                    break;
                case FattoAction.down:
                    gameTransform.MoveDown();
                    PlaySound(moveSound);
                    break;
                case FattoAction.left:
                case FattoAction.right:
                    if (gameTransform.facingRight == (currentAction == FattoAction.right))
                    {
                        animator.SetTrigger("Attack");
                        isRushing = true;
                        PlaySound(attackSound);
                    }
                    else
                    {
                        animator.SetTrigger("Flip");
                        PlaySound(flipSound);
                    }
                    break;
            }
            actionList.RemoveAt(0);
        }
    }

    void UpdateInput()
    {
        if (Input.anyKeyDown && CanAddAction())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && CanMove())
            {
                actionList.Add(FattoAction.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && CanMove())
            {
                actionList.Add(FattoAction.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                actionList.Add(FattoAction.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                actionList.Add(FattoAction.right);
            }
        }
    }

    bool CanAddAction()
    {
        return IsCattoAlive();
        //return IsIdle();
    }

    bool CanMove()
    {
        return true;
        //return IsIdle();
    }

    bool IsIdle()
    {
        return IsCattoAlive() && animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle");
    }

    public static int GetLineIndex()
    {
        return fattoCatto.gameTransform.GetLineIndex();
    }

    public static void DealDamageToFattoCatto()
    {
        if (IsCattoAlive())
        {
            fattoCatto.lives--;
            fattoCatto.actionList.Clear();
            fattoCatto.currentAction = FattoAction.none;
            if (!IsCattoAlive())
            {
                fattoCatto.animator.SetTrigger("Die");
                fattoCatto.PlaySound(fattoCatto.dieSound);
                EndMessage.ShowMessage("The cans killed you!");
            }
        }
    }

    public static bool IsCattoAlive()
    {
        return fattoCatto.lives > 0;
    }

    public void RefreshFlip()
    {
        if (currentAction == FattoAction.right)
            gameTransform.facingRight = true;
        if (currentAction == FattoAction.left)
            gameTransform.facingRight = false;
        gameTransform.RefreshTransform();
    }

    public void TableShake()
    {
        Debug.Log("Table shake");
    }
}
