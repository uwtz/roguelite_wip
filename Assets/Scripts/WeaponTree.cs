using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class WeaponTree
{
    public WeaponTreeNode root; // root should have default behavior ie being able to branch to n children


    public override string ToString()
    {
        return ToStringRecur(root, "");
    }
    string ToStringRecur(WeaponTreeNode node, string indent)
    {
        string ret = $"{indent}|--{node.gem.Name}";
        foreach (WeaponTreeNode child in node.children)
        {
            ret += $"\n{ToStringRecur(child, $"{indent}\t")}";
        }
        return ret;
    }
}

public class WeaponTreeNode
{
    public Gem gem;
    public WeaponTreeNode[] children;
}