using UnityEngine;

public interface IOnFire
{void OnFire(Projectile proj);}

public interface IOnHit
{void OnHit(Projectile proj, GameObject target);}

public interface IOnTick
{void OnTick(Projectile proj);}

public interface IOnExpire
{void OnExpire(Projectile proj);}