using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleDoorInteract : MonoBehaviour
{
    public Transform player;
    public float interactionRange = 2f;
    public float openAngle = 90f;
    public float openSpeed = 3f;

    private bool hasOpened = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        if (player == null)
        {
            GameObject playerObject = GameObject.Find("PlayerCapsule");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (hasOpened || player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool playerIsNear = distanceToPlayer <= interactionRange;
        bool pressedInteract = Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame;

        if (playerIsNear && pressedInteract)
        {
            hasOpened = true;
        }
    }

    private void LateUpdate()
    {
        if (hasOpened)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
    }
}
