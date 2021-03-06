﻿using CjClutter.OpenGl.OpenGl.Diagnostics;
using OpenTK.Graphics.OpenGL;

namespace CjClutter.OpenGl.OpenGl
{
    public class OpenGlResourceFactory : IOpenGlResourceFactory
    {
        private readonly OpenGl _openGl;

        public OpenGlResourceFactory()
        {
            _openGl = new OpenGl();
        }

        public IProgram CreateProgram()
        {
            var program = new Program(_openGl);
            return new DebugProgram(program, new ConsoleLogger());
        }

        public IShader CreateShader(ShaderType shaderType)
        {
            var shader = new Shader(_openGl);
            shader.Create(shaderType);

            return new DebugShader(shader, new ConsoleLogger());
        }

        public VertexArrayObject CreateVertexArrayObject()
        {
            var vertexArrayObject = new VertexArrayObject();
            vertexArrayObject.Create();

            return vertexArrayObject;
        }

        public VertexBufferObject<T> CreateVertexBufferObject<T>(BufferTarget bufferTarget) where T : struct, IBufferDataType
        {
            var sizeInBytes = new T().SizeInBytes;
            var vertexBufferObject = new VertexBufferObject<T>(bufferTarget, sizeInBytes);

            vertexBufferObject.Generate();

            return vertexBufferObject;
        }

        public VertexBufferObject<T> CreateVertexBufferObject<T>(BufferTarget bufferTarget, int sizeOfT) where T : struct
        {
            var vertexBufferObject = new VertexBufferObject<T>(bufferTarget, sizeOfT);
            vertexBufferObject.Generate();

            return vertexBufferObject;
        }
    }
}
