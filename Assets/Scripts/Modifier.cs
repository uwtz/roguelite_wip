using NUnit.Framework.Internal;
using UnityEngine;

public abstract class Modifier
{
    public bool enabled = true; // only add modifier to projectile if it is enabled
    public virtual Modifier Clone(bool enabled = true) // always clone with enabled on at the moment, set enable false seperately (eg. in Projectile.cs using index)
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
    public PierceModifier(PierceGemData pierceGemData)
    {
        count = pierceGemData.count;
    }
    public PierceModifier(int count)
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
    int spread;
    public VolleyModifier(VolleyGemData volleyGemData)
    {
        count = volleyGemData.count;
        spread = volleyGemData.spread;
    }
    public VolleyModifier(int count, int spread)
    {
        this.count = count;
        this.spread = spread;
    }
    public override Modifier CreateClone()
    {
        return new VolleyModifier(count, spread);
    }
    public void OnFire(Projectile proj)
    {
        // spawn copies of the proj in a fan shape
        // count is # of additional projs

        if (count <= 0) return;

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

class BounceModifier : Modifier, IOnHit
{
    int count;
    public BounceModifier(BounceGemData bounceGemData)
    {
        count = bounceGemData.count;
    }
    public BounceModifier(int count)
    {
        this.count = count;
    }
    public override Modifier CreateClone()
    {
        return new BounceModifier(count);
    }
    public void OnHit(Projectile proj, GameObject hitObject)
    {
        if (hitObject.layer == LayerMask.NameToLayer("Wall") &&
            proj.destroyFlag && count>0) // TODO: formalize process of limiting the number of onhit modifiers that prevent proj from destroying from activating to only one
        {
            Debug.Log("onhit bounce");
            Vector2 dir = proj.GetDirection();
            // need to use GetMask() instead of NameToLayer() below because raycast need a bit mask (in the form of a int) and not a single layer index
            RaycastHit2D hit = Physics2D.Raycast(proj.transform.position, dir, 1f, LayerMask.GetMask("Wall"));
            
            if (hit.collider != null) // TODO: might need to check if the raycasthit object is the same as the hitObject
            {
                //Debug.Log($"hit {hit.collider.gameObject.name}");
                var newDir = Vector2.Reflect(proj.GetDirection(), hit.normal);
                proj.SetDirection(newDir);
            }

            proj.destroyFlag = false;
            count--;
        }
    }
}