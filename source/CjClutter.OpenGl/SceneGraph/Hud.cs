﻿using System;
using System.ComponentModel;
using CjClutter.OpenGl.Noise;
using Gwen;
using Gwen.Control;
using Gwen.Skin;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Base = Gwen.Control.Base;

namespace CjClutter.OpenGl.SceneGraph
{
    public class GeneratoionSettingsControl
    {
        private readonly Base _parent;

        private int _octaves;
        private double _amplitude;
        private double _frequency;
        private TextBox _octavesTextBox;
        private TextBox _amplitudeTextBox;
        private TextBox _frequenceyTextBox;


        public GeneratoionSettingsControl(Base parent)
        {
            _parent = parent;
            _octavesTextBox = CreateTextBoxForPropety(() => _octaves, x => _octaves = x);
            _amplitudeTextBox = CreateTextBoxForPropety(() => _amplitude, x => _amplitude = x);
            _frequenceyTextBox = CreateTextBoxForPropety(() => _frequency, x => _frequency = x);
        }

        private TextBox CreateTextBoxForPropety<T>(Func<T> getter, Action<T> setter)
        {
            var textBox = new TextBox(_parent)
                {
                    Text = getter().ToString(),
                    Dock = Pos.Top,
                };

            textBox.TextChanged += x =>
                {
                    var text = textBox.Text;
                    var converter = TypeDescriptor.GetConverter(typeof (T));

                    try
                    {
                        var newValue = (T)converter.ConvertFromInvariantString(text);
                        setter(newValue);
                    }
                    catch (Exception e)
                    {
                        
                    }
                    
                };
            return textBox;
        }

        public FractalBrownianMotionSettings GetSettings()
        {
            return new FractalBrownianMotionSettings(_octaves, _amplitude, _frequency);
        }
    }

    public class Hud
    {
        private readonly Label _frameTimeLabel;
        private readonly Label _fpsLabel;
        private readonly Canvas _canvas;
        private readonly Gwen.Renderer.OpenTK _renderer;
        private readonly TexturedBase _texturedBase;
        private Matrix4 _projectionMatrix;

        public Hud(GameWindow gameWindow)
        {
            _renderer = new Gwen.Renderer.OpenTK();
            _texturedBase = new TexturedBase(_renderer, "DefaultSkin.png");
            _canvas = new Canvas(_texturedBase);

            var input = new Gwen.Input.OpenTK(gameWindow);
            input.Initialize(_canvas);

            //_canvas.ShouldDrawBackground = true;
            //_canvas.BackgroundColor = Color.FromArgb(255, 150, 170, 170);

            _fpsLabel = new Label(_canvas)
                {
                    AutoSizeToContents = true, 
                    Dock = Pos.Top
                };

            _frameTimeLabel = new Label(_canvas)
                {
                    AutoSizeToContents = true, 
                    Dock = Pos.Top
                };

            var generatoionSettingsControl = new GeneratoionSettingsControl(_canvas);

            gameWindow.Mouse.Move += (sender, args) => input.ProcessMouseMessage(args);
            gameWindow.Mouse.ButtonDown += (sender, args) => input.ProcessMouseMessage(args);
            gameWindow.Mouse.ButtonUp += (sender, args) => input.ProcessMouseMessage(args);
            gameWindow.Mouse.WheelChanged += (sender, args) => input.ProcessMouseMessage(args);

            gameWindow.Keyboard.KeyDown += (sender, args) => input.ProcessKeyDown(args);
            gameWindow.Keyboard.KeyUp += (sender, args) => input.ProcessKeyUp(args);
            gameWindow.KeyPress += (sender, args) => input.KeyPress(sender, args);
        }

        public void Resize(int width, int height)
        {
            _projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, -1, 1);
        }

        public void Update(double elapsed, double frameTime)
        {
            MaintainTextCache();

            UpdateControls(elapsed, frameTime);
        }

        private void MaintainTextCache()
        {
            if (_renderer.TextCacheSize > 1000)
            {
                _renderer.FlushTextCache();
            }
        }

        private double _deadLine = 0;
        private void UpdateControls(double elapsed, double frameTime)
        {
            if (_deadLine > elapsed)
                return;

            _fpsLabel.Text = string.Format("{0:0}fps", 1 / frameTime);
            _frameTimeLabel.Text = string.Format("{0:0}ms", frameTime * 1000);
            _deadLine = elapsed + 1;
        }

        public void Draw()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref _projectionMatrix);

            _canvas.RenderCanvas();
        }

        public void Close()
        {
            _renderer.Dispose();
            _texturedBase.Dispose();
            _canvas.Dispose();
        }
    }
}