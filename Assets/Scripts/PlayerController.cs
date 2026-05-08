using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;


    // [6] set the range of the player's movement in x-axis
    public float xRange = 10;

    // [8] declare Projectile prefab variable
    public GameObject projectilePrefab;
    public GameObject HomingBulletPrefab;
    public GameObject bomberBulletPrefab;

    public string moveActionName = "MoveP1";
    public string shootActionName = "ShootP1";

    private float horizontalInput;

    // [1] declare a private InputAction variable
    private InputAction moveAction;
    // [10] declare a private InputAction variable for shooting
    private InputAction shootAction;
    public void ApplySpeedBoost(float amount, float time)
    {
        speed += amount;
        // (เสริม) ถ้าอยากให้ความเร็วกลับมาเท่าเดิมหลังผ่านไปกี่วินาที ให้ใช้ Invoke
        Invoke("ResetSpeed", time);
    }

    void ResetSpeed()
    {
        speed = 10.0f; // กลับไปเป็นความเร็วปกติของคุณ
    }

    private void Awake()
    {
        // [2] find the action by name
        // this is to optimize the search for the action
        moveAction = InputSystem.actions.FindAction(moveActionName);

        // [11] find the action by name
        shootAction = InputSystem.actions.FindAction(shootActionName);
    }

    // Update is called once per frame
    void Update()
    {
        // [3] use input system to get horizontal input
        horizontalInput = moveAction.ReadValue<Vector2>().x;

        // [4] move the player
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);



        // [5] keep the player inbounds
        // if (transform.position.x < -10)
        // {
        //     transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        // }

        // [7] keep the player inbounds using xRange variable
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // สร้างกระสุนพิซซ่าที่ตำแหน่งผู้เล่น
            Instantiate(HomingBulletPrefab, transform.position, HomingBulletPrefab.transform.rotation);
        }
        
            


            // [12] check if the player is shooting
            if (shootAction.triggered)
            {
                // [13] spawn a projectile
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            }


        }
    }
