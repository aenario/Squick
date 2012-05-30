using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;

namespace Squick.Control
{
    public class KinectInterface
    {
        public const int MODE_HANDS = 1;
        public const int MODE_EDGE = 2;

        public const int STATUS_NO_KINECT = 1;
        public const int STATUS_NO_FRAME = 2;
        public const int STATUS_OK = 3;

        public const int RIGHT_HAND = 0;
        public const int LEFT_HAND = 1;


        private const int HAND_CURSOR_WIDTH = 50;

        private static KinectManager _manager;
        private static KinectSensor _sensor;
        private static int _mode;
        private static int _status;
        public int Status
        {
            get { return _status; }
        }

        private static Vector2[] handPositions;



        public KinectInterface(KinectManager manager, int mode)
        {
            _manager = manager;
            _mode = mode;
            _status = STATUS_NO_KINECT;
            handPositions = new Vector2[2]{
                new Vector2(0, 0),
                new Vector2(0, 0)
            };

            _manager.SensorChanged += new KinectManager.StatusChangedEventHandler(SensorChanged);
            StartSensor();
            
        }

        public void StartSensor()
        {
            if (!_manager.hasSensor())
            {
                _sensor = null;
                return;
            }
            _sensor = _manager.getSensor();

            if (_mode == MODE_HANDS)
            {
                _sensor.SkeletonStream.Enable(new TransformSmoothParameters()
                { // TO BE PLAYED WITH
                    Smoothing = 0.5f, 
                    Correction = 0.5f,
                    Prediction = 0.5f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                });
                _sensor.ColorStream.Disable();
                _sensor.DepthStream.Disable();
                _sensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(Sensor_SkeletonFrameReady);
            }
            else if (_mode == MODE_EDGE)
            {
                _sensor.SkeletonStream.Disable();
                _sensor.ColorStream.Disable();
                _sensor.DepthStream.Enable();
                _sensor.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(Sensor_DepthFrameReady);
            }
            else return;

            _sensor.Start();
        }

        public void ChangeMode(int mode)
        {
            _mode = mode;
            _sensor.Stop();
            StartSensor();
        }

        void Sensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            throw new NotImplementedException();
        }

        void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];

                    skeletonFrame.CopySkeletonDataTo(skeletonData);
                    Skeleton playerSkeleton = (from s in skeletonData where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                    if (playerSkeleton != null)
                    {
                        var pos = playerSkeleton.Joints[JointType.HandRight].Position;
                        var di = _sensor.MapSkeletonPointToDepth(pos, DepthImageFormat.Resolution640x480Fps30);
                        handPositions[RIGHT_HAND].X = 800*di.X/640;
                        handPositions[RIGHT_HAND].Y = 600*di.Y/480;
                        pos = playerSkeleton.Joints[JointType.HandLeft].Position;
                        di = _sensor.MapSkeletonPointToDepth(pos, DepthImageFormat.Resolution640x480Fps30);
                        handPositions[LEFT_HAND].X = 800 * di.X / 640;
                        handPositions[LEFT_HAND].Y = 600 * di.Y / 480;
                    }
                }

            }
        }
        public void SensorChanged(Object sender, EventArgs e)
        {
            StartSensor();
        }

        public Vector2[] GetLatestCoordinates(){
            return handPositions;
        }


        public Rectangle[] GetHandCursorsBoundingBoxes()
        {
            Rectangle[] cursors = new Rectangle[2];
            cursors[LEFT_HAND] = new Rectangle((int) (handPositions[LEFT_HAND].X - HAND_CURSOR_WIDTH / 2), 
                                               (int) (handPositions[LEFT_HAND].Y - HAND_CURSOR_WIDTH / 2),
                                               HAND_CURSOR_WIDTH,HAND_CURSOR_WIDTH);
            cursors[RIGHT_HAND] = new Rectangle((int)(handPositions[RIGHT_HAND].X - HAND_CURSOR_WIDTH / 2),
                                                (int)(handPositions[RIGHT_HAND].Y - HAND_CURSOR_WIDTH / 2),
                                               HAND_CURSOR_WIDTH, HAND_CURSOR_WIDTH);
            return cursors;
        }

    }
}
