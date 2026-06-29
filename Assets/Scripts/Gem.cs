using UnityEngine;

public enum GemType
{
    Root,
    Fireball,
    Pierce,
    Volley
}

public abstract class Gem
{
    public abstract string Name { get; }
    public int maxChildren = 0;
    public abstract void Execute(WeaponContext ctx);
}

public abstract class SpellGem : Gem
{
    
}

public abstract class ProjectileGem : SpellGem
{
    public GameObject projectilePrefab;
}

public abstract class ModifierGem : Gem
{
    //public abstract Modifier modifier { get; }
}

public class Root : Gem
{
    public override string Name => "Root";
    public Root(int maxChildren = 1)
    {
        this.maxChildren = maxChildren;
    }
    public override void Execute(WeaponContext ctx)
    {
        return;
    }
}

public class Fireball : ProjectileGem
{
    public override string Name => "Fireball";
    //public override GameObject projectilePrefab => Resources.Load<GameObject>("Prefabs/FireballProjectile");
    //public override int MaxChildren { get; set; }
    public Fireball(GameObject projectilePrefab, int maxChildren = 0)
    {
        this.maxChildren = maxChildren;
        this.projectilePrefab = projectilePrefab;
    }
    public override void Execute(WeaponContext ctx)
    {
        // store prefab in ctx for support gems to use eg. volley
        ctx.projectilePrefab = projectilePrefab;

        GameObject projObject = GameObject.Instantiate(projectilePrefab);
        Projectile proj = projObject.GetComponent<Projectile>();
        proj.Initialize(ctx, projectilePrefab);
    }
}

public class Pierce : ModifierGem
{
    public override string Name => "Pierce";
    //public override int MaxChildren { get; set; }
    //public override Modifier modifier => new PierceModifier();
    public Pierce(int maxChildren = 1)
    {
        this.maxChildren = maxChildren;
    }
    public override void Execute(WeaponContext ctx)
    {
        ctx.modifiers.Add(new PierceModifier());
    }
}

public class Volley : ModifierGem
{
    public override string Name => "Volley";
    public Volley(int maxChildren = 1)
    {
        this.maxChildren = maxChildren;
    }
    public override void Execute(WeaponContext ctx)
    {
        ctx.modifiers.Add(new VolleyModifier());
    }
}