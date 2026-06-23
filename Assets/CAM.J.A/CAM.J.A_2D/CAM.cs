using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAMJA_2D
{
    public class CAM : MonoBehaviour
    {
        public static CAM Instance { get; set; }

        private DrawRectangle drawRectangle;

        private CAMJA_FollowTarget followTargetScript;
        private CAMJA_FollowTarget LastfollowTargetScript;

        private CAMJA_RoomsTarget roomsTarget;

        private CAMJA_MultiPlayer multiPlayer;

        [SerializeField] private Transform CameraTransform;
        [SerializeField] private Camera CameraComponent;

        public Modes CAM_MODES;
        public enum Modes
        {
            Default, FollowTarget, ROOMS, Multiplayer
        }

        [SerializeField] private Transform Target;
        [SerializeField] private int TargetLayer_int;
        [SerializeField] private string TargetLayer_String;

        [SerializeField] public bool MoveTowards;
        [SerializeField] public bool Lerp;

        [SerializeField] private Vector2 CamPosition;

        [SerializeField] public bool MoveInX;
        [SerializeField] public bool MoveInY;

        [SerializeField] public bool SmoothMoviment;

        [SerializeField] private float SpeedX;
        [SerializeField] private float SpeedY;

        #region FollowTargetConfig
        [SerializeField] public bool UsingRectangle;
        [SerializeField] public Vector2 Size;
        [SerializeField] private Vector2 Position;
        [SerializeField] private float DistanceMinForStopTheMoveX;
        [SerializeField] private float DistanceMinForStopTheMoveY;

        [SerializeField] public bool LevelArea;
        [SerializeField] public Vector2 AreaSize;
        [SerializeField] public Vector2 AreaPosition;
        #endregion

        #region ROOMS
        [SerializeField] private float Speed;
        [SerializeField] public List<Vector3> pointsToRooms;
        #endregion

        #region Multplayer
        [SerializeField] private List<Transform> Targets = new List<Transform>();

        [SerializeField] private float MaxSize;
        [SerializeField] private float MinSize;

        [SerializeField] private float MaxDistance;
        [SerializeField] private float MinDistance;
        #endregion

        private void Awake()
        {
            followTargetScript = new CAMJA_FollowTarget
                    (
                        CameraTransform,
                        CameraComponent,
                        MoveInX, MoveInY,
                        SmoothMoviment,
                        SpeedX, SpeedY,
                        Target,
                        CamPosition,
                        UsingRectangle,
                        TargetLayer_int,
                        TargetLayer_String, Size,
                        Position,
                        DistanceMinForStopTheMoveX, DistanceMinForStopTheMoveY,
                        LevelArea, AreaSize, AreaPosition
                    );

            LastfollowTargetScript = followTargetScript;

            roomsTarget = new CAMJA_RoomsTarget(CameraTransform, CameraComponent, Speed, pointsToRooms, TargetLayer_int, TargetLayer_String);

            multiPlayer = new CAMJA_MultiPlayer(CameraTransform, CameraComponent, Targets, MaxSize, MinSize, MaxDistance, MinDistance);

            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            followTargetScript.start();
            roomsTarget.start();
        }

        private void Update()
        {
            WhyCam();
            UpdateVariablesValues();
        }

        private void LateUpdate()
        {
            if (CAM_MODES == Modes.FollowTarget)
            {
                followTargetScript.MoveWithOutSmoothX();
                followTargetScript.MoveWithOutSmoothY();
            }
        }

        private void WhyCam()
        {
            if (CAM_MODES == Modes.FollowTarget)
            {
                followTargetScript.Control();
            }

            if (CAM_MODES == Modes.ROOMS)
            {
                roomsTarget.Control();
            }

            if(CAM_MODES == Modes.Multiplayer)
            {
                multiPlayer.Control();
            }
        }

        private void UpdateVariablesValues()
        {
            if (CheckUpdateVariablesValues())
            {
                followTargetScript = new CAMJA_FollowTarget
                    (
                        CameraTransform,
                        CameraComponent,
                        MoveInX, MoveInY,
                        SmoothMoviment,
                        SpeedX, SpeedY,
                        Target,
                        CamPosition,
                        UsingRectangle,
                        TargetLayer_int,
                        TargetLayer_String, Size,
                        Position,
                        DistanceMinForStopTheMoveX, DistanceMinForStopTheMoveY,
                        LevelArea, AreaSize, AreaPosition
                    );

                LastfollowTargetScript = followTargetScript;
            }
        }

        private bool CheckUpdateVariablesValues()
        {
            return
                !Equals(CameraTransform, LastfollowTargetScript.CameraTransform) ||
                !Equals(CameraComponent, LastfollowTargetScript.CameraComponent) ||
                MoveInX != LastfollowTargetScript.MoveInX ||
                MoveInY != LastfollowTargetScript.MoveInY ||
                SmoothMoviment != LastfollowTargetScript.SmoothMoviment ||
                SpeedX != LastfollowTargetScript.SpeedX ||
                SpeedY != LastfollowTargetScript.SpeedY ||
                !Equals(Target, LastfollowTargetScript.Target) ||
                CamPosition != LastfollowTargetScript.CamPosition ||
                UsingRectangle != LastfollowTargetScript.UsingRectangle ||
                TargetLayer_int != LastfollowTargetScript.TargetLayer_int ||
                TargetLayer_String != LastfollowTargetScript.TargetLayer_String ||
                Size != LastfollowTargetScript.Size ||
                Position != LastfollowTargetScript.Position ||
                DistanceMinForStopTheMoveX != LastfollowTargetScript.TolarenceX ||
                DistanceMinForStopTheMoveY != LastfollowTargetScript.TolarenceY ||
                LevelArea != LastfollowTargetScript.LevelArea ||
                !Equals(AreaSize, LastfollowTargetScript.AreaSize) ||
                !Equals(AreaPosition, LastfollowTargetScript.AreaPosition);
        }

        #region ROOM

        public void AddPoint(Vector3 point)
        {
            pointsToRooms.Add(point);
        }

        public Vector3 InstantiatePoint()
        {
            Vector3 newPoint;

            if (pointsToRooms.Count > 0)
            {
                Vector3 lastPointPos = pointsToRooms[pointsToRooms.Count - 1];
                newPoint = new Vector3(lastPointPos.x + 20f, lastPointPos.y, lastPointPos.z);
            }
            else
            {
                newPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            return newPoint;
        }

        public void RemovePoint(Vector3 point)
        {
            pointsToRooms.Remove(point);
        }

        #endregion


#if UNITY_EDITOR
        #region OnDrawFGizmos
        private void OnDrawGizmos()
        {
            switch (CAM_MODES)
            {
                case Modes.FollowTarget:

                    if (UsingRectangle)
                    {
                        drawRectangle = new DrawRectangle();
                        Vector2 localPosition = new Vector2(CameraTransform.position.x + Position.x, CameraTransform.position.y + Position.y);
                        drawRectangle._DrawRectangle(localPosition, Size, Color.cyan);
                    }

                    if (LevelArea)
                    {
                        drawRectangle = new DrawRectangle();
                        drawRectangle._DrawRectangle(AreaPosition, AreaSize, Color.green);


                        float orthographicSize = CameraComponent.orthographicSize;
                        float aspectRatio = CameraComponent.aspect;

                        float Height = orthographicSize * 2;
                        float Width = Height * aspectRatio;

                        drawRectangle._DrawCameraArea(CameraTransform.position, Height, Width, Color.blue);
                    }

                    break;
                case Modes.ROOMS:

                    drawRectangle = new DrawRectangle();
                    foreach (var points in pointsToRooms)
                    {
                        if (points != null)
                        {
                            float orthographicSize = CameraComponent.orthographicSize;
                            float aspectRatio = CameraComponent.aspect;

                            float Height = orthographicSize * 2;
                            float Width = Height * aspectRatio;

                            drawRectangle._DrawCameraArea(points, Height, Width, Color.red);
                        }
                    }

                    for (int i = 0; i < pointsToRooms.Count; i++)
                    {
                        drawRectangle._DrawCharacters(pointsToRooms[i], i.ToString(), Color.yellow);
                    }

                    break;
                case Modes.Multiplayer:
                    break;
                default:
                    break;
            }

        }
        #endregion
#endif
    }
}
