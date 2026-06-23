using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAMJA_2D
{
    public class AreaValues
    {
        public Vector2 Size;
        public Vector2 Position;

        public AreaValues(Vector2 Size, Vector2 Position)
        {
            this.Size = Size;
            this.Position = Position;
        }

        public Vector4 calculateAreaSidePositions()
        {
            //Right
            float RightSidePostion = this.Position.x + this.Size.x / 2;
            //End Right

            //Left
            float LeftSidePosition = this.Position.x - this.Size.x / 2;
            //End Left

            //Top
            float TopSidePosition = this.Position.y + this.Size.y / 2;
            //End Top

            //Bottom
            float BottomSidePosition = this.Position.y - this.Size.y / 2;
            //End Bottom

            return new Vector4(RightSidePostion, LeftSidePosition, TopSidePosition, BottomSidePosition);
        }

        public struct _AreaSidePosition
        {
            public float RightSide { get; set; }
            public float LeftSide { get; set; }
            public float TopSide { get; set; }
            public float BottomSide { get; set; }
        }

        public _AreaSidePosition AreaSidePosition()
        {
            _AreaSidePosition cameraSidePosition = new _AreaSidePosition
            {
                RightSide = calculateAreaSidePositions().x,
                LeftSide = calculateAreaSidePositions().y,
                TopSide = calculateAreaSidePositions().z,
                BottomSide = calculateAreaSidePositions().w
            };

            return cameraSidePosition;
        }
    }
}


