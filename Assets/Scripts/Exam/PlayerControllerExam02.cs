using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam02 : MonoBehaviour
{
    public float speed = 5f;
    public float zRange = 10f;
    public GameObject projectilePrefab;

    private float verticalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        shootAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        shootAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = moveAction.ReadValue<Vector2>().y;
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        float clampZ = Mathf.Clamp(transform.position.z, -zRange, zRange);
        transform.position = new Vector3(transform.position.x,transform.position.y, clampZ);

        if (shootAction.triggered)
        {
            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            bullet.transform.right = Vector3.right;
        }

    }
}
