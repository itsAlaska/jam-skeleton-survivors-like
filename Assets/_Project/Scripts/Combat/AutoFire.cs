using UnityEngine;

public class AutoFire : MonoBehaviour
{
    [Header("Firing")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 0.5f;
    [SerializeField] private float targetingRadius = 10f;
    [SerializeField] private LayerMask enemyLayers;

    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            FireAtNearestEnemy();
            fireTimer = 0f;
        }
    }

    private void FireAtNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRadius, enemyLayers);

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = hit.transform;
            }
        }

        Vector2 direction = Vector2.right; // default direction
        if (closestEnemy != null)
        {
            direction = (closestEnemy.position - firePoint.position).normalized;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Adjust speed as needed
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRadius);
    }
}