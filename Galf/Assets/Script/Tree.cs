using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Tree : MonoBehaviour
{

    public int treeHP = 10;

    public int currentHealth;
    public bool isReserved;
    public Lumberjack reservedBy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    private void Awake()
    {
        currentHealth = treeHP;

    }
 
    public bool TryReserve(Lumberjack reserver)
    {
        if (isReserved) return false;

        isReserved = true;
        reservedBy = reserver;
        return true;
    }

    public void ReleaseTree()
    {
        isReserved = false;
        reservedBy = null;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
   
}
