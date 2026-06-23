using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAMJA_2D
{
    public class CAMJA_MANAGER
    {
        public Transform CameraTransform;
        public Camera CameraComponent;

        public CAMJA_MANAGER(Transform CameraTransform, Camera CameraComponent)
        {
            this.CameraTransform = CameraTransform;
            this.CameraComponent = CameraComponent;
        }

        private Vector2 GetCamSize()
        {
            float orthographicSize = CameraComponent.orthographicSize;
            float aspectRatio = CameraComponent.aspect;

            float height = orthographicSize * 2;
            float width = height * aspectRatio;

            return new Vector2(width, height);
        }

        public struct _CamSize
        {
            public float Width { get; set; }
            public float Height { get; set; }
        }

        public _CamSize CameraSize()
        {
            _CamSize camSize = new _CamSize
            {
                Width = GetCamSize().x,
                Height = GetCamSize().y
            };

            return camSize;
        }

        private Vector4 calculateCameraSidePositions()
        {
            //Right
            float RightSidePostion = CameraTransform.position.x + CameraSize().Width / 2;
            //End Right

            //Left
            float LeftSidePosition = CameraTransform.position.x - CameraSize().Width / 2;
            //End Left

            //Top
            float TopSidePosition = CameraTransform.position.y + CameraSize().Height / 2;
            //End Top

            //Bottom
            float BottomSidePosition = CameraTransform.position.y - CameraSize().Height / 2;
            //End Bottom

            return new Vector4(RightSidePostion, LeftSidePosition, TopSidePosition, BottomSidePosition);
        }

        public struct _CameraSidePosition
        {
            public float RightSide { get; set; }
            public float LeftSide { get; set; }
            public float TopSide { get; set; }
            public float BottomSide { get; set; }
        }

        public _CameraSidePosition CameraSidePosition()
        {
            _CameraSidePosition cameraSidePosition = new _CameraSidePosition
            {
                RightSide = calculateCameraSidePositions().x,
                LeftSide = calculateCameraSidePositions().y,
                TopSide = calculateCameraSidePositions().z,
                BottomSide = calculateCameraSidePositions().w
            };

            return cameraSidePosition;
        }

        private Vector4 CalculateDistanceFromCamCenterToSide()
        {
            //Right
            float DistanceToRightSide = CameraSidePosition().RightSide - CameraTransform.position.x;
            //End Right

            //Left
            float DistanceToLeftSide = CameraSidePosition().LeftSide - CameraTransform.position.x;
            //EndLEft

            //Top
            float DistanceToTopSide = CameraSidePosition().TopSide - CameraTransform.position.y;
            //EndTop

            //Bottom
            float DistanceToBottomSide = CameraSidePosition().BottomSide - CameraTransform.position.y;
            //EndBottom
            return new Vector4(DistanceToRightSide, DistanceToLeftSide, DistanceToTopSide, DistanceToBottomSide);
        }

        public struct _CameraCenterToSide
        {
            public float ToRightSide { get; set; }
            public float ToLeftSide { get; set; }
            public float ToTopSide { get; set; }
            public float ToBottomSide { get; set; }
        }

        public _CameraCenterToSide CameraCenterToSide()
        {
            _CameraCenterToSide cameraCenterToSide = new _CameraCenterToSide
            {
                ToRightSide = CalculateDistanceFromCamCenterToSide().x,
                ToLeftSide = CalculateDistanceFromCamCenterToSide().x,
                ToTopSide = CalculateDistanceFromCamCenterToSide().z,
                ToBottomSide = CalculateDistanceFromCamCenterToSide().w
            };

            return cameraCenterToSide;
        }

        public float distanceForTarget(Transform target)
        {
            return target.transform.localPosition.x - CameraTransform.position.x;
        }
    }
}


