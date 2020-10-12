public class Inventory
{
    public readonly int maximumSnowballs = 2;

    private int snowballs;

    private Player player;

    public int Snowballs
    {
        get
        {
            return snowballs;
        }
        set
        {
            snowballs = value;
            player.RaiseSnowballChangedEvent();
        }
    }

    public Inventory(Player player)
    {
        this.player = player;
    }

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
