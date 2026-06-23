using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CAMJA_2D
{
    public class DrawRectangle
    {
        public void _DrawRectangle(Vector2 Origin, Vector2 Size, Color cor)
        {
            Vector2 topLeft = Origin + new Vector2(-Size.x / 2, Size.y / 2);
            Vector2 topRight = Origin + new Vector2(Size.x / 2, Size.y / 2);
            Vector2 bottomLeft = Origin + new Vector2(-Size.x / 2, -Size.y / 2);
            Vector2 bottomRight = Origin + new Vector2(Size.x / 2, -Size.y / 2);

            Gizmos.color = cor;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

        public void _DrawCameraArea(Vector2 Origin, float Height, float Width, Color color)
        {
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;

            Vector2 topLeft = Origin + new Vector2(-halfWidth, halfHeight);
            Vector2 topRight = Origin + new Vector2(halfWidth, halfHeight);
            Vector2 bottomLeft = Origin + new Vector2(-halfWidth, -halfHeight);
            Vector2 bottomRight = Origin + new Vector2(halfWidth, -halfHeight);

            Gizmos.color = color;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

        public void _DrawCharacters(Vector3 position, string characters, Color color)
        {
            Handles.color = color;
            Handles.Label(position + Vector3.up, characters);
        }
    }
}

