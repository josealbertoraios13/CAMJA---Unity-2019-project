using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAMJA_2D
{
    public class CAMJA_RoomsTarget : CAMJA_MANAGER
    {
        public float Speed;
        public List<Vector3> pointsToRooms = new List<Vector3>();
        public int TargetLayer_int;
        public string TargetLayer_String;

        public CAMJA_RoomsTarget(Transform CameraTransform, Camera CameraComponent, float Speed, List<Vector3> PointsToRooms, int TargetLayer_int, string TargetLayer_String) : base(CameraTransform, CameraComponent)
        {
            this.Speed = Speed;
            this.pointsToRooms = PointsToRooms;

            this.TargetLayer_int = TargetLayer_int;
            this.TargetLayer_String = TargetLayer_String;
        }

        public void start()
        {
            newPosition = new Vector3(CameraTransform.localPosition.x, CameraTransform.localPosition.y, CameraTransform.localPosition.z);
        }

        private Vector3 newPosition;
        public void Control()
        {
            OverlapRactangle();
            MoveType();
        }

        void MoveType()
        {
            if (CAM.Instance.MoveTowards == true)
            {
                CameraTransform.localPosition = Vector3.MoveTowards(CameraTransform.position, newPosition, Speed * Time.deltaTime);
            }
            else if (CAM.Instance.Lerp == true)
            {
                CameraTransform.localPosition = Vector3.Lerp(CameraTransform.position, newPosition, Speed * Time.deltaTime);
            }
        }

        public void OverlapRactangle()
        {
            Vector2 Scale = new Vector2(CameraSize().Width, CameraSize().Height);
            LayerMask layer = LayerMask.GetMask(TargetLayer_String);

            foreach (Vector3 pointRoom in pointsToRooms)
            {
                Collider2D collision = Physics2D.OverlapBox(pointRoom, Scale, 0, layer);

                if (collision != null)
                {
                    if (collision.gameObject.layer == TargetLayer_int)
                    {
                        newPosition = new Vector3(pointRoom.x, pointRoom.y, CameraTransform.position.z);
                    }
                }
            }
        }
    }
}


