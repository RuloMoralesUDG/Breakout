using Sprites.Bricks;
using UnityEngine;

namespace Assets.Scripts.Sprites.Bricks
{
    public class BrickGlass : BrickBase
    {
        protected override void Start()
        {
            base.Start();
            Resistance = 2;
        }
    }
}
