namespace Sprites.Bricks
{
    public class BrickRubber : BrickBase
    {
        protected override void Start()
        {
            base.Start();
            Resistance = 10;
        }
    }
}
