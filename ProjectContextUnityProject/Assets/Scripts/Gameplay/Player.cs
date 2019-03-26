using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public Tilemap slipperyTilemap;

    public bool isMoving = false;
    public bool isSliding = false;
    public bool onCooldown = false;
    public float moveTime = 0.2f;

    [SerializeField] GameObject renderParent;
    [SerializeField] Animator shoulderAnimator;

    private Vector3 startingPosition;

    Coroutine moveRoutine;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        //We do nothing if the player is still moving.
        if (isMoving || onCooldown) return; //|| onExit) return;

        //To store move directions.
        int horizontal = 0;
        int vertical = 0;
        //To get move directions
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        //We can't go in both directions at the same time
        if (horizontal != 0)
            vertical = 0;

        //If there's a direction, we are trying to move.
        if (horizontal != 0 || vertical != 0)
        {
            StartCoroutine(actionCooldown(0.1f));
            ChangeRotation(horizontal, vertical);
            Move(horizontal, vertical);
        }
    }

    public void TeleportToStart()
    {
        transform.position = startingPosition;
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        isMoving = false;
        isSliding = false;
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        if (shoulderAnimator != null)
        {
            shoulderAnimator.SetBool("IsMoving", isMoving);
            shoulderAnimator.SetBool("IsSliding", isSliding);
        }

    }

    private void ChangeRotation(int xDir, int yDir)
    {
        if (xDir < 0)
            renderParent.transform.localRotation = Quaternion.Euler(Vector3.forward * 90);
        else if (xDir > 0)
            renderParent.transform.localRotation = Quaternion.Euler(Vector3.forward * 270);
        else if (yDir < 0)
            renderParent.transform.localRotation = Quaternion.Euler(Vector3.forward * 180);
        else if (yDir > 0)
            renderParent.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void Move(int xDir, int yDir)
    {

        Vector2 startCell = transform.position;
        Vector2 targetCell = startCell + new Vector2(xDir, yDir);

        bool isOnGround = getCell(floorTilemap, startCell) != null; //If the player is on the ground
        bool hasGroundTile = getCell(floorTilemap, targetCell) != null; //If target Tile has a ground
        bool hasObstacleTile = getCell(wallTilemap, targetCell) != null; //if target Tile has an obstacle
        bool hasSlipperyTile = getCell(slipperyTilemap, targetCell) != null; //if target Tile is slippery


        //If the front tile is a walkable ground tile, the player moves here.
        if (!hasObstacleTile)
        {
            if (objectCheck(targetCell))
                StartSmoothMovementRoutine(targetCell, hasSlipperyTile);
            else
                StartBlockedMovementRoutine(targetCell);
        }

        //else
        //StartBlockedMovementRoutine(targetCell);

        //if (!isMoving)
        //tartBlockedMovementRoutine(targetCell);

    }

    void StartSmoothMovementRoutine(Vector3 end, bool isSlippery = false)
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(SmoothMovement(end, isSlippery));
    }

    void StartBlockedMovementRoutine(Vector3 end)
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(BlockedMovement(end));
    }

    private IEnumerator actionCooldown(float cooldown)
    {
        onCooldown = true;

        //float cooldown = 0.2f;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        onCooldown = false;
    }

    private IEnumerator SmoothMovement(Vector3 end, bool isSlippery = false)
    {
        //while (isMoving) yield return null;

        Vector3 _start = transform.position;

        isMoving = true;
        isSliding = isSlippery;
        UpdateAnimations();

        //Play movement sound
        //if (walkingSound != null)
        //{
        //    walkingSound.loop = true;
        //    walkingSound.Play();
        //}

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        //if (walkingSound != null)
        //    walkingSound.loop = false;

        isMoving = false;
        isSliding = false;
        UpdateAnimations();

        if (isSlippery)
        {
            Vector3 _newPos = end - _start;
            Move((int)_newPos.x, (int)_newPos.y);
        }
    }

    //Blocked animation
    private IEnumerator BlockedMovement(Vector3 end)
    {
        //while (isMoving) yield return null;

        isMoving = true;
        UpdateAnimations();

        //if (AudioManager.getInstance() != null)
        //    AudioManager.getInstance().Find("blocked").source.Play();

        Vector3 originalPos = transform.position;

        end = transform.position + ((end - transform.position) / 3);
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = (1 / (moveTime * 2));

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, originalPos, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;

            yield return null;
        }

        //The lever disable the sound so its doesn't overlap with this one, so it blocked has been muted, we restore it.
        //if (AudioManager.getInstance() != null && AudioManager.getInstance().Find("blocked").source.mute)
        //{
        //    AudioManager.getInstance().Find("blocked").source.Stop();
        //    AudioManager.getInstance().Find("blocked").source.mute = false;
        //}
        isMoving = false;
        UpdateAnimations();
    }

    private bool objectCheck(Vector2 targetCell)
    {
        Collider2D coll = whatsThere(targetCell);

        //No obstacle, we can walk there
        if (coll == null)
            return true;

        //if there's a levered door in front of the character.
        if (coll.tag == "LeveredDoor")
        {
            Debug.Log("LeveredDoor detected!");
            LeveredDoor door = coll.gameObject.GetComponent<LeveredDoor>();
            //If the door is open
            if (door.isOpen)
                return true;
            //If the door is close.
            else
                return false;
        }
        else if (coll.tag == "Lever")
        {

            //Click sound !
            //if (AudioManager.getInstance() != null)
            //{
            //    AudioManager.getInstance().Find("leverClick").source.Play();
            //    AudioManager.getInstance().Find("blocked").source.mute = true;
            //}
            Lever lever = coll.gameObject.GetComponent<Lever>();
            lever.operate();
            //We operate the lever, but can't move there, so we return false;
            return false;
        }
        else if (coll.tag == "WallButton")
        {
            Vector2 _dir = targetCell - new Vector2(transform.position.x, transform.position.y);
            coll.GetComponent<WallButton>().InteractWithButton(_dir);
            return false;
        }
        else if (coll.tag == "Exit")
        {
            LevelManager.Instance.OnExit();
            return false;
        }
        else
            return true;

    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    public Collider2D whatsThere(Vector2 targetPos)
    {
        RaycastHit2D hit;
        hit = Physics2D.Linecast(targetPos, targetPos);
        return hit.collider;
    }

}
