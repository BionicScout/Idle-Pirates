using UnityEngine;

[System.Serializable]
public class Resource {
    public enum Type {
        Gold,
        Ship_Build,
        Trade
    }

    [SerializeField]
    private Type type;

    [SerializeField]
    private string resourceName;

    [SerializeField]
    private int amount;

    [SerializeField]
    private int cost;

    public Resource(Type t, string name, int a, int c) {
        type = t;
        resourceName = name;
        amount = a;
        cost = c;
    }

    public string GetName() 
    {
        return resourceName;
    }

    public void AddName(string name)
    {
        resourceName = name;
    }

    public void AddAmount(int a)
    {
        amount = a;
    }

    public int GetAmount() {
        return amount;
    }

    public void AddCost(int c) 
    {
        cost = c;
    }

    public int GetCost()
    {
        return cost;
    }

    public void AddType(Type t) 
    {
        type = t;
    }

    public Type GetResourceType()
    {
        return type;
    }

    public void Add(Resource newResource) {
        amount += newResource.GetAmount();

        if(Inventory.instance.crew.Find(x => x.active).crewName == "Dave")
            amount += Mathf.CeilToInt(newResource.GetAmount() * .1f);
    }

    public void SubtractAmount(int payment)
    {
        amount -= payment;
    }


    
}
