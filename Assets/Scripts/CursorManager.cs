using UnityEngine;

/// <summary>
/// Checks position of the cursor and adapts its texture.
/// </summary>
public class CursorManager : MonoBehaviour
{
    // Maximal click distance
    private readonly int MaxDist = 100;
    // Game interface
    private GameInterface _gameInterface;
    // Raycast hit
    private RaycastHit _raycastHit;
    // Isometric camera
    private Camera _iso;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckCursorPosition();
    }

    // Set basic parameters
    private void Init()
    {
        _iso = Camera.main.gameObject.GetComponent<Camera>();
        _gameInterface = GameObject.Find(GameInterface.GameInterfaceController).GetComponent<GameInterface>();
    }

    /// <summary>
    /// Sets proper texture of the cursor.
    /// </summary>
    /// <param name="texture">A current texture to set.</param>
    private void SetCursorTexture(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// Checks position of the cursor and searches proper textures.
    /// </summary>
    private void CheckCursorPosition()
    {
        // Check specific case and set standard cursor
        if (GameMouseAction.IsMouseOverUI() || !_iso.isActiveAndEnabled || _gameInterface.IsGamePaused)
        {
            // Set standard cursor
            SetCursorTexture(CursorDatabase.Pointers[0].Texture);
            // Break action
            return;
        }
        // Check hit
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _raycastHit, MaxDist))
            // Break action
            return;
        // Search proper pointer
        for (int cnt1 = 0; cnt1 < CursorDatabase.Pointers.Length; cnt1++)
            // Search proper tag
            for (int cnt2 = 0; cnt2 < CursorDatabase.Pointers[cnt1].Tag.Length; cnt2++)
                // Check current tag
                if (_raycastHit.collider.tag.Equals(CursorDatabase.Pointers[cnt1].Tag[cnt2]))
                {
                    // Set proper cursor texture
                    SetCursorTexture(CursorDatabase.Pointers[cnt1].Texture);
                    // Break action
                    return;
                }
    }
}