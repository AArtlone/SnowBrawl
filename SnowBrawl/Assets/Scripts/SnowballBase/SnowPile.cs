public class SnowPile : PickableBase
{
    private void Start()
    {
        // You can always pick up from the MainBase
        CanPickUp = true;
    }
}
