using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CAMJA_2D
{
    #region EDITOR
#if UNITY_EDITOR

    [CustomEditor(typeof(CAM))]
    public class InspectorGUI : Editor
    {
        private SerializedProperty CAM_MODES;

        //Variables
        private SerializedProperty CameraTransform;
        private SerializedProperty CameraComponent;

        //FOLLOW TARGET
        private SerializedProperty MoveInX;
        private SerializedProperty MoveInY;

        private SerializedProperty SmoothMoviment;

        private SerializedProperty SpeedX;
        private SerializedProperty SpeedY;

        private SerializedProperty Target;
        private SerializedProperty CamPosition;

        private SerializedProperty MoveTowards;
        private SerializedProperty Lerp;

        private SerializedProperty UsingRectangle;

        private SerializedProperty TargetLayer_int;
        private SerializedProperty TargetLayer_String;

        private SerializedProperty Size;
        private SerializedProperty Position;
        private SerializedProperty DistanceMinForStopTheMoveX;
        private SerializedProperty DistanceMinForStopTheMoveY;

        private SerializedProperty LevelArea;
        private SerializedProperty AreaSize;
        private SerializedProperty AreaPosition;

        //ROOMS
        private SerializedProperty Speed;
        private SerializedProperty pointsToRooms;

        //MULTIPLAYER
        private SerializedProperty Targets;

        private SerializedProperty MaxSize;
        private SerializedProperty MinSize;

        private SerializedProperty MaxDistance;
        private SerializedProperty MinDistance;

        private void OnEnable()
        {
            CAM_MODES = serializedObject.FindProperty("CAM_MODES");

            CameraTransform = serializedObject.FindProperty("CameraTransform");
            CameraComponent = serializedObject.FindProperty("CameraComponent");

            MoveInX = serializedObject.FindProperty("MoveInX");
            MoveInY = serializedObject.FindProperty("MoveInY");

            SmoothMoviment = serializedObject.FindProperty("SmoothMoviment");

            SpeedX = serializedObject.FindProperty("SpeedX");
            SpeedY = serializedObject.FindProperty("SpeedY");

            Target = serializedObject.FindProperty("Target");

            CamPosition = serializedObject.FindProperty("CamPosition");


            TargetLayer_int = serializedObject.FindProperty("TargetLayer_int");
            TargetLayer_String = serializedObject.FindProperty("TargetLayer_String");

            MoveTowards = serializedObject.FindProperty("MoveTowards");
            Lerp = serializedObject.FindProperty("Lerp");

            #region FollowTargetConfig
            UsingRectangle = serializedObject.FindProperty("UsingRectangle");
            Size = serializedObject.FindProperty("Size");
            Position = serializedObject.FindProperty("Position");
            DistanceMinForStopTheMoveX = serializedObject.FindProperty("DistanceMinForStopTheMoveX");
            DistanceMinForStopTheMoveY = serializedObject.FindProperty("DistanceMinForStopTheMoveY");

            LevelArea = serializedObject.FindProperty("LevelArea");
            AreaSize = serializedObject.FindProperty("AreaSize");
            AreaPosition = serializedObject.FindProperty("AreaPosition");
            #endregion

            #region ROOMS
            Speed = serializedObject.FindProperty("Speed");
            pointsToRooms = serializedObject.FindProperty("pointsToRooms");
            #endregion

            #region Multiplayer
            Targets = serializedObject.FindProperty("Targets");

            MaxSize = serializedObject.FindProperty("MaxSize");
            MinSize = serializedObject.FindProperty("MinSize");

            MaxDistance = serializedObject.FindProperty("MaxDistance");
            MinDistance = serializedObject.FindProperty("MinDistance");
            #endregion
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CAM Modes = (CAM)target;

            EditorGUILayout.LabelField("Required Components", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(CameraTransform);
            EditorGUILayout.PropertyField(CameraComponent);

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Camera Modes", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(CAM_MODES);

            EditorGUILayout.Space(10);

            if (Modes.CAM_MODES == CAM.Modes.FollowTarget)
            {
                EditorGUILayout.LabelField("Follow Target", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(Target);

                EditorGUILayout.PropertyField(CamPosition);

                EditorGUILayout.Space(10);

                if (Modes.SmoothMoviment == false)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField("Move in X", GUILayout.Width(60));
                    MoveInX.boolValue = EditorGUILayout.Toggle(MoveInX.boolValue, GUILayout.Width(20));

                    EditorGUILayout.LabelField("Move in Y", GUILayout.Width(60));
                    MoveInY.boolValue = EditorGUILayout.Toggle(MoveInY.boolValue, GUILayout.Width(20));

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space(10);
                }

                EditorGUILayout.PropertyField(SmoothMoviment);

                EditorGUILayout.Space(10);

                if (Modes.SmoothMoviment == true)
                {
                    EditorGUILayout.PrefixLabel("Speed Axis");

                    EditorGUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField("X", GUILayout.Width(10));
                    SpeedX.floatValue = EditorGUILayout.FloatField(SpeedX.floatValue, GUILayout.Width(100));

                    EditorGUILayout.LabelField("Y", GUILayout.Width(10));
                    SpeedY.floatValue = EditorGUILayout.FloatField(SpeedY.floatValue, GUILayout.Width(100));

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space(10);

                    EditorGUILayout.PropertyField(Target);

                    EditorGUILayout.Space(10);

                    EditorGUILayout.Space(10);

                    EditorGUILayout.PropertyField(TargetLayer_int);
                    EditorGUILayout.PropertyField(TargetLayer_String);

                    EditorGUILayout.Space(10);

                    EditorGUILayout.PropertyField(UsingRectangle);

                    if (Modes.UsingRectangle == true)
                    {
                        EditorGUILayout.PropertyField(Size);
                        EditorGUILayout.PropertyField(Position);

                        EditorGUILayout.Space(10);

                        EditorGUILayout.LabelField("Distance Min For Stop The Move");

                        EditorGUILayout.Space(5);

                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("X", GUILayout.Width(10));
                        DistanceMinForStopTheMoveX.floatValue = EditorGUILayout.FloatField(DistanceMinForStopTheMoveX.floatValue, GUILayout.Width(100));

                        EditorGUILayout.LabelField("Y", GUILayout.Width(10));
                        DistanceMinForStopTheMoveY.floatValue = EditorGUILayout.FloatField(DistanceMinForStopTheMoveY.floatValue, GUILayout.Width(100));

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space(10);

                    EditorGUILayout.PropertyField(MoveTowards);
                    EditorGUILayout.PropertyField(Lerp);

                    if (Modes.MoveTowards) { Modes.Lerp = false; }
                    if (Modes.Lerp) { Modes.MoveTowards = false; }
                }

                EditorGUILayout.PropertyField(LevelArea);

                EditorGUILayout.Space(10);

                if(Modes.LevelArea == true)
                {
                    EditorGUILayout.PropertyField(AreaSize);
                    EditorGUILayout.PropertyField(AreaPosition);
                }
            }

            if (Modes.CAM_MODES == CAM.Modes.ROOMS)
            {
                EditorGUILayout.PropertyField(Speed); 

                EditorGUILayout.Space(10);

                EditorGUILayout.PropertyField(MoveTowards);
                EditorGUILayout.PropertyField(Lerp);

                EditorGUILayout.Space(10);

                EditorGUILayout.PropertyField(TargetLayer_int);
                EditorGUILayout.PropertyField(TargetLayer_String);

                EditorGUILayout.Space(10);

                //EditorGUILayout.PropertyField(pointsToRooms);

                if (GUILayout.Button("Add Point"))
                {
                    Modes.AddPoint(Modes.InstantiatePoint());
                }

                if (GUILayout.Button("Remove last point") && Modes.pointsToRooms.Count > 0)
                {
                    Vector3 RemovePoint = Modes.pointsToRooms[Modes.pointsToRooms.Count - 1];
                    Modes.RemovePoint(RemovePoint);
                }
            }

            if(Modes.CAM_MODES == CAM.Modes.Multiplayer)
            {
                EditorGUILayout.PropertyField(Targets);

                EditorGUILayout.Space(10);

                EditorGUILayout.LabelField("Size", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Max", GUILayout.Width(40));
                MaxSize.floatValue = EditorGUILayout.FloatField(MaxSize.floatValue, GUILayout.Width(70));

                EditorGUILayout.LabelField("Min", GUILayout.Width(40));
                MinSize.floatValue = EditorGUILayout.FloatField(MinSize.floatValue, GUILayout.Width(70));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(10);

                EditorGUILayout.LabelField("Distance", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Max", GUILayout.Width(40));
                MaxDistance.floatValue = EditorGUILayout.FloatField(MaxDistance.floatValue, GUILayout.Width(70));

                EditorGUILayout.LabelField("Min", GUILayout.Width(40));
                MinDistance.floatValue = EditorGUILayout.FloatField(MinDistance.floatValue, GUILayout.Width(70));

                EditorGUILayout.EndHorizontal();
            }

            serializedObject.ApplyModifiedProperties();
        }

        void OnSceneGUI()
        {
            CAM cam = (CAM)target;

            Tool currentTool = Tools.current;

            if(cam.CAM_MODES == CAM.Modes.FollowTarget && cam.LevelArea == true)
            {
                Vector3 VectorPosition = (Vector2)cam.AreaPosition;

                switch (currentTool)
                {
                    case Tool.Move:

                        EditorGUI.BeginChangeCheck();

                        Vector3 newPosition = Handles.DoPositionHandle(VectorPosition, Quaternion.identity);

                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(cam, "Move point");
                            cam.AreaPosition = newPosition;
                            EditorUtility.SetDirty(cam);
                        }

                        break;

                    case Tool.Scale:

                        Vector3 VectorScale = (Vector2)cam.AreaSize;

                        EditorGUI.BeginChangeCheck();

                        Vector3 newSizeHandle = Handles.DoScaleHandle(VectorScale, VectorPosition, Quaternion.identity, HandleUtility.GetHandleSize(VectorPosition));

                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(cam, "Scaled point");
                            cam.AreaSize = newSizeHandle;
                            EditorUtility.SetDirty(cam);
                        }

                        break;

                    default:
                        break;
                }
            }

            if(cam.CAM_MODES == CAM.Modes.ROOMS)
            {
                for (int i = 0; i < cam.pointsToRooms.Count; i++)
                {
                    Vector3 point = cam.pointsToRooms[i];

                    EditorGUI.BeginChangeCheck();

                    Vector3 newPosition = Handles.PositionHandle(point, Quaternion.identity);

                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(cam, "Move point");
                        cam.pointsToRooms[i] = newPosition;
                        EditorUtility.SetDirty(cam);
                    }
                }
            }
        }
    }

#endif
    #endregion
}