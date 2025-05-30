using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E01PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(UpdatePosition());
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontal, 0, vertical);

        characterController.Move(direction * speed * Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(targetPosition);
        }
    }

    IEnumerator UpdatePosition()
    {
        while (true)
        {
            // GameManager에게 Player 위치 전달
            E01GameManager.Instance.UpdatePlayerPosition(characterController.transform.position);
            yield return new WaitForSeconds(1);
        }
    }
}
