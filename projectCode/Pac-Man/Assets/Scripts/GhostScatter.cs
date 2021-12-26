using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior // picks a random direction at each node (intersection), tries to avoid backtracking if possible
{
    private void OnDisable() // transistion to chase state
    {
        ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled && !ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -ghost.movement.direction && node.availableDirections.Count > 1) // making sure ghost doesn't backtrack and look stupid
            {
                index++;

                if (index >= node.availableDirections.Count) // if overflow, go to 0 index
                {
                    index = 0;
                }
            }

            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
