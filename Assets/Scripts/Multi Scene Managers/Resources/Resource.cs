using UnityEngine;

[System.Serializable]
public class Resource {
    public enum Type {
        Gold,
        Ship_Build,
        Trade
    }

    [SerializeField]
    public Type type;

    [SerializeField]
    private string resourceName;

    [SerializeField]
    public int amount;

    [SerializeField]
    private int cost;

    public Resource(Type t, string name, int a, int c) {
        type = t;
        resourceName = name;
        amount = a;
        cost = c;
    }

    public Resource(MainResources resource)
    {
        type = resource.type;
        resourceName = resource.resourceName;
        amount = Random.Range(50, 100);
        cost = resource.buyValue;
    }

    public Resource(SaveData_Resource r) {
        switch(r.type) {
            case 1:
                type = Type.Gold;
                break;

            case 2:
                type = Type.Ship_Build;
                break;

            case 3:
                type = Type.Trade;
                break;

            default:
                break;
        }

        resourceName = r.resourceName;
        amount = r.amount;
        cost = r.cost;
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

    public void AddNewResource(Resource newResource) {
        amount += newResource.GetAmount();

        //if (Inventory.instance.crew.Find(x => x.active).crewName == "Dave")
        //{
        //    amount += Mathf.CeilToInt(newResource.GetAmount() * .1f);
        //}

        
    }

    public void SubtractAmount(int payment)
    {
        amount -= payment;
    }


    
}
