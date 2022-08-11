using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public Tilemap map;

    public GameObject player;

    public bool isTicking = false;
    public Vector3Int targetCell;

    public bool reactToPlayerWithinRange = false;

    public float reactionTime = 2f;
    public float maxReactionTime = 2f;
    public int aquisitionRange = 3;

    public int minX = -2;
    public int maxX = 1;
    public int minY = -2;
    public int maxY = 2;

    public SpriteRenderer spriteRenderer;

    void Init()
    {
        isTicking = false;
        reactToPlayerWithinRange = false;
        reactionTime = 2;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        foreach (var e in Object.FindObjectsOfType<Enemy>()) {
            e.Init();
        }
    }

    Vector3Int RelativeCell(Vector2 direction)
    {
        return map.WorldToCell(transform.position + (Vector3)direction);
    }

    void Move()
    {
        if (map.WorldToCell(transform.position) != targetCell)
        {
            var diff = targetCell - map.WorldToCell(transform.position);

            Vector3Int nextcell;
            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            {
                nextcell = RelativeCell((new Vector2(diff.x, 0)).normalized);
            }
            else
            {
                nextcell = RelativeCell((new Vector2(0, diff.y)).normalized);
            }

            transform.position = map.CellToWorld(nextcell);
        }
    }

    bool InsideBounds(Vector3Int cell, int minX, int maxX, int minY, int maxY)
    {
        return cell.x <= maxX
            && cell.x >= minX
            && cell.y <= maxY
            && cell.y >= minY;
    }

    void Tick()
    {
        var curCell = map.WorldToCell(transform.position);
        var playerCell = map.WorldToCell(player.transform.position);
        var distance = Vector3Int.Distance(playerCell, curCell);

        if (distance <= aquisitionRange)
        {
            if (reactionTime > 0f)
            {
                reactToPlayerWithinRange = true;
                // change color
                // shake
                // change size
                // "jump"
            } else
            {

                reactToPlayerWithinRange = false;
                targetCell = playerCell;

                Move();
            }
        }
        else if (!InsideBounds(curCell, minX, maxX, minY, maxY))
        {
            targetCell = new Vector3Int(minX, minY);

            reactionTime = maxReactionTime;
            spriteRenderer.color = new Color(1f, 1f, 1f);
            reactToPlayerWithinRange = false;

            Move();
        }
        else
        {
            do
            {
                var diff = new Vector3Int(Random.Range(-2, 2),
                                          Random.Range(-2, 2),
                                          0);

                targetCell = map.WorldToCell(transform.position) + diff;
            } while (!InsideBounds(targetCell, minX, maxX, minY, maxY));

            reactionTime = maxReactionTime;
            spriteRenderer.color = new Color(1f, 1f, 1f);
            reactToPlayerWithinRange = false;

            Move();
        }
    }

    IEnumerator TickLoop()
    {
        isTicking = true;

        while (true)
        {
            Tick();

            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTicking)
        {
            StartCoroutine(TickLoop());
        }

        if (reactToPlayerWithinRange)
        {
            var t = 1 - (reactionTime / maxReactionTime);
            spriteRenderer.color = Color.Lerp(Color.white, new Color(1f, 0f, 0f), t);
            if (t < 0.5f)
                transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(1f, 1.5f, 1f), t * 2);
            else
                transform.localScale = Vector3.Lerp(new Vector3(1f, 1.5f, 1f), new Vector3(1f, 1f, 1f), (t - 0.5f) *  2f);
            reactionTime -= Time.fixedDeltaTime;
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
