using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private TMP_Text healthText;
    private Animator animator;
    private static readonly int hurtHash = Animator.StringToHash("hurt");

    void Start()
    {
        health = maxHealth;
        UpdateHealthText();

        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthText();
        
        if (animator != null) animator.SetTrigger(hurtHash);
    }

    public void UpdateHealthText()
    {
        if (healthText != null) healthText.text = $"{health}/{maxHealth}";
    }
}
