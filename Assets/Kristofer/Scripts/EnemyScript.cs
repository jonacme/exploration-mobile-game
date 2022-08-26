using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace Kristofer.exploration
{
    public class EnemyScript : MonoBehaviour
    {
        #region Variables
        public Tilemap tileMap;
        public GameObject player;
        public SpriteRenderer spriteRenderer;
        public IState state;

        public bool playerInRange = false;
        public bool isMoving = false;
        public Vector3Int targetCell;

        public float reactionTime = 2f;
        public float maxReactionTime = 2f;
        public int targetingRange = 3;

        [Header("Grid Coordinates")]
        public int minX = -2;
        public int maxX = 1;
        public int minY = -2;
        public int maxY = 2;

        [Range(0.1f, 5f)]
        public float moveTimer = .3f;
        #endregion


        private void Init()
        {
            playerInRange = false;
            isMoving = false;
            reactionTime = 2f;
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            state = new GoToRandomPosition();
            

        }

        void Start()
        {
            Init();
        }


        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            foreach(var e in Object.FindObjectsOfType<EnemyScript>())
            {
                e.Init();

            }

        }


        Vector3Int RelativeCell(Vector2 direction)
        {


            return tileMap.WorldToCell(transform.position + (Vector3)direction);

        }

        

        void TickClasses()
        {

            state.Tick(this);
            var newState = state.Transition(this);
            if (newState != null) { state = newState; }

        }


        void TickIfStatements()
        {

            var currentCell = tileMap.WorldToCell(transform.position);

            var playerCell = tileMap.WorldToCell(player.transform.position);

            var distance = Vector3Int.Distance(playerCell, currentCell);

            if (distance <= targetingRange)
            {
                if(reactionTime > 0f)
                {
                    playerInRange = true;
                    
                   
                }
                else
                {
                    playerInRange = false;
                    targetCell = playerCell;
                    Move();
                }
                
            }
           


            else if (!TileUtils.InsideBounds(currentCell, minX, maxX, minY, maxY))
            {

                targetCell = new Vector3Int(minX, minY);
                reactionTime = maxReactionTime;
                spriteRenderer.color = new Color(1f, 1f, 1f);
                playerInRange = false;

                Move();
            }
            else 
            {
                do
                {
                    var diff = new Vector3Int(Random.Range(-2, 2),
                            Random.Range(-2, 2),
                            0);
                    targetCell = tileMap.WorldToCell(transform.position + diff);

                } while (!TileUtils.InsideBounds(targetCell, minX, maxX, minY, maxY));

                reactionTime = maxReactionTime;
                spriteRenderer.color = new Color(1f, 1f, 1f);
                playerInRange = false;
                 
                Move();
            }






        }

        
        private IEnumerator TickRoutine()
        {
            isMoving = true;
            //var loop = 500;
            while (true)
            {
                //loop--;
                //Debug.Log(loop);
                /*
                if (loop == 0)
                {
                    Debug.Log("endless loop");
                    yield break;
                }
                */
                    
                if(Loader.Instance.useClass)
                {
                    TickClasses();
                    //Debug.Log("classes");
                }
                else
                {

                    TickIfStatements();
                    //Debug.Log("if/else");
                }
                
             
                yield return new WaitForSeconds(moveTimer);

            }



        }

        public void Move()
        {

            
            
            if(tileMap.WorldToCell(transform.position) != targetCell)
            {

                
                var currentPosition = targetCell - tileMap.WorldToCell(transform.position);

                Vector3Int nextcell;
                if (Mathf.Abs(currentPosition.x) > Mathf.Abs(currentPosition.y))
                {
                    nextcell = RelativeCell((new Vector2(currentPosition.x, 0)).normalized);
                }
                else
                {
                    nextcell = RelativeCell((new Vector2(0, currentPosition.y)).normalized);
                }

                transform.position = tileMap.CellToWorld(nextcell);
            }

            
        }

        private void Update()
        {
        
            
            if(!isMoving)
            {

                StartCoroutine(TickRoutine());

            }

            if(playerInRange)
            {
                var t = 1 - (reactionTime / maxReactionTime);
                spriteRenderer.color = Color.Lerp(Color.white, new Color(1f, 0f, 1f), t);
                if (t < 0.5f)
                    transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(1f, 1.5f, 1f), t * 2);
                else
                    transform.localScale = Vector3.Lerp(new Vector3(1f, 1.5f, 1f), new Vector3(1f, 1f, 1f), (t - 0.5f) * 2);    
                reactionTime -= Time.fixedDeltaTime;

            }

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }


    }

}

