using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotateSpeed = 200.0f; // ความเร็วในการหันหัวไปหาศัตรู
    private Transform target;

    void Start()
    {
        FindClosestEnemy();
    }

    void Update()
    {
        if (target == null)
        {
            // ถ้าไม่มีเป้าหมาย หรือเป้าหมายถูกทำลายไปแล้ว ให้วิ่งตรงไปข้างหน้าปกติ
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            FindClosestEnemy(); // พยายามหาเป้าหมายใหม่
            return;
        }

        // 1. คำนวณทิศทางไปยังเป้าหมาย
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // ล็อคแกน Y ไว้เพื่อไม่ให้กระสุนมุดลงดินหรือเหินขึ้นฟ้า

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // 2. ค่อยๆ หันหน้าไปหาเป้าหมาย (Rotate)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

        // 3. เคลื่อนที่ไปข้างหน้าตามทิศทางที่หันไป
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Animal");
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
    }

    // ทำลายเมื่อชนศัตรู
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

