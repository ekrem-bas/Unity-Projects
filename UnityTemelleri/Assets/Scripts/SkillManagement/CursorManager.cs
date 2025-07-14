using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D beamCursor;
    public Vector2 hotspot;
    public GameObject areaIndicatorPrefab;
    private GameObject areaIndicatorInstance;
    public SkillManager skillManager;

    void Start()
    {
        hotspot = new Vector2(beamCursor.width / 2, beamCursor.height / 2);
    }

    void LateUpdate()
    {
        if (GameManager.instance.isPlayerDead)
        {
            ResetCursor();
            HideAreaIndicator();
            return; // Hiçbir şey yapma
        }
    }

    void Update()
    {
        int currentSkill = skillManager.selectedSkill;
        if (areaIndicatorInstance != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
            {
                areaIndicatorInstance.SetActive(true);
                areaIndicatorInstance.transform.position = hit.point + Vector3.up * 0.01f;
                if (Input.GetMouseButtonDown(0))
                {
                    skillManager.OnMeteorAreaSelected(hit.point);
                    HideAreaIndicator(); // Alan seçildiğinde göstergeleri gizle
                    ResetCursor();
                }
            }
            else
            {
                areaIndicatorInstance.SetActive(false);
            }
        }

        if (currentSkill == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        skillManager.OnBeamTargetSelected(hit.collider.gameObject);
                        ResetCursor();
                    }
                    else
                    {
                        Debug.Log("Beam skill hedefi bir düşman değil: " + hit.collider.name);
                        skillManager.ResetSkill(); // Eğer hedef düşman değilse skill'i resetle
                        ResetCursor(); // Eğer hedef düşman değilse cursor'u resetle,
                    }
                }
            }
        }
    }

    public void ShowAreaIndicator()
    {
        if (areaIndicatorInstance == null && areaIndicatorPrefab != null)
            areaIndicatorInstance = Instantiate(areaIndicatorPrefab);
    }

    public void HideAreaIndicator()
    {
        if (areaIndicatorInstance != null)
        {
            Destroy(areaIndicatorInstance);
            areaIndicatorInstance = null;
        }
    }

    public void SetCustomCursor(int skillIndex)
    {
        if (skillIndex == 0)
        {
            ShowAreaIndicator();
        }
        else if (skillIndex == 1)
        {
            Cursor.visible = true;
            Cursor.SetCursor(beamCursor, hotspot, CursorMode.Auto);
            HideAreaIndicator();
        }
    }

    public void ResetCursor()
    {
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        HideAreaIndicator();
    }
}