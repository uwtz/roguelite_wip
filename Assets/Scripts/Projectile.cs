using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int spd;
    int dmg;
    [SerializeField] float maxLifeTime;
    float lifeTime;
    Vector2 dir = new Vector2(0,0); // if not set, proj remain stationary
    Rigidbody2D rb;
    [HideInInspector] public bool destroyFlag; // if true, mark gameobject to destroy

    public void Initialize(WeaponContext ctx)
    {
        dir = ctx.dir; // TODO: might need to do ctx.dir.normalized;
        AddModifier(ctx);
    }

    // called in FixedUpdate().
    // updates projectile's rigidbody2D's linear velocity
    void Move()
    {
        rb.linearVelocity = spd * Time.deltaTime * dir;
    }


    // TODO: collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: dmg, call onhit(), check for pierce/chain, check for wall bounce
        Destroy(gameObject);
    }



    #region modifier subscription
    private List<IOnFire> onFireListeners = new();
    private List<IOnTick> onTickListeners = new();
    private List<IOnHit> onHitListeners = new();
    private List<IOnExpire> onExpireListeners = new();
    
    // each modifier implements different 'On' interfaces,
    // call the respective 'On' method throughout the projectile's lifetime.
    public void AddModifier(Modifier m)
    {
        // cast once on add, not on every event
        if (m is IOnFire   f) onFireListeners.Add(f);
        if (m is IOnHit    h) onHitListeners.Add(h);
        if (m is IOnTick   t) onTickListeners.Add(t);
        if (m is IOnExpire e) onExpireListeners.Add(e);
    }
    public void AddModifier(WeaponContext ctx)
    {
        foreach (Modifier m in ctx.modifiers)
            {AddModifier(m);}
    }

    void OnFire()
    {
        foreach(IOnFire m in onFireListeners) m.OnFire(this);
    }
    void OnTick()
    {
        foreach(IOnTick m in onTickListeners) m.OnTick(this);
    }
    void OnHit()
    {
        // on hit, determine if there exist modifiers that allow proj to live
        // eg. pierce, chain
        // modifiers closer to root gets 'spent' first, marking the destroyFlag false

        destroyFlag = true;
        foreach(IOnHit m in onHitListeners) m.OnHit(this, null);
        if(destroyFlag) Destroy(gameObject);
    }
    // TODO: call when destroyFlag is true. need to make sure is only called once
    // maybe lateupdate, after onhit is checked
    // refresh lifetime on hit
    void OnExpire()
    {
        foreach(IOnExpire m in onExpireListeners) m.OnExpire(this);
    }
    #endregion

    void Awake()
    {
        OnFire();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        OnTick();

        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime) Destroy(gameObject);
    }
    void FixedUpdate()
    {
        Move();
    }
}
