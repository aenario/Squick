using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Squick
{
    class KinectInterface
    {
        public static int modeHands = 1;
        public static int modeEdge = 2;

        public static int statusNoKinect = 1;
        public static int statusNoFrame = 2;
        public static int statusOK = 3;

        public static int rightHand = 0;
        public static int leftHand = 1;

        private KinectManager _manager;
        private KinectSensor sensor;
        private int _mode;
        private int _status;

        DepthImagePoint[] handPositions;



        public KinectInterface(KinectManager manager, int mode)
        {
            _manager = manager;
            _mode = mode;
            _status = statusNoKinect;
            handPositions = new DepthImagePoint[2];
           
            _manager.SensorChanged += new KinectManager.StatusChangedEventHandler(SensorChanged);
            startSensor();
            
        }

        public void startSensor()
        {
            if (!_manager.hasSensor())
            {
                sensor = null;
                return;
            }
            sensor = _manager.getSensor();

            if (_mode == modeHands)
            {
                sensor.SkeletonStream.Enable(new TransformSmoothParameters()
                { // TO BE PLAYED WITH
                    Smoothing = 0.5f, 
                    Correction = 0.5f,
                    Prediction = 0.5f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                });
                sensor.ColorStream.Disable();
                sensor.DepthStream.Disable();
                sensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(sensor_SkeletonFrameReady);
            }
            else if (_mode == modeEdge)
            {
                sensor.SkeletonStream.Disable();
                sensor.ColorStream.Disable();
                sensor.DepthStream.Enable();
                sensor.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(sensor_DepthFrameReady);
            }
            else return;

            sensor.Start();
        }

        public void changeMode(int mode)
        {
            _mode = mode;
            sensor.Stop();
            startSensor();
        }

        void sensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            throw new NotImplementedException();
        }

        void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    int skeletonSlot = 0;
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];

                    skeletonFrame.CopySkeletonDataTo(skeletonData);
                    Skeleton playerSkeleton = (from s in skeletonData where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                    if (playerSkeleton != null)
                    {
                        handPositions[rightHand] = sensor.MapSkeletonPointToDepth(playerSkeleton.Joints[JointType.HandRight].Position, DepthImageFormat.Resolution640x480Fps30);
                        handPositions[leftHand] = sensor.MapSkeletonPointToDepth(playerSkeleton.Joints[JointType.HandLeft].Position, DepthImageFormat.Resolution640x480Fps30);
                    }
                }

            }
        }
        public void SensorChanged(Object sender, EventArgs e)
        {
            startSensor();
        }

        public DepthImagePoint[] getLatestCoordinates(){
            return handPositions;
        }

    }
}
