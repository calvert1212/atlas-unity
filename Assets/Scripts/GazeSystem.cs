using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GazeSystem : MonoBehaviour
{
    public float defaultDwellTime = 1.5f;
    public Image gazeProgressImage;
    public LayerMask uiLayer;
    public Transform cameraTransform;

    private float dwellTimer = 0f;
    private bool isGazing = false;
    private GameObject currentTarget;
    private float dwellTime;

    void Start()
    {
        dwellTime = defaultDwellTime;
    }

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, uiLayer))
        {
            if (hit.transform.gameObject != currentTarget)
            {
                ResetGaze();
                currentTarget = hit.transform.gameObject;
                isGazing = true;
            }

            if (isGazing)
            {
                dwellTimer += Time.deltaTime;
                gazeProgressImage.fillAmount = dwellTimer / dwellTime;

                if (dwellTimer >= dwellTime)
                {
                    ExecuteEvents.Execute(currentTarget, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
                    ResetGaze();
                }
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void ResetGaze()
    {
        dwellTimer = 0;
        gazeProgressImage.fillAmount = 0;
        isGazing = false;
        currentTarget = null;
    }

    public void SetDwellTime(float time)
    {
        dwellTime = time;
    }
}
