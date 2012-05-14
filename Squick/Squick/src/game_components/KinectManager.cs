using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Squick
{

    

    class KinectManager
    {

        public delegate void StatusChangedEventHandler(object sender, EventArgs e);

        private KinectSensor kinectSensor;
        public string connectedStatus = "Not connected";
        public event StatusChangedEventHandler SensorChanged;

        public KinectManager()
        {
            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(KinectSensors_StatusChanged);
            this.DiscoverKinectSensor();
        }

        public bool hasSensor()
        {
            return (this.kinectSensor != null);
        }

        public KinectSensor getSensor()
        {
            return this.kinectSensor;
        }

        public bool startSensor()
        {
            if (!this.hasSensor()) return false;
            try
            {
                if (kinectSensor.IsRunning) return true;
                kinectSensor.Start();
                kinectSensor.ElevationAngle = 0;
                return true;
            }
            catch
            {
                connectedStatus = "Unable to start the Kinect Sensor";
            }
            return false;
        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            if (this.kinectSensor == e.Sensor)
            {
                if (e.Status == KinectStatus.Disconnected || e.Status == KinectStatus.NotPowered)
                {
                    this.kinectSensor = null;
                    this.DiscoverKinectSensor();
                }
            }
        }

        private void DiscoverKinectSensor()
        {
            foreach (KinectSensor sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    // Found one, set our sensor to this
                    kinectSensor = sensor;
                    break;
                }
            }

            if (this.kinectSensor == null)
            {
                connectedStatus = "Found none Kinect Sensors connected to USB";
                return;
            }

            // You can use the kinectSensor.Status to check for status
            // and give the user some kind of feedback
            switch (kinectSensor.Status)
            {
                case KinectStatus.Connected:
                    {
                        connectedStatus = "Status: Connected";
                        break;
                    }
                case KinectStatus.Disconnected:
                    {
                        connectedStatus = "Status: Disconnected";
                        break;
                    }
                case KinectStatus.NotPowered:
                    {
                        connectedStatus = "Status: Connect the power";
                        break;
                    }
                default:
                    {
                        connectedStatus = "Status: Error";
                        break;
                    }
            }


            if(SensorChanged != null) SensorChanged(this, EventArgs.Empty);
        }
    }
}
