using Unity.Mathematics;
using UnityEngine;
public abstract class Gem
{
    public abstract string Name { get; }
    public virtual int MaxChildren { get; set; } = 0;
    public abstract void Execute(WeaponContext ctx);
}

public abstract class SpellGem : Gem
{
    
}

public abstract class ProjectileGem : SpellGem
{
    public abstract GameObject ProjectilePrefab { get; }
}

public abstract class ModifierGem : Gem
{
    //public abstract Modifier modifier { get; }
}

public class Root : Gem
{
    public override string Name => "Root";
    public Root(int MaxChildren = 1)
    {
        this.MaxChildren = MaxChildren;
    }
    public override void Execute(WeaponContext ctx)
    {
        return;
    }
}

public class Fireball : ProjectileGem
{
    public override string Name => "Fireball";
    public override GameObject ProjectilePrefab => Resources.Load<GameObject>("Prefabs/FireballProjectile");
    //public override int MaxChildren { get; set; }
    public Fireball(int MaxChildren = 0)
    {
        this.MaxChildren = MaxChildren;
    }
    public override void Execute(WeaponContext ctx)
    {
        GameObject projObject = GameObject.Instantiate(ProjectilePrefab, ctx.origin, Quaternion.identity);
        Projectile proj = projObject.GetComponent<Projectile>();
        proj.Initialize(ctx);
    }
}

public class Pierce : ModifierGem
{
    public override string Name => "Pierce";
    //public override int MaxChildren { get; set; }
    //public override Modifier modifier => new PierceModifier();
    public Pierce(int MaxChildren = 1)
    {
        this.MaxChildren = MaxChildren;
    }
    public override void Execute(WeaponContext ctx)
    {
        ctx.modifiers.Add(new PierceModifier());
    }
}

public class Volley : ModifierGem
{
    public override string Name => "Volley";
    public Volley(int MaxChildren = 1)
    {
        this.MaxChildren = MaxChildren;
    }
    public override void Execute(WeaponContext ctx)
    {
        ctx.modifiers.Add(new VolleyModifier());
    }
}