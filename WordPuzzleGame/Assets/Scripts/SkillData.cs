using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;
    public bool isUnlocked;
    public Vector2 position;
    public List<LineRenderer> lines;
    public SkillData[] dependencies;
}