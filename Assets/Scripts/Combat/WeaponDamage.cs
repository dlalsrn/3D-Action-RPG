using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public Collider myColldier;

    private int damage;
    private float knockback;

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myColldier == other)
        {
            return; // 자기 자신과 충돌하지 않도록
        }

        // Damage 적용
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // 넉백 적용
        ForceReceiver forceReceiver = other.GetComponent<ForceReceiver>();
        if (forceReceiver != null)
        {
            // ForceReceiver가 있다면 knockback 적용
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0; // 수평 방향으로만 knockback 적용
            forceReceiver.AddImpact(direction * knockback);
        }
    }
}
