using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject weapon;
    private BaseStateMachine stateMachine;
    
    private void Start()
    {
        stateMachine = GetComponent<BaseStateMachine>();
    }

    public void EnableWeapon()
    {
        if (stateMachine.currentState is PlayerAttackState || stateMachine.currentState is EnemyAttackState)
        {
            weapon.SetActive(true);
        }
    }

    public void DisableWeapon()
    {
        weapon.SetActive(false);
    }
}
