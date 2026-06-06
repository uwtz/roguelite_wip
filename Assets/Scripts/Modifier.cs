using UnityEngine;

public class Modifier
{
    
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
    public void OnHit(Projectile proj, GameObject target)
    {
        if(proj.destroyFlag && count>0)
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
            Debug.Log(directionOffset);

        for (int i=0; i<count; i++)
        {
            proj.CopyProjectile(Vector2.zero, -interval * (i+1), this);
        }
    }
}