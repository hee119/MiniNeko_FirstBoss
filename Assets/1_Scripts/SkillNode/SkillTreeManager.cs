using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField]
    public RectTransform contentRoot;
    public GameObject nodePrefab;
    public GameObject connectionPrefab;
    public SkillData[] allSkills;

    private Dictionary<SkillData, SkillNodeUI> nodes = new Dictionary<SkillData, SkillNodeUI>();
    private List<GameObject> connections = new List<GameObject>();

    private void Start()
    {
        BuildTree();
    }

    void BuildTree()
    {
        float startX = -400f;
        float startY = 200f;
        float xStep = 180;
        float yStep = -140;
        for (int i = 0; i < allSkills.Length; i++)
        {
            SkillData data = allSkills[i];
            GameObject g = Instantiate(nodePrefab, contentRoot);
            RectTransform rt = g.GetComponent<RectTransform>();
            int col = i % 4;
            int row = i / 4;
            rt.anchoredPosition = new Vector2(startX + col * xStep, startY + row * yStep);

            SkillNodeUI ui = g.GetComponent<SkillNodeUI>();
            ui.skillData = data;
            ui.SetRank(0);
            ui.onClicked += OnNodeClicked;

            nodes[data] = ui;
        }

        foreach(var kv in nodes)
        {
            SkillData data = kv.Key;
            SkillNodeUI fromUI = kv.Value;
            if (data.preRequisites == null) 
                continue;
            foreach(var pre in data.preRequisites)
            {
                if (pre == null) 
                    continue;
                if (!nodes.ContainsKey(pre))
                    continue;
                SkillNodeUI toUI = fromUI;
                SkillNodeUI fromPreUI = nodes[pre];
                CreateConnecton(fromPreUI.GetComponent<RectTransform>(), toUI.GetComponent<RectTransform>());
            }
        }
    }
    void CreateConnecton(RectTransform a, RectTransform b)
    {
        GameObject conn = Instantiate(connectionPrefab, contentRoot);
        RectTransform r = conn.GetComponent<RectTransform>();

        conn.transform.SetAsFirstSibling();

        Vector2 posA = a.anchoredPosition;
        Vector2 posB = b.anchoredPosition;

        Vector2 dir = posB - posA;
        float dist = dir.magnitude;
        r.sizeDelta = new Vector2(dist, r.sizeDelta.y);
        r.anchoredPosition = posA + dir * 0.5f;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        r.localRotation = Quaternion.Euler(0, 0, angle);

        connections.Add(conn);
    }
    void OnNodeClicked(SkillNodeUI node)
    {
        bool canUnlock = true;
        foreach(var pre in node.skillData.preRequisites)
        {
            if (pre == null)
            {
                Debug.LogError($"[ERROR] 스킬 데이터 '{node.skillData.skillName}'의 선행 스킬 배열에 NULL 참조가 있습니다. 에디터에서 확인하세요!", node.skillData);
                canUnlock = false;
                break;
            }
            if (!nodes.ContainsKey(pre) || nodes[pre].currentRank <= 0)
            {
                canUnlock = false;
                break;
            }
        }
        if (canUnlock && node.currentRank < node.skillData.maxRank)
        {
            node.SetRank(node.currentRank + 1);
            Debug.Log($"스킬 획득: {node.skillData.skillName}");
        }
        else
        {
            Debug.Log("선행 조건 부족 또는 이미 최대 레벨입니다.");
        }
    }
    public void SaveProgress()
    {
        foreach (var kv in nodes)
        {
            string key = "skill" + kv.Key.name;
            PlayerPrefs.SetInt(key, kv.Value.currentRank);
            PlayerPrefs.Save();
        }
    }
    public void LoadProgress()
    {
        foreach(var kv in nodes)
        {
            string key = "skill" + kv.Key.name;
            if (PlayerPrefs.HasKey(key))
                kv.Value.SetRank(PlayerPrefs.GetInt(key));
        }
    }
}