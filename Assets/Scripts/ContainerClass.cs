using UnityEngine;

/// <summary>
/// Describes the object that represents the container.
/// </summary>
public class ContainerClass : MonoBehaviour
{
    // Container structure
    public string Kind { get; set; }
    public int MinItemAmt { get; set; }
    public int MaxItemAmt { get; set; }
    public ItemDatabase.Item[][] ItemPool { get; set; }
    public int[] ItemChancePercent { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init(transform.parent.name);
    }

    // Set basic parameters
    private void Init(string name)
    {
        // Search proper container
        for (int cnt = 0; cnt < ContainerDatabase.Containers.Length; cnt++)
            // Check container name
            if (name.Equals(ContainerDatabase.Containers[cnt].Kind))
            {
                // Initialize container
                InitContainer(ContainerDatabase.Containers[cnt]);
                // Break action
                return;
            }
    }

    /// <summary>
    /// Sets the basic parameters of the container for example kind or items amount inside.
    /// </summary>
    /// <param name="container">A type of container from database.</param>
    private void InitContainer(ContainerDatabase.Container container)
    {
        Kind = container.Kind;
        MinItemAmt = container.MinItemAmt;
        MaxItemAmt = container.MaxItemAmt;
        ItemPool = container.ItemPool;
        ItemChancePercent = container.ItemChancePercent;
    }

    /// <summary>
    /// Generates specific items when the hero is opening some container.
    /// </summary>
    public void GenerateContainerItems()
    {
        // Generated item amount
        int itemAmt = Random.Range(MinItemAmt, MaxItemAmt);
        // Set secound counter for item spread
        int cnt2 = 0;
        // Generate some items
        for (int cnt1 = 0; cnt1 < itemAmt; cnt1++)
        {
            // Random item rank
            int itemRank = Random.Range(0, ItemPool.Length);
            // Random item chance
            int itemChance = Random.Range(0, 100);
            // Check if item chance
            if (itemChance >= ItemChancePercent[itemRank])
                // Check another option
                continue;
            // Get some item from pool
            string itemKind = ItemPool[itemRank][Random.Range(0, ItemPool[itemRank].Length)].Kind;
            // Create item
            GameObject generatedItem = Instantiate(Resources.Load(ItemDatabase.Prefabs + itemKind),
                new Vector3(transform.parent.position.x, transform.parent.position.y,
                transform.parent.position.z), transform.parent.rotation) as GameObject;
            // Generate new name for item
            string newName = generatedItem.name.Replace(ItemClass.Clone, ItemClass.EmptySpace);
            // Change generated item name
            generatedItem.name = newName;
            // Check current item
            if ((cnt1 % 2).Equals(0))
            {
                // Change object rotation
                generatedItem.transform.rotation =
                    Quaternion.Euler(transform.parent.rotation.eulerAngles.x,
                    transform.parent.rotation.eulerAngles.y + cnt2 * ContainerDatabase.SpreadAngle,
                    transform.parent.rotation.eulerAngles.z);
                // Increment second counter
                cnt2++;
            }
            else
                // Change object rotation
                generatedItem.transform.rotation =
                    Quaternion.Euler(transform.parent.rotation.eulerAngles.x,
                    transform.parent.rotation.eulerAngles.y - cnt2 * ContainerDatabase.SpreadAngle,
                    transform.parent.rotation.eulerAngles.z);
            // Get item class
            ItemClass itemClass = generatedItem.GetComponentInChildren<ItemClass>();
            // Check if object is gold
            if (itemClass.Type.Equals(ItemDatabase.Gold))
                // Play loot animation
                generatedItem.GetComponentInChildren<Animator>().SetTrigger(ItemClass.LootGoldMotion);
            // Object is item
            else
                // Play loot animation
                generatedItem.GetComponentInChildren<Animator>().SetTrigger(ItemClass.LootItemMotion);
        }
    }
}