using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavController))]
public class zombie : MonoBehaviour
{
    [SerializeField] private Animator zombieAnimator;
    [SerializeField] private NavController navController;
    private static readonly int walkState = Animator.StringToHash("Walk");
    private static readonly int attackState = Animator.StringToHash("Attack1");
    private static readonly int deathState = Animator.StringToHash("Death");

    [SerializeField] private GameObject target;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(zombieAnimator == null)
        {
            zombieAnimator = GetComponent<Animator>();
        }
        if(navController == null)
        {
            navController = GetComponent<NavController>();
        }
        navController.agent.enabled = true;
        navController.agent.isStopped = false;
        
        ResetAnimation();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!zombieAnimator.GetBool(deathState))
        {
            
        
        if(navController.agent.velocity.magnitude > 0.1f)
        {
            zombieAnimator.SetBool(walkState, true);
            zombieAnimator.SetFloat("Speed", navController.agent.velocity.magnitude);
        }
        else
        {
            zombieAnimator.SetBool(walkState, false);
        }
        if(target != null)
        {
            navController.SetTarget(target.transform.position);
        }else{
            target = GameObject.FindGameObjectWithTag("Player");
        }
        }
    }

    public void Death()
    {
        if(zombieAnimator.GetBool(deathState))
        {
            return;
        }
        zombieAnimator.SetBool(deathState, true);
        navController.agent.isStopped = true;
        navController.agent.enabled = false;
        GetComponent<Collider>().enabled = false;
        ZombieCounter.AddDeadZombie();
        Invoke("DestroyZombie", 30f);
    }

    private void DestroyZombie()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void ResetAnimation()
    {
        GetComponent<Collider>().enabled = true;
        zombieAnimator.SetBool(deathState, false);
        zombieAnimator.SetBool(attackState, false);
        zombieAnimator.SetBool(walkState, false);
        zombieAnimator.Play("ZombieFisherman_Torch_LookAround");
    }

}
