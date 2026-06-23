using System.Collections.Generic;
using UnityEngine;

namespace CAMJA_2D
{
    public class CAMJA_MultiPlayer : CAMJA_MANAGER
    {
        public List<Transform> Targets = new List<Transform>();

        public float MaxSize;
        public float MinSize;

        public float MaxDistance;
        public float MinDistance;

        public CAMJA_MultiPlayer
            (
                Transform CameraTransform, Camera CameraComponent, List<Transform> Targets, float MaxSize, float MinSize,
                float MaxDistance, float MinDistance
            ) 
            : 
            base(CameraTransform, CameraComponent)
        {
            this.Targets = Targets;

            this.MaxDistance = MaxDistance;
            this.MinDistance = MinDistance;

            this.MaxSize = MaxSize;
            this.MinSize = MinSize;
        }

        public void Control()
        {
            SetValues();
        }

        private void SetValues()
        {
            if(GetDistance() >= MaxDistance)
            {
                CameraTransform.position = CameraTransform.position;
            }
            else
            {
                CameraTransform.position = GetCenterPosition();
            }

            CameraComponent.orthographicSize = _Size();
        }

        private Vector3 GetCenterPosition()
        {
            Vector2 sum = new Vector3(0, 0);

            foreach (Transform Target in Targets)
            {
                sum = new Vector2(sum.x += Target.position.x, sum.y += Target.position.y);
            }

            Vector2 Division = sum / Targets.Count;

            Vector3 EndPosition = new Vector3(Division.x, Division.y, CameraTransform.position.z);

            return EndPosition;
        }

        private float GetDistance()
        {
            float DistanceCalcX = Targets[0].position.x - GetCenterPosition().x;
            float DistanceCalcY = Targets[0].position.y - GetCenterPosition().y;

            float DistanceAbsX = Mathf.Abs(DistanceCalcX);
            float DistanceAbsY = Mathf.Abs(DistanceCalcY);

            float Distance = Mathf.Sqrt(Mathf.Pow(DistanceAbsX, 2) + Mathf.Pow(DistanceAbsY, 2));

            return Mathf.Clamp(Distance, MinDistance, MaxDistance);
        }

        private float _Size()
        {
            float Size;
            return Size = MinSize + ((GetDistance() - MinDistance) / (MaxDistance - MinDistance) * (MaxSize - MinSize)); 
        }
    }
}


