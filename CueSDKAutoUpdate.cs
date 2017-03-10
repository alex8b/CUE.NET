// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using System;
using System.Threading;
using CUE.NET.Devices;
using CUE.NET.Devices.Generic.Enums;

namespace CUE.NET
{
    /// <summary>
    /// Static entry point to work with the Corsair-SDK.
    /// </summary>
    public static partial class CueSDK
    {
        #region Properties & Fields

        //private static CancellationTokenSource _updateTokenSource;
        //private static CancellationToken _updateToken;
        private static Thread _updateTask;
	    private static volatile bool _isCancellationRequested;

		/// <summary>
		/// Gets or sets the update-frequency in seconds. (Calculate by using '1f / updates per second')
		/// </summary>
		public static float UpdateFrequency { get; set; } = 1f / 30f;

        private static UpdateMode _updateMode = UpdateMode.Manual;
        /// <summary>
        /// Gets or sets the update-mode for the device.
        /// </summary>
        public static UpdateMode UpdateMode
        {
            get { return _updateMode; }
            set
            {
                _updateMode = value;
                CheckUpdateLoop();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if automatic updates should occur and starts/stops the update-loop if needed.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the requested update-mode is not available.</exception>
        private static void CheckUpdateLoop()
        {
            bool shouldRun;
            switch (UpdateMode)
            {
                case UpdateMode.Manual:
                    shouldRun = false;
                    break;
                case UpdateMode.Continuous:
                    shouldRun = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (shouldRun && _updateTask == null) // Start task
            {
				//_updateTokenSource?.Dispose();
				//_updateTokenSource = new CancellationTokenSource();
	            _isCancellationRequested = false;
				_updateTask = new Thread(UpdateLoop);
				_updateTask.Start();
			}
            else if (!shouldRun && _updateTask != null) // Stop task
            {
				//_updateTokenSource.Cancel();
	            _isCancellationRequested = true;
				_updateTask.Join();
				//_updateTask.Dispose();
                _updateTask = null;
            }
        }

        private static void UpdateLoop()
        {
            while (!_isCancellationRequested)
            {
                long preUpdateTicks = DateTime.Now.Ticks;

                foreach (ICueDevice device in InitializedDevices)
                    device.Update();

                int sleep = (int)((UpdateFrequency * 1000f) - ((DateTime.Now.Ticks - preUpdateTicks) / 10000f));
                if (sleep > 0)
                    Thread.Sleep(sleep);
            }
        }

        #endregion
    }
}
