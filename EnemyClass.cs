using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    // Enemy tag
    public static readonly string EnemyTag = "Enemy";

    // Motions
    public static readonly string AttackMotion = "isAttacking";
    public static readonly string MoveMotion = "isMoving";
    public static readonly string ImpactMotion = "impact";
    public static readonly string DeathMotion = "death";
    public static readonly string RespawnMotion = "respawn";

    // Decay time
    public static readonly float DecayTime = 30f;
    // Putrefaction time
    public static readonly float PutrefactionTime = 60f;
    // Respawn time
    public static readonly float RespawnTime = 120f;
    // Devilish portal
    public static readonly string DevilishPortal = "Devilish Portal";

    // Enemy properties
    public string Nature { get; private set; }
    public string Type { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int MaxHealth { get; set; }
    public int CurHealth { get; set; }
    public int MaxDamage { get; set; }
    public int MinDamage { get; set; }
    public float AttackRate { get; set; }
    public float DetectRay { get; set; }
    public float AttackRay { get; set; }
    public string AttackType { get; set; }
    public int MinItemAmt { get; set; }
    public int MaxItemAmt { get; set; }
    public ItemDatabase.Item[][] ItemPool { get; set; }
    public int[] ItemChancePercent { get; set; }
    public float DeathTime { get; set; }
    public float SpawnTime { get; set; }
    public Vector3 RespawnPosition { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init(gameObject.name);
    }

    // Set basic parameters
    public void Init(string name)
    {
        // Search proper enemy
        for (int cnt = 0; cnt < EnemyDatabase.Enemies.Length; cnt++)
            // Check enemy name
            if (name.Equals(EnemyDatabase.Enemies[cnt].Type))
            {
                // Initialize enemy
                InitEnemy(EnemyDatabase.Enemies[cnt]);
                // Break action
                return;
            }
    }

    // Initialize proper enemy
    public void InitEnemy(EnemyDatabase.Enemy enemy)
    {
        // Set enemy parameters
        Nature = enemy.Nature;
        Type = enemy.Type;
        Level = enemy.Level;
        Experience = enemy.Experience;
        MaxHealth = enemy.MaxHealth;
        CurHealth = enemy.CurHealth;
        MaxDamage = enemy.MaxDamage;
        MinDamage = enemy.MinDamage;
        AttackRate = enemy.AttackRate;
        DetectRay = enemy.DetectRay;
        AttackRay = enemy.AttackRay;
        AttackType = enemy.AttackType;
        MinItemAmt = enemy.MinItemAmt;
        MaxItemAmt = enemy.MaxItemAmt;
        ItemPool = enemy.ItemPool;
        ItemChancePercent = enemy.ItemChancePercent;
        DeathTime = enemy.DeathTime;
        SpawnTime = enemy.SpawnTime;
        RespawnPosition = transform.position;
    }
}