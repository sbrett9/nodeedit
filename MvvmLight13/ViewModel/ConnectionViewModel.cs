﻿namespace MvvmLight13.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// Defines a connection between two connectors (aka connection points) of two nodes.
    /// </summary>
    public sealed class ConnectionViewModel : ViewModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// The source connector the connection is attached to.
        /// </summary>
        private ConnectorViewModel sourceConnector = null;

        /// <summary>
        /// The destination connector the connection is attached to.
        /// </summary>
        private ConnectorViewModel destConnector = null;

        /// <summary>
        /// The source and dest hotspots used for generating connection points.
        /// </summary>
        private Point sourceConnectorHotspot;
        private Point destConnectorHotspot;

        /// <summary>
        /// Points that make up the connection.
        /// </summary>
        private PointCollection points = null;

        #endregion Internal Data Members

        /// <summary>
        /// The source connector the connection is attached to.
        /// </summary>
        public ConnectorViewModel SourceConnector
        {
            get
            {
                return sourceConnector;
            }
            set
            {
                if (sourceConnector == value)
                {
                    return;
                }

                if (sourceConnector != null)
                {
                    sourceConnector.AttachedConnection = null;
                    sourceConnector.HotspotUpdated -= new EventHandler<EventArgs>(sourceConnector_HotspotUpdated);
                }

                sourceConnector = value;

                if (sourceConnector != null)
                {
                    sourceConnector.AttachedConnection = this;
                    sourceConnector.HotspotUpdated += new EventHandler<EventArgs>(sourceConnector_HotspotUpdated);
                    this.SourceConnectorHotspot = sourceConnector.Hotspot;
                }

                RaisePropertyChanged(()=>SourceConnector);
                OnConnectionChanged();
            }
        }

        /// <summary>
        /// The destination connector the connection is attached to.
        /// </summary>
        public ConnectorViewModel DestConnector
        {
            get
            {
                return destConnector;
            }
            set
            {
                if (destConnector == value)
                {
                    return;
                }

                if (destConnector != null)
                {
                    destConnector.AttachedConnection = null;
                    destConnector.HotspotUpdated -= new EventHandler<EventArgs>(destConnector_HotspotUpdated);
                }

                destConnector = value;

                if (destConnector != null)
                {
                    destConnector.AttachedConnection = this;
                    destConnector.HotspotUpdated += new EventHandler<EventArgs>(destConnector_HotspotUpdated);
                    this.DestConnectorHotspot = destConnector.Hotspot;
                }

                RaisePropertyChanged(()=>DestConnector);
                OnConnectionChanged();
            }
        }

        /// <summary>
        /// The source and dest hotspots used for generating connection points.
        /// </summary>
        public Point SourceConnectorHotspot
        {
            get
            {
                return sourceConnectorHotspot;
            }
            set
            {
                sourceConnectorHotspot = value;

                ComputeConnectionPoints();

                RaisePropertyChanged(()=>SourceConnectorHotspot);
            }
        }

        public Point DestConnectorHotspot
        {
            get
            {
                return destConnectorHotspot;
            }
            set
            {
                destConnectorHotspot = value;

                ComputeConnectionPoints();

                RaisePropertyChanged(()=>DestConnectorHotspot);
            }
        }

        /// <summary>
        /// Points that make up the connection.
        /// </summary>
        public PointCollection Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;

                RaisePropertyChanged("Points");
            }
        }

        /// <summary>
        /// Event fired when the connection has changed.
        /// </summary>
        public event EventHandler<EventArgs> ConnectionChanged;

        #region Private Methods

        /// <summary>
        /// Raises the 'ConnectionChanged' event.
        /// </summary>
        private void OnConnectionChanged()
        {
            if (ConnectionChanged != null)
            {
                ConnectionChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Event raised when the hotspot of the source connector has been updated.
        /// </summary>
        private void sourceConnector_HotspotUpdated(object sender, EventArgs e)
        {
            this.SourceConnectorHotspot = this.SourceConnector.Hotspot;
        }

        /// <summary>
        /// Event raised when the hotspot of the dest connector has been updated.
        /// </summary>
        private void destConnector_HotspotUpdated(object sender, EventArgs e)
        {
            this.DestConnectorHotspot = this.DestConnector.Hotspot;
        }

        /// <summary>
        /// Rebuild connection points.
        /// </summary>
        private void ComputeConnectionPoints()
        {
            PointCollection computedPoints = new PointCollection();
            computedPoints.Add(this.SourceConnectorHotspot);

            double deltaX = Math.Abs(this.DestConnectorHotspot.X - this.SourceConnectorHotspot.X);
            double deltaY = Math.Abs(this.DestConnectorHotspot.Y - this.SourceConnectorHotspot.Y);
            if (deltaX > deltaY)
            {
                double midPointX = this.SourceConnectorHotspot.X + ((this.DestConnectorHotspot.X - this.SourceConnectorHotspot.X) / 2);
                computedPoints.Add(new Point(midPointX, this.SourceConnectorHotspot.Y));
                computedPoints.Add(new Point(midPointX, this.DestConnectorHotspot.Y));
            }
            else
            {
                double midPointY = this.SourceConnectorHotspot.Y + ((this.DestConnectorHotspot.Y - this.SourceConnectorHotspot.Y) / 2);
                computedPoints.Add(new Point(this.SourceConnectorHotspot.X, midPointY));
                computedPoints.Add(new Point(this.DestConnectorHotspot.X, midPointY));
            }

            computedPoints.Add(this.DestConnectorHotspot);
            computedPoints.Freeze();

            this.Points = computedPoints;
        }

        #endregion Private Methods
    }
}