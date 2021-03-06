using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CjClutter.OpenGl.OpenGl
{
    public class Uniform<T>
    {
        private readonly Dictionary<Type, Action<object>> _setterDictionary;
        private readonly int _location;

        private readonly Action<object> _uniformSetter;

        public Uniform(int location)
        {
            _location = location;

            _setterDictionary = new Dictionary<Type, Action<object>>
                                    {
                                        {typeof(Matrix4), SetMatrix4},
                                        {typeof(Vector3), SetVector3},
                                        {typeof(Vector4), SetVector4},
                                        {typeof(Vector2), SetVector2},
                                        {typeof(Matrix3), SetMatrix3},
                                    };

            var type = typeof (T);

            if(!_setterDictionary.ContainsKey(type))
            {
                throw new NotImplementedException("Setter is not implemented for uniform type");
            }

            _uniformSetter = _setterDictionary[type];
        }

        public void Set(T value)
        {
            var o = (object) value;
            _uniformSetter(o);
        }

        private void SetVector4(object obj)
        {
            var vector4 = (Vector4) obj;
            GL.Uniform4(_location, ref vector4);
        }

        private void SetVector3(object obj)
        {
            var vector3 = (Vector3) obj;
            GL.Uniform3(_location, ref vector3);
        }

        private void SetVector2(object obj)
        {
            var vector2 = (Vector2) obj;
            GL.Uniform2(_location, vector2);
        }

        private void SetMatrix4(object value)
        {
            var matrix = (Matrix4)value;
            GL.UniformMatrix4(_location, false, ref matrix);
        }

        private void SetMatrix3(object value)
        {
            var matrix = (Matrix3) value;
            GL.UniformMatrix3(_location, false, ref matrix);
        }
    }
}