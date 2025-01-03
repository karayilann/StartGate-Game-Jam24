using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeGenerator : MonoBehaviour
{
    public SkillData[] skills;
    public GameObject skillPrefab;
    public SpriteRenderer skillIcon;
    public GameObject linePrefab;
    private SkillData _afterSkill;
    private SkillData _dependency; // Maybe we can find new method for dependency skills
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            UnlockSkill(skills[0]);
            Debug.Log("Skill 1 Unlocked");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            UnlockSkill(skills[1]);
            Debug.Log("Skill 2 Unlocked");
        }if (Input.GetKey(KeyCode.Alpha3))
        {
            UnlockSkill(skills[2]);
            Debug.Log("Skill 2 Unlocked");
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            UnlockSkill(_dependency);
        }
    }
    
    [ContextMenu("Generate Skill Tree")]
    public void GenerateSkillTree()
    {
#if UNITY_EDITOR
        foreach(var skill in skills) {
            skill.lines.Clear();
            skill.isUnlocked = false;
        }
#endif
        
        for (int i = 0; i < skills.Length; i++)
        {
            SkillData skill = skills[i];
            GameObject skillObj = Instantiate(skillPrefab, transform);

            Vector2 position = new Vector2(skill.position.x, skill.position.y);
            skillObj.transform.localPosition = position;

            skillIcon.sprite = skill.skillIcon;
            
            if (i == skills.Length - 1) return;
            _afterSkill = skills[i + 1];
            GenerateConnections(position, new Vector2(_afterSkill.position.x, _afterSkill.position.y),_afterSkill);
            CheckDependecies(skill);
        }
    }
    
    void CheckDependecies(SkillData skill)
    {
        if (skill.dependencies.Length == 0) return;
        foreach (SkillData dependency in skill.dependencies)
        {
            GameObject skillObj = Instantiate(skillPrefab, transform);
            Vector2 position = new Vector2(dependency.position.x, dependency.position.y);
            skillObj.transform.localPosition = position;
            _dependency = dependency;
            GenerateConnections(position, new Vector2(skill.position.x, skill.position.y),dependency);
        }
    }
    
    void GenerateConnections(Vector2 start, Vector2 end, SkillData afterSkill)
    {
        GameObject lineObj = Instantiate(linePrefab, transform);
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        
        if(afterSkill != null)
        {
            afterSkill.lines.Add(lineRenderer);
        }
    }
    
    void UnlockSkill(SkillData targetSkill)
    {
        if (targetSkill.isUnlocked) return;
        targetSkill.isUnlocked = true;
        foreach (var line in targetSkill.lines)
        {
            line.material.color = Color.green;
            // MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            //
            // line.GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
        }
    }
}