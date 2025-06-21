using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float CurrentHealthRatio => (float)CurrentHealth / MaxHealth;
    public int MaxHealth = 100;
    public int CurrentHealth;
    public bool IsDead => CurrentHealth <= 0;

    public event Action OnTakeDamage; // 데미지 받았을 때 이벤트
    public event Action OnDie; // 사망 이벤트

    private void Start()
    {
        CurrentHealth = MaxHealth; // 초기 체력을 최대 체력으로 설정
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return; // 이미 죽은 상태라면 더 이상 데미지를 받지 않음
        }

        CurrentHealth -= damage;
        
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0; // 체력이 0 이하로 떨어지지 않도록 제한

            OnDie?.Invoke(); // 사망 이벤트 호출

            return;
        }
        else
        {
            OnTakeDamage?.Invoke(); // 데미지를 받았을 때 이벤트 호출
        }
    }
}
