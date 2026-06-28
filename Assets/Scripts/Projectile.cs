using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int spd;
    [SerializeField] int dmg;
    [SerializeField] float maxLifeTime;
    float lifeTime;
    Vector2 dir = new Vector2(0,0); // if not set, proj remain stationary
    Rigidbody2D rb;
    [HideInInspector] public bool destroyFlag; // if true, mark gameobject to destroy
    [HideInInspector] public GameObject projectilePrefab; // set when initialized, used and passed on when copying self
    WeaponContext ctx;

    public void Initialize(WeaponContext ctx, GameObject projectilePrefab)
    {
        this.projectilePrefab = projectilePrefab;
        this.ctx = ctx;
        transform.position = ctx.origin;
        dir = ctx.dir; // TODO: might need to do ctx.dir.normalized;
        AddModifier(ctx);
    }

    // called in FixedUpdate().
    // updates projectile's rigidbody2D's linear velocity
    void Move()
    {
        rb.linearVelocity = spd * Time.deltaTime * dir;
    }

    // create a copy of this projectile 
    // able to set position and rotation offset using param
    public void CopyProjectile(Vector2 positionOffset = default, float directionOffset = 0f, Modifier modifierToDisable = null)
    {
        WeaponContext ctxCopy = new WeaponContext(ctx);

        // can disable modifier so projectiles dont copy themselves infinitely
        // set modifer.enabled to false to not add the modifier to the projectile when it initializes
        if (modifierToDisable != null)
        {
            int idx = ctx.modifiers.IndexOf(modifierToDisable);
            if (idx >= 0 && idx < ctxCopy.modifiers.Count)
            {
                ctxCopy.modifiers[idx].enabled = false;
            }
        }

        // use this projectile's current position and direction as the inital weapon context for the copied projectile
        ctxCopy.origin = (Vector2)transform.position + positionOffset;
        ctxCopy.dir = Quaternion.Euler(0, 0, directionOffset) * dir;

        GameObject projObjectCopy = GameObject.Instantiate(projectilePrefab);
        Projectile projCopy = projObjectCopy.GetComponent<Projectile>();
        projCopy.Initialize(ctxCopy, projectilePrefab);
        
        //projObjectCopy.transform.position += new Vector3(positionOffset.x, positionOffset.y);
        //AddDirectionOffset(directionOffset);

        // TODO: set rotation using dir in update
    }

    public void AddDirectionOffset(float offset)
    {
        dir = Quaternion.Euler(0, 0, offset) * dir;
    }

    // TODO: collision
    void OnTriggerEnter2D(Collider2D col)
    {
        // note that projectile's rigidbody and collider should already be set to ignore player and projectile layer
        OnHit(col.gameObject);
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
        //Debug.Log($"adding {m.GetType().Name}");
        
        // cast once on add, not on every event
        // ??? huh

        if (!m.enabled) return;
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
        //Debug.Log(onFireListeners.Count);
        foreach(IOnFire m in onFireListeners)
        {
            //Debug.Log(m.GetType().Name);
            m.OnFire(this);
        }
    }

    void OnTick()
    {
        foreach(IOnTick m in onTickListeners) m.OnTick(this);
    }

    void OnHit(GameObject hitObject)
    {
        // on hit, determine if there exist modifiers that allow proj to live
        // eg. pierce, chain
        // modifiers closer to root gets 'spent' first, marking the destroyFlag false

        if (hitObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(dmg);
        }

        destroyFlag = true;
        foreach(IOnHit m in onHitListeners)
            m.OnHit(this, hitObject);
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
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        OnFire();
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
