using UnityEngine;

public abstract class Modifier
{
    public bool enabled = true;
    public virtual Modifier Clone(bool enabled = true)
    {
        Modifier m = CreateClone();
        m.enabled = enabled;
        return m;
    }
    public abstract Modifier CreateClone();
}

class SpeedModifier
{
    
}

class PierceModifier : Modifier, IOnHit
{
    int count;
    public PierceModifier(int count = 3)
    {
        this.count = count;
    }
    public override Modifier CreateClone()
    {
        return new PierceModifier(count);
    }
    public void OnHit(Projectile proj, GameObject hitObject)
    {
        if(hitObject.layer == LayerMask.NameToLayer("Enemy") &&
           proj.destroyFlag && count>0)
        {
            proj.destroyFlag = false;
            count--;
        }
    }
}

class VolleyModifier : Modifier, IOnFire
{
    int count;
    public VolleyModifier(int count = 5)
    {
        this.count = count;
    }
    public override Modifier CreateClone()
    {
        return new VolleyModifier(count);
    }
    public void OnFire(Projectile proj)
    {
        // spawn copies of the proj in a fan shape
        // count is # of additional projs

        if (count <= 0) return;

        float spread = 90f;
        float directionOffset = spread/2;
        float interval = spread / count;

        // set this proj to left most angle, then create copie of proj at increments to the right
        proj.AddDirectionOffset(directionOffset);

        for (int i=0; i<count; i++)
        {
            proj.CopyProjectile(Vector2.zero, -interval * (i+1), this);
        }
    }
}