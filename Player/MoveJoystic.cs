using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform rect_Background, rect_Joystic;
    public float radius, moveSpeed;
    public bool isTouch = false;
    public GameObject player;
    PlayerInform playerInfo;
    Vector3 movePosition;
    const string LATEST_SPEED = "LatestSpeed";

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = player.GetComponent<PlayerInform>();
        radius = rect_Background.rect.width * 0.4f;

        moveSpeed *= PlayerPrefs.GetFloat(LATEST_SPEED) + 1;
    }

    void FixedUpdate()
    {
        if (!playerInfo.isDead)
        {
            if (isTouch)
            {
                player.transform.position += movePosition;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_Background.position;

        value = Vector2.ClampMagnitude(value, radius);

        rect_Joystic.localPosition = value;

        value = value.normalized;
        // movePosition = new Vector3(value.x * moveSpeed * Time.deltaTime, value.y * moveSpeed * Time.deltaTime, 0);
        movePosition = new Vector3(value.x * moveSpeed, value.y * moveSpeed, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_Joystic.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
    }
}
