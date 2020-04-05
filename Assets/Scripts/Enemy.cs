using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    public AnimationCurve percentTimerToShakeIntensifies
        = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    public Vector2 attackDelayMinMax = new Vector2(3, 4);
    public Vector2 scaleMinMax = new Vector2(.8f, 1.2f);
    public bool canBeAttacked = true;
    public bool isTryingToAttack = false;

    [SerializeField]
    RandomSound spawnSounds, hitSounds, launchSounds, landSounds, openSounds;

    void PlaySound(RandomSound sounds)
    {
        if (sounds) sounds.PlayRandom();
    }

    public bool TryAttack()
    {
        if (isTryingToAttack && !hasAttacked && gameTransform.GetLineIndex() == FattoCatto.GetLineIndex())
        {
            FattoCatto.DealDamageToFattoCatto();
            hasAttacked = true;
            Destroy(Instantiate(explosionPrefab, transform.position, transform.rotation), 5f);
            Destroy(gameTransform.gameObject);
            return true;
        }
        return false;
    }

    public void StartTryingAttack()
    {
        isTryingToAttack = true;
    }

    public void StopTryingAttack()
    {
        isTryingToAttack = false;
    }

    public bool TryToKill()
    {
        if (canBeAttacked)
        {
            enabled = false;
            animator.SetTrigger("Dead");
            canBeAttacked = false;
            isTryingToAttack = false;
            PlaySound(hitSounds);
            InvokeDestroy();
            Invoke("PlayLandSound", Random.Range(.7f,1f));
            Score.CanKilled();
            return true;
        }
        return false;
    }

    [HideInInspector]
    public GameTransform gameTransform;
    Animator animator = null;
    bool hasAttacked = false;
    float attackDelay = 7;
    float timer;
    bool yetAttacked = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameTransform = GetComponentInParent<GameTransform>();
        attackDelay = Random.Range(attackDelayMinMax.x, attackDelayMinMax.y);
        transform.localScale = new Vector3(
            Random.Range(scaleMinMax.x, scaleMinMax.y),
            Random.Range(scaleMinMax.x, scaleMinMax.y),
            1);
    }

    private void Start()
    {
        PlaySound(spawnSounds);
    }

    private void Update()
    {
        if (isTryingToAttack)
        {
            //TryAttack();
        }
        timer += Time.deltaTime;
        animator.SetFloat("Shake Intensifies", percentTimerToShakeIntensifies.Evaluate(timer / attackDelay));
        if (timer > attackDelay && !yetAttacked)
        {
            InvokeDestroy();
            animator.SetTrigger("Attack");
            PlaySound(openSounds);
            PlaySound(launchSounds);
            launchSounds.StartFadingOut(2.5f);
            yetAttacked = true;
        }
    }

    void InvokeDestroy()
    {
        
        Destroy(transform.parent.gameObject, 3);
        //enabled = false;
    }

    void PlayLandSound()
    {
        PlaySound(landSounds);
    }
}
