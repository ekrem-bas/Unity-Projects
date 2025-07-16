using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Cursor'ı değiştirmek için kullanılacak
    public CursorManager cursorManager;
    // // herhangi bir skill tıklandı mı kontrolü
    // public bool skillClicked = false;
    // bu frame'de input consume edildi mi kontrolü
    public bool inputConsumedThisFrame = false;
    // seçilen skill'in indexi
    public int selectedSkill = -1;
    // skillManager singleton örneği
    public static SkillManager instance;
    public PlayerData playerData;
    public GameObject meteorPrefab;
    Meteor meteorScript;
    public GameObject beamPrefab;
    Beam beamScript;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        meteorScript = meteorPrefab.GetComponent<Meteor>();
        beamScript = beamPrefab.GetComponent<Beam>();
    }
    void LateUpdate()
    {
        if (GameManager.instance.isGameOverScreenActivated)
        {
            selectedSkill = -1;
            ResetSkill();
            gameObject.SetActive(false);
            return;
        }

        inputConsumedThisFrame = false;
    }

    public void SelectSkill(int skillIndex)
    {
        // Kullanıcı öldüyse skill seçemez
        if (GameManager.instance.isPlayerDead) return;
        // eğer seçilen skill zaten tıklanmışşsa
        // ve yine aynı skill tıklanırsa
        // skill'i resetle
        if (GameManager.instance.isSkillSelected && selectedSkill == skillIndex)
        {
            ResetSkill();
        }
        else
        {
            // seçili skill'e göre cursor'ı değiştir
            selectedSkill = skillIndex;
            cursorManager.SetCustomCursor(skillIndex);
            // skill tıklandı olarak işaretle
            GameManager.instance.SetSkillSelected(true);
            // skillClicked = true;
        }
    }

    public void ResetSkill()
    {
        // skill'i resetle
        // skillClicked = false;
        GameManager.instance.SetSkillSelected(false);
        selectedSkill = -1;
        cursorManager.ResetCursor();
        cursorManager.HideAreaIndicator();
        // Input'u consume et
        inputConsumedThisFrame = true;
    }

    // Meteor düşünce ne olacağının fonksiyonu
    public void OnMeteorAreaSelected(Vector3 position)
    {
        Meteor meteor = SkillPoolManager.Instance.meteorPool.Get();

        // Meteor'u yukarıdan düşür - Y koordinatını yükseltin!
        Vector3 spawnPosition = new Vector3(position.x, position.y + meteorScript.meteorFallStartHeight, position.z);
        meteor.Init(spawnPosition, SkillPoolManager.Instance.meteorPool);

        ResetSkill();
    }

    // Beam skill hedef seçildiğinde ne olacağı
    public void OnBeamTargetSelected(GameObject enemy)
    {
        Beam beam = SkillPoolManager.Instance.beamPool.Get();
        beam.SetTarget(enemy.transform);
        ResetSkill();
    }
}
