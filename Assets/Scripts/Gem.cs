using UnityEngine;

public enum GemType
{
    Root,
    Fireball,
    Pierce,
    Volley,
    Bounce
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
    RootGemData rootGemData;
    public Root(RootGemData rootGemData)
    {
        this.rootGemData = rootGemData;
    }
    public override void Execute(WeaponContext ctx)
    {
        return;
    }
}

public class Fireball : ProjectileGem
{
    public override string Name => "Fireball";
    FireballGemData fireballGemData;
    //public override GameObject projectilePrefab => Resources.Load<GameObject>("Prefabs/FireballProjectile");
    //public override int MaxChildren { get; set; }
    public Fireball(GameObject projectilePrefab, FireballGemData fireballGemData)
    {
        this.fireballGemData = fireballGemData;
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
    PierceGemData pierceGemData;
    //int count;
    //public override int MaxChildren { get; set; }
    //public override Modifier modifier => new PierceModifier();
    public Pierce(PierceGemData pierceGemData)
    {
        this.pierceGemData = pierceGemData;
    }
    public override void Execute(WeaponContext ctx)
    {
        ctx.modifiers.Add(new PierceModifier(pierceGemData));
    }
}

public class Volley : ModifierGem
{
    public override string Name => "Volley";
    VolleyGemData volleyGemData;
    public Volley(VolleyGemData volleyGemData)
    {
        this.volleyGemData = volleyGemData;
    }
    public override void Execute(WeaponContext ctx)
    {
        ctx.modifiers.Add(new VolleyModifier(volleyGemData));
    }
}

public class Bounce : ModifierGem
{
    public override string Name => "Bounce";
    BounceGemData bounceGemData;
    public Bounce(BounceGemData bounceGemData)
    {
        this.bounceGemData = bounceGemData;
    }
    public override void Execute(WeaponContext ctx)
    {
        ctx.modifiers.Add(new BounceModifier(bounceGemData));
    }
}