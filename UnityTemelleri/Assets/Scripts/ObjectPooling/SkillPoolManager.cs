using UnityEditor;
using UnityEngine;

public class SkillPoolManager : MonoBehaviour
{
    public static SkillPoolManager Instance;
    public Beam beamPrefab;
    public Meteor meteorPrefab;
    public ObjectPooler<Beam> beamPool;
    public ObjectPooler<Meteor> meteorPool;
    
    void Awake()
    {
        Instance = this;
        beamPool = new ObjectPooler<Beam>(beamPrefab, 10);
        meteorPool = new ObjectPooler<Meteor>(meteorPrefab, 10);
    }
}