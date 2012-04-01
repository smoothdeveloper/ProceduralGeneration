﻿
using System.Collections.Generic;
using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data
{
    public class DataStore : IGroupDataStore, IVertexDataStore, ITextureDataStore, INormalDataStore, IFaceGroup
    {
        private Group _currentGroup;

        private readonly List<Group> _groups = new List<Group>();

        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Texture> _textures = new List<Texture>();
        private readonly List<Normal> _normals = new List<Normal>();

        public Face GetFace(int i)
        {
            return _currentGroup.GetFace(i);
        }

        public void AddFace(Face face)
        {
            PushGroupIfNeeded();

            _currentGroup.AddFace(face);
        }

        public void PushGroup(string groupName)
        {
            _currentGroup = new Group(groupName);
            _groups.Add(_currentGroup);
        }

        private void PushGroupIfNeeded()
        {
            if (_currentGroup == null)
            {
                PushGroup("default");
            }
        }

        public Vertex GetVertex(int i)
        {
            return _vertices[i - 1];
        }

        public void AddVertex(Vertex vertex)
        {
            _vertices.Add(vertex);
        }

        public Texture GetTexture(int i)
        {
            return _textures[i - 1];
        }

        public void AddTexture(Texture texture)
        {
            _textures.Add(texture);
        }

        public Normal GetNormal(int i)
        {
            return _normals[i - 1];
        }

        public void AddNormal(Normal normal)
        {
            _normals.Add(normal);
        }
    }
}