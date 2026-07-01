using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    int maxMana;
    int mana;
    float castCooldown;
    int branchCount;
    WeaponTree weaponTree;
    public WeaponTreeData weaponTreeData;


    //Ryker was here :3
    void Start()
    {
        // create weapon tree, use weaponTreeData if it exist
        // assume root node to be at index 0
        weaponTree = new WeaponTree();
        if (weaponTreeData != null && weaponTreeData.weaponTreeNodeDatas.Length > 0)
        {
            weaponTree.root = BuildWeaponTreeNode(0);
        }
    }
    
    // keep track of nodes used in BuildWeaponTreeNode() to prevent loops in tree and inf recursion
    List<int> usedNodeDataIndices = new();

    // create weapon tree recursively from weaponTreeData
    // param - int - nodeDataIndex - index of the node to build in weaponTreeData.nodeDatas
    // ret - WeaponTreeNode - ref to node along with its children built
    WeaponTreeNode BuildWeaponTreeNode(int nodeDataIndex)
    {
        // inf recursion prevention
        if (usedNodeDataIndices.Contains(nodeDataIndex))
        { Debug.Log($"Loop detected in weapon tree data!!! Index {nodeDataIndex} already used.\nUsed indices: {string.Join(',', usedNodeDataIndices)}"); return null; }
        else
        { usedNodeDataIndices.Add(nodeDataIndex); }

        // node creation
        WeaponTreeNodeData nodeData = weaponTreeData.weaponTreeNodeDatas[nodeDataIndex];
        Gem gem = CreateGem(nodeData.gem);
        WeaponTreeNode node = new WeaponTreeNode { gem = gem };

        // children recursion
        node.children =  new WeaponTreeNode[nodeData.childIndices.Length];
        for (int i=0; i<nodeData.childIndices.Length; i++)
        {
            node.children[i] = BuildWeaponTreeNode(nodeData.childIndices[i]);
        }

        return node;
    }

    Gem CreateGem(GemData gemData)
    {
        // TODO: use enum instead of string?
        Gem gem = null;
        switch(gemData.name)
        {
            case "Root" : gem = new Root(); break;
            case "Fireball": gem = GemHelper.Instance.CreateFireball(); break;
            case "Volley": gem = new Volley(); break;
            case "Pierce": gem = new Pierce(); break;
            default: Debug.Log($"Failed to create gem: {name}"); break;
        }
        gem.maxChildren = gemData.maxChildren;
        return gem;
    }


    public void Cast(Vector2 origin, Vector2 dir)
    {
        Debug.Log($"Casting {gameObject.name}\nOrigin: {origin}, Direction: {dir}");
        //Debug.Log(weaponTree.ToString());
        WeaponContext ctx = new WeaponContext(origin, dir);
        if (weaponTree.root != null)
        {
            CastRecursive(weaponTree.root, ctx);
        }
    }


    // DFS, recursively execute gems preorder, pass copy of weapon ctx to children
    void CastRecursive(WeaponTreeNode node, WeaponContext ctx)
    {
        //Debug.Log(n.gem.Name);
        // TODO: print out list of nodes in each path (print at leaf? or spell gem?)
        // e.g. Root => Volley => Fireball
        //      Root => AnotherFireball
        // maybe store traveled nodes in weaponcontext, or pass off recursively

        node.gem.Execute(ctx);

        foreach(WeaponTreeNode child in node.children)
        {
            WeaponContext ctxCopy = new WeaponContext(ctx);
            CastRecursive(child, ctxCopy);
        }

        ctx = null; // help with garage collect? idk if needed
    }
}

// hold temp info for spells to use
// set at Cast(), duplicated at each node to accomodate for branching
public class WeaponContext
{
    public Vector2 origin;
    public Vector2 dir;
    public List<Modifier> modifiers;
    public GameObject projectilePrefab; // not used atm pretty sure
    public WeaponContext()
    {
        origin = new Vector2(0,0);
        dir = new Vector2(0,0);
        modifiers = new List<Modifier>();
    }
    public WeaponContext(Vector2 origin, Vector2 dir)
    {
        this.origin = origin;
        this.dir = dir;
        modifiers = new List<Modifier>();
    }
    public WeaponContext(WeaponContext ctx)
    {
        this.origin = ctx.origin;
        this.dir = ctx.dir;
        this.modifiers = ctx.modifiers.ConvertAll(m => m.Clone());
        //this.modifiers = new List<Modifier>(ctx.modifiers); // copy modifer list so copies of ctx dont ref the same list
        this.projectilePrefab = ctx.projectilePrefab;
    }
}