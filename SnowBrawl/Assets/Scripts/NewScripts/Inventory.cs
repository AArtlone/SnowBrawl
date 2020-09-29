public class Inventory
{
    public int maximumSnowballs = 2;

    public int Snowballs { get; set; }

    public bool IsInventoryFull()
    {
        if (Snowballs == maximumSnowballs)
            return true;
        else
            return false;
    }

    public bool IsInventoryEmpty()
    {
        if (Snowballs == 0)
            return true;
        else
            return false;
    }
}
