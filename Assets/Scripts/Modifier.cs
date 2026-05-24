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
    public VolleyModifier(int count = 3)
    {
        this.count = count;
    }
    public void OnFire(Projectile proj)
    {
        // TODO: instantiate copies of proj (need ref to prefab, maybe remove volley modifier before copying)
        //       and set dir to +- degrees. interval within range [-70, 70]
    }
}