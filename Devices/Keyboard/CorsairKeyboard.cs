﻿// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedMember.Global

using System;
using System.Runtime.InteropServices;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Native;

namespace CUE.NET.Devices.Keyboard
{
    /// <summary>
    /// Represents the SDK for a corsair keyboard.
    /// </summary>
    public class CorsairKeyboard : AbstractCueDevice
    {
        #region Properties & Fields

        /// <summary>
        /// Gets specific information provided by CUE for the keyboard.
        /// </summary>
        public CorsairKeyboardDeviceInfo KeyboardDeviceInfo { get; }

        #region Indexers

        /// <summary>
        /// Gets the <see cref="CorsairLed" /> representing the given character by calling the SDK-method 'CorsairGetLedIdForKeyName'.<br />
        /// Note that this currently only works for letters.
        /// </summary>
        /// <param name="key">The character of the key.</param>
        /// <returns>The led representing the given character or null if no led is found.</returns>
        public CorsairLed this[char key]
        {
            get
            {
                CorsairLedId ledId = _CUESDK.CorsairGetLedIdForKeyName(key);
                CorsairLed led;
                return LedMapping.TryGetValue(ledId, out led) ? led : null;
            }
        }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CorsairKeyboard"/> class.
        /// </summary>
        /// <param name="info">The specific information provided by CUE for the keyboard</param>
        internal CorsairKeyboard(CorsairKeyboardDeviceInfo info)
            : base(info)
        {
            this.KeyboardDeviceInfo = info;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the keyboard.
        /// </summary>
        public override void Initialize()
        {
            _CorsairLedPositions nativeLedPositions = (_CorsairLedPositions)Marshal.PtrToStructure(_CUESDK.CorsairGetLedPositions(), typeof(_CorsairLedPositions));
            int structSize = Marshal.SizeOf(typeof(_CorsairLedPosition));
            IntPtr ptr = nativeLedPositions.pLedPosition;
            for (int i = 0; i < nativeLedPositions.numberOfLed; i++)
            {
                _CorsairLedPosition ledPosition = (_CorsairLedPosition)Marshal.PtrToStructure(ptr, typeof(_CorsairLedPosition));
                InitializeLed(ledPosition.ledId, new RectangleF((float)ledPosition.left, (float)ledPosition.top, (float)ledPosition.width, (float)ledPosition.height));

                ptr = new IntPtr(ptr.ToInt64() + structSize);
            }

            base.Initialize();
        }

        #endregion
    }
}
