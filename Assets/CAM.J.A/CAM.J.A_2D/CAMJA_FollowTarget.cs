using System;
using System.Collections;
using UnityEngine;

namespace CAMJA_2D
{
    public class CAMJA_FollowTarget : CAMJA_MANAGER
    {
        public bool MoveInX;
        public bool MoveInY;

        public bool SmoothMoviment;

        public float SpeedX;
        public float SpeedY;

        public Transform Target;
        public Vector2 CamPosition;

        public bool UsingRectangle;

        public int TargetLayer_int;
        public string TargetLayer_String;

        public Vector2 Size;
        public Vector2 Position;

        public float TolarenceX;
        public float TolarenceY;

        public bool LevelArea;
        public Vector2 AreaSize;
        public Vector2 AreaPosition;

        public CAMJA_FollowTarget
            (
                Transform CameraTransform, Camera CameraComponent,
                bool MoveInX, bool MoveInY,
                bool SmoothMoviment,
                float SpeedX, float SpeedY,
                Transform Target,
                Vector2 CamPosition,
                bool UsingRectangle,
                int TargetLayer_int, string TargetLayer_String,
                Vector2 Size, Vector2 Position,
                float TolarenceX, float TolarenceY, 
                bool LevelArea, Vector2 AreaSize, Vector2 AreaPosition
            )
            :
            base(CameraTransform, CameraComponent)

        {
            this.MoveInX = MoveInX;
            this.MoveInY = MoveInY;

            this.SmoothMoviment = SmoothMoviment;

            this.SpeedX = SpeedX;
            this.SpeedY = SpeedY;

            this.Target = Target;
            this.CamPosition = CamPosition;

            this.UsingRectangle = UsingRectangle;
            this.TargetLayer_int = TargetLayer_int;
            this.TargetLayer_String = TargetLayer_String;
            this.Size = Size;
            this.Position = Position;

            this.TolarenceX = TolarenceX;
            this.TolarenceY = TolarenceY;

            this.LevelArea = LevelArea;
            this.AreaSize = AreaSize;
            this.AreaPosition = AreaPosition;
        }

        AreaValues areaValue;
        public void start()
        {
            positionX = CameraTransform.position.x;
            positionY = CameraTransform.position.y;
        }

        Vector3 newPosition;

        float TargetDistanceLeft;
        float TargetDistanceRight;
        float TargetDistanceBottom;
        float TargetDistanceTop;
        public void Control()
        {
            // newPosition = new Vector3(this.Target.position.x + this.CamPosition.x, this.Target.position.y + this.CamPosition.y, this.CameraTransform.position.z);
            newPosition = new Vector3(this.Target.position.x + this.CamPosition.x, this.Target.position.y + this.CamPosition.y, this.CameraTransform.position.z);

            if (this.SmoothMoviment) { CameraTransform.position = new Vector3(positionX, positionY, newPosition.z); }

            if (this.UsingRectangle)
            {
                OnArea();
            }
            else
            {
                OutArea();
            }

            if(this.LevelArea)
            {
                areaValue = new AreaValues(AreaSize, AreaPosition);

                CameraSize();
                CameraSidePosition();
                areaValue.AreaSidePosition();
                Distance();

                TargetDistanceLeft = Distance().TargetRelativeDistanceForAreLeft;
                TargetDistanceRight = Distance().TargetRelativeDistanceForAreRight;
                TargetDistanceBottom = Distance().TargetRelativeDistanceForAreBottom;
                TargetDistanceTop = Distance().TargetRelativeDistanceForAreTop;
            }
        }

        void OutArea()
        {
            MoveTypeX();
            MoveTypeY();
        }

        private bool InArea;
        private bool GoToNewPositionX;
        private bool GoToNewPositionY;
        void OnArea()
        {
            OverLapRectangle();

            float Xa = this.CameraTransform.position.x;
            float Ya = this.CameraTransform.position.y;

            float Xb = this.Target.position.x;
            float Yb = this.Target.position.y;

            if (!InArea)
            {
                GoToNewPositionX = true;
                GoToNewPositionY = true;
            }

            if (InArea && AlmostEqualArea(Xa, Xb, TolarenceX))
            {
                GoToNewPositionX = false;
            }

            if (InArea && AlmostEqualArea(Ya, Yb, TolarenceY))
            {
                GoToNewPositionY = false;
            }


            if (GoToNewPositionX)
            {
                MoveTypeX();
            }
            else
            {
                CameraTransform.position = CameraTransform.position;
            }

            if (GoToNewPositionY)
            {
                MoveTypeY();
            }
            else
            {
                CameraTransform.position = CameraTransform.position;
            }
        }

        float positionX;
        float positionY;
        void MoveTypeX()
        {
            if (SmoothMoviment)
            {
                Move_MoveTowardsX();
                Move_LerpX();
            }
        }
        private void Move_LerpX()
        {
            if (CAM.Instance.Lerp)
            {
                if (this.SpeedX > 0)
                {
                    if (this.LevelArea)
                    {
                        if(TargetDistanceLeft > 0 && TargetDistanceRight < 0)
                        {
                            positionX = Mathf.Lerp(positionX, newPosition.x, this.SpeedX * Time.deltaTime);
                        }
                        else if(TargetDistanceLeft < 0)
                        {
                            float newPositionX = areaValue.AreaSidePosition().LeftSide + CameraCenterToSide().ToLeftSide;
                            positionX = Mathf.Lerp(positionX, newPositionX, this.SpeedX * Time.deltaTime);
                        }
                        else if(TargetDistanceRight > 0)
                        {
                            float newPositionX = areaValue.AreaSidePosition().RightSide - CameraCenterToSide().ToRightSide;
                            positionX = Mathf.Lerp(positionX, newPositionX, this.SpeedX * Time.deltaTime);
                        }
                    }
                    else
                    {
                        positionX = Mathf.Lerp(positionX, newPosition.x, this.SpeedX * Time.deltaTime);
                    }
                }
                else
                {
                    positionX = CameraTransform.position.x;
                }
            }
        }
        private void Move_MoveTowardsX()
        {
            if (CAM.Instance.MoveTowards)
            {
                if (this.SpeedX > 0)
                {
                    if (this.LevelArea)
                    {
                        if (TargetDistanceLeft > 0 && TargetDistanceRight < 0)
                        {
                            positionX = Mathf.MoveTowards(positionX, newPosition.x, this.SpeedX * Time.deltaTime);
                        }
                        else if (TargetDistanceLeft < 0)
                        {
                            float newPositionX = areaValue.AreaSidePosition().LeftSide + CameraCenterToSide().ToLeftSide;
                            positionX = Mathf.MoveTowards(positionX, newPositionX, this.SpeedX * Time.deltaTime);
                        }
                        else if (TargetDistanceRight > 0)
                        {
                            float newPositionX = areaValue.AreaSidePosition().RightSide - CameraCenterToSide().ToRightSide;
                            positionX = Mathf.MoveTowards(positionX, newPositionX, this.SpeedX * Time.deltaTime);
                        }
                    }
                    else
                    {
                        positionX = Mathf.MoveTowards(positionX, newPosition.x, this.SpeedX * Time.deltaTime);
                    }
                }
                else
                {
                    positionX = this.CameraTransform.position.x;
                }
            }
        }
        void MoveTypeY()
        {
            if (this.SmoothMoviment)
            {
                Move_MoveTowardsY();
                Move_LerpY();
            }
        }
        private void Move_LerpY()
        {
            if (CAM.Instance.Lerp)
            {
                if (this.SpeedY > 0)
                {
                    if (this.LevelArea == true)
                    {
                        if (TargetDistanceBottom > 0 && TargetDistanceTop < 0)
                        {
                            positionY = Mathf.Lerp(positionY, newPosition.y, this.SpeedY * Time.deltaTime);
                        }
                        else if (TargetDistanceBottom < 0)
                        {
                            float newPositionY = areaValue.AreaSidePosition().BottomSide - CameraCenterToSide().ToBottomSide;
                            positionY = Mathf.Lerp(positionY, newPositionY, this.SpeedY * Time.deltaTime);
                        }
                        else if (TargetDistanceTop > 0)
                        {
                            float newPositionY = areaValue.AreaSidePosition().TopSide - CameraCenterToSide().ToTopSide;
                            positionY = Mathf.Lerp(positionY, newPositionY, this.SpeedY * Time.deltaTime);
                        }
                    }
                    else
                    {
                        positionY = Mathf.Lerp(positionY, newPosition.y, this.SpeedY * Time.deltaTime);
                    }
                }
                else
                {
                    positionY = this.CameraTransform.position.y;
                }
            }
        }
        private void Move_MoveTowardsY()
        {
            if (CAM.Instance.MoveTowards)
            {
                if (this.SpeedY > 0)
                {
                    if (this.LevelArea)
                    {
                        if (TargetDistanceBottom > 0 && TargetDistanceTop < 0)
                        {
                            positionY = Mathf.MoveTowards(positionY, newPosition.y, this.SpeedY * Time.deltaTime);
                        }
                        else if (TargetDistanceBottom < 0)
                        {
                            float newPositionY = areaValue.AreaSidePosition().BottomSide - CameraCenterToSide().ToBottomSide;
                            positionY = Mathf.MoveTowards(positionY, newPositionY, this.SpeedY * Time.deltaTime);
                        }
                        else if (TargetDistanceTop > 0)
                        {
                            float newPositionY = areaValue.AreaSidePosition().TopSide - CameraCenterToSide().ToTopSide;
                            positionY = Mathf.MoveTowards(positionY, newPositionY, this.SpeedY * Time.deltaTime);
                        }
                    }
                    else
                    {
                        positionY = Mathf.MoveTowards(positionY, newPosition.y, this.SpeedY * Time.deltaTime);
                    }
                }
                else
                {
                    positionY = this.CameraTransform.position.y;
                }
            }
        }

        Vector3 RealyPosition;
        public void MoveWithOutSmoothX()
        {
            if (!SmoothMoviment)
            {
                newPosition = new Vector3(this.Target.position.x + this.CamPosition.x, this.Target.position.y + this.CamPosition.y, this.CameraTransform.position.z);

                if (this.MoveInX)
                {
                    if (this.LevelArea)
                    {
                        if (TargetDistanceLeft > 0 && TargetDistanceRight < 0)
                        {
                            this.CameraTransform.position = new Vector3(newPosition.x, this.CameraTransform.position.y, this.CameraTransform.position.z);
                        }
                        else if (TargetDistanceLeft < 0)
                        {
                            this.CameraTransform.position = new Vector3
                                (areaValue.AreaSidePosition().LeftSide + CameraCenterToSide().ToLeftSide, this.CameraTransform.position.y, this.CameraTransform.position.z);
                        }
                        else if (TargetDistanceRight > 0)
                        {
                            this.CameraTransform.position = new Vector3
                                (areaValue.AreaSidePosition().RightSide - CameraCenterToSide().ToRightSide, this.CameraTransform.position.y, this.CameraTransform.position.z); ;
                        }
                    }
                    else
                    {
                        this.CameraTransform.position = new Vector3(newPosition.x, this.CameraTransform.position.y, this.CameraTransform.position.z);
                    }
                }              
            }
        }
        public void MoveWithOutSmoothY()
        {
            if (!SmoothMoviment)
            {
                newPosition = new Vector3(this.Target.position.x + this.CamPosition.x, this.Target.position.y + this.CamPosition.y, this.CameraTransform.position.z);

                if (this.MoveInY)
                {
                    if (this.LevelArea)
                    {
                        if (TargetDistanceBottom > 0 && TargetDistanceTop < 0)
                        {
                            this.CameraTransform.position = new Vector3(this.CameraTransform.position.x, newPosition.y, this.CameraTransform.position.z);
                        }
                        else if (TargetDistanceBottom < 0)
                        {
                            this.CameraTransform.position = new Vector3
                                (this.CameraTransform.position.x, areaValue.AreaSidePosition().BottomSide - CameraCenterToSide().ToBottomSide, this.CameraTransform.position.z);
                        }
                        else if (TargetDistanceTop > 0)
                        {
                            this.CameraTransform.position = new Vector3
                                (this.CameraTransform.position.x, areaValue.AreaSidePosition().TopSide - CameraCenterToSide().ToTopSide, this.CameraTransform.position.z);
                        }
                    }
                    else
                    {
                        this.CameraTransform.position = new Vector3(this.CameraTransform.position.x, newPosition.y, this.CameraTransform.position.z);
                    }
                }
            }
        }

        public void OverLapRectangle()
        {
            Vector2 Origim = new Vector2(this.CameraTransform.position.x, this.CameraTransform.position.y);

            Vector2 LocalPosition = new Vector2(Origim.x + this.Position.x, Origim.y + this.Position.y);

            LayerMask targetLayer = LayerMask.GetMask(TargetLayer_String);

            InArea = Physics2D.OverlapBox(LocalPosition, Size, 0, targetLayer);
        }
        private bool AlmostEqualArea(float AxisA, float AxisB, float tolarence)
        {
            float distance = AxisA - AxisB;

            return Mathf.Abs(distance) < tolarence;
        }

        private struct CalcDistances
        {
            public float TargetRelativeDistanceForAreRight;
            public float TargetRelativeDistanceForAreLeft;
            public float TargetRelativeDistanceForAreTop;
            public float TargetRelativeDistanceForAreBottom;
        }

        private CalcDistances Distance()
        {
            CalcDistances calcDistances = new CalcDistances
            {
                TargetRelativeDistanceForAreRight = (((areaValue.AreaSidePosition().RightSide - Target.localPosition.x) - CameraCenterToSide().ToRightSide) - CamPosition.x)* -1f,
                TargetRelativeDistanceForAreLeft = (((areaValue.AreaSidePosition().LeftSide - Target.localPosition.x) + CameraCenterToSide().ToLeftSide) - CamPosition.x) * -1f,
                TargetRelativeDistanceForAreTop = (((areaValue.AreaSidePosition().TopSide - Target.localPosition.y) - CameraCenterToSide().ToTopSide) - CamPosition.y) * -1,
                TargetRelativeDistanceForAreBottom = (((areaValue.AreaSidePosition().BottomSide - Target.localPosition.y) - CameraCenterToSide().ToBottomSide) - CamPosition.y) * -1f
            };

            return calcDistances;
        }
    }
}


