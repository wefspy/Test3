using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFImageLoading;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreparationTraining : ContentPage
    {
        private List<Exercise> exercises;
        private int numberEx;
        private int numberApproache;
        private bool isDrawing = true;
        private int timerValue = 10;
        private int timerThreshold = 10;
        public PreparationTraining(List<Exercise> exercises, int numberEx, int numberApproache)
        {
            InitializeComponent();
            this.exercises = exercises;
            this.numberEx = numberEx;
            this.numberApproache = numberApproache;
            GifcurEx.Source = exercises[numberEx].GIF;
            ExerciseName.Text = exercises[numberEx].Name;
            StartDrawing();
        }

        private async void StartDrawing()
        {
            isDrawing = true;
            while (isDrawing)
            {
                if (timerValue == 0) SwitchOnExercise();
                Timer.InvalidateSurface();
                await Task.Delay(1000);
                timerValue--;
            }
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            var width = e.Info.Width;
            var height = e.Info.Height;
            var radius = Math.Min(width, height) / 2.0f * 0.8f;

            // Draw timer circle
            var timerPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 20,
                Color = new SKColor(27, 141, 235)
            };
            var timerRect = new SKRect(width / 2 - radius, height / 2 - radius, width / 2 + radius, height / 2 + radius);
            canvas.DrawOval(timerRect, timerPaint);

            // Draw timer value
            var valuePaint = new SKPaint
            {
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                TextSize = radius / 2,
                Color = SKColors.Black
            };
            canvas.DrawText(timerValue.ToString(), width / 2, height / 2 + valuePaint.TextSize / 3, valuePaint);

            // Draw timer contour
            var contourPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 20,
                Color = SKColors.White
            };
            var sweepAngle = (float)((timerThreshold - timerValue) % timerThreshold) / timerThreshold * 360;
            canvas.DrawArc(timerRect, -90, sweepAngle, false, contourPaint);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            isDrawing = false;
        }

        private void Add20SecToTimer(object sender, EventArgs e)
        {
            timerValue += 20;
            timerThreshold += 20;
        }

        private async void SkipExercise(object sender, EventArgs e)
        {
            numberEx++;
            if (numberEx != exercises.Count)
                await Navigation.PushAsync(new PreparationTraining(exercises, numberEx, numberApproache));
            else
                await Navigation.PushAsync(new CompletionTraining(false));
        }

        private void SkipWaiting(object sender, EventArgs e)
        {
            SwitchOnExercise();
        }

        private async void SwitchOnExercise()
        {
            await Navigation.PushAsync(new PerformingTraining(exercises, numberEx, numberApproache));
        }
    }
}