//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM Telescope driver for Arduino
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Telescope interface version: 2.0
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	1.0.0	Initial edit, from ASCOM Telescope Driver template
// --------------------------------------------------------------------------------
//
using System;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

using ASCOM;
using ASCOM.Helper;
using ASCOM.Helper2;
using ASCOM.Interface;
using ASCOM.Utilities;

namespace ASCOM.Arduino
{
    //
    // Your driver's ID is ASCOM.Arduino.Telescope
    //
    // The Guid attribute sets the CLSID for ASCOM.Arduino.Telescope
    // The ClassInterface/None addribute prevents an empty interface called
    // _Telescope from being created and used as the [default] interface
    //
    [Guid("e56b92d0-fe34-4ed4-9bb1-c558d8330541")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Telescope : ITelescope
    {
        //
        // Driver ID and descriptive string that shows in the Chooser
        //
        private static string s_csDriverID = "ASCOM.Arduino.Telescope";
        // TODO Change the descriptive string for your driver then remove this line
        private static string s_csDriverDescription = "Arduino Telescope";

        private bool connected = false;

        private bool isPulseGuiding = false;

        private bool isSlewing = false;

        private ControlBox ctrl;

        private ASCOM.Utilities.Serial SerialConnection;

        private ASCOM.Helper.Util HC = new ASCOM.Helper.Util();

        //
        // Driver private data (rate collections)
        //
        private AxisRates[] m_AxisRates;
        private TrackingRates m_TrackingRates;

        //
        // Constructor - Must be public for COM registration!
        //
        public Telescope()
        {
            m_AxisRates = new AxisRates[3];
            m_AxisRates[0] = new AxisRates(TelescopeAxes.axisPrimary);
            m_AxisRates[1] = new AxisRates(TelescopeAxes.axisSecondary);
            m_AxisRates[2] = new AxisRates(TelescopeAxes.axisTertiary);
            m_TrackingRates = new TrackingRates();


            SerialConnection = new ASCOM.Utilities.Serial();
            SerialConnection.Port = 4;
            SerialConnection.StopBits = SerialStopBits.One;
            SerialConnection.Parity = SerialParity.None;
            SerialConnection.Speed = SerialSpeed.ps9600;
        }

        #region ASCOM Registration
        //
        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        private static void RegUnregASCOM(bool bRegister)
        {
            Helper.Profile P = new Helper.Profile();
            P.DeviceTypeV = "Telescope";					//  Requires Helper 5.0.3 or later
            if (bRegister)
                P.Register(s_csDriverID, s_csDriverDescription);
            else
                P.Unregister(s_csDriverID);
            try										// In case Helper becomes native .NET
            {
                Marshal.ReleaseComObject(P);
            }
            catch (Exception) { }
            P = null;
        }

        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }
        #endregion

        //
        // PUBLIC COM INTERFACE ITelescope IMPLEMENTATION
        //

        #region ITelescope Members

        public void AbortSlew()
        {
            SerialConnection.Transmit(": H #");
        }

        public AlignmentModes AlignmentMode
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("AlignmentMode", false); }
        }

        public double Altitude
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("Altitude", false); }
        }

        public double ApertureArea
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("ApertureArea", false); }
        }

        public double ApertureDiameter
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("ApertureDiameter", false); }
        }

        public bool AtHome
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("AtHome", false); }
        }

        public bool AtPark
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("AtPark", false); }
        }

        public IAxisRates AxisRates(TelescopeAxes Axis)
        {
            switch (Axis)
            {
                case TelescopeAxes.axisPrimary:
                    return m_AxisRates[0];
                case TelescopeAxes.axisSecondary:
                    return m_AxisRates[1];
                case TelescopeAxes.axisTertiary:
                    return m_AxisRates[2];
                default:
                    return null;
            }
        }

        public double Azimuth
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("Azimuth", false); }
        }

        public bool CanFindHome
        {
            get { return false; }
        }

        public bool CanMoveAxis(TelescopeAxes Axis)
        {
            return true;
        }

        public bool CanPark
        {
            get { return false; }
        }

        public bool CanPulseGuide
        {
            get { return true; }
        }

        public bool CanSetDeclinationRate
        {
            get { return false; }
        }

        public bool CanSetGuideRates
        {
            get { return false; }
        }

        public bool CanSetPark
        {
            get { return false; }
        }

        public bool CanSetPierSide
        {
            get { return false; }
        }

        public bool CanSetRightAscensionRate
        {
            get { return false; }
        }

        public bool CanSetTracking
        {
            get { return false; }
        }

        public bool CanSlew
        {
            get { return false; }
        }

        public bool CanSlewAltAz
        {
            get { return false; }
        }

        public bool CanSlewAltAzAsync
        {
            get { return false; }
        }

        public bool CanSlewAsync
        {
            get { return false; }
        }

        public bool CanSync
        {
            get { return false; }
        }

        public bool CanSyncAltAz
        {
            get { return false; }
        }

        public bool CanUnpark
        {
            get { return false; }
        }

        public void CommandBlind(string Command, bool Raw)
        {
            // TODO Replace this with your implementation
            throw new MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string Command, bool Raw)
        {
            // TODO Replace this with your implementation
            throw new MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string Command, bool Raw)
        {
            // TODO Replace this with your implementation
            throw new MethodNotImplementedException("CommandString");
        }

        public bool Connected
        {
            get { return this.connected; }
            set
            {
                switch (value)
                {
                    case true:
                        this.connected = this.ConnectTelescope();
                        break;
                    case false:
                        this.connected = !this.DisconnectTelescope();
                        break;
                }
            }
        }

        public bool ConnectTelescope()
        {
            ctrl = new ControlBox(this);
            ctrl.Show();
            SerialConnection.Connected = true;
            return true;
        }

        public bool DisconnectTelescope()
        {
            SerialConnection.Connected = false;
            ctrl.Dispose();
            return true;
        }

        public double Declination
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("Declination", false); }
        }

        public double DeclinationRate
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("DeclinationRate", false); }
            set { throw new PropertyNotImplementedException("DeclinationRate", true); }
        }

        public string Description
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("Description", false); }
        }

        public PierSide DestinationSideOfPier(double RightAscension, double Declination)
        {
            // TODO Replace this with your implementation
            throw new MethodNotImplementedException("DestinationSideOfPier");
        }

        public bool DoesRefraction
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("DoesRefraction", false); }
            set { throw new PropertyNotImplementedException("DoesRefraction", true); }
        }

        public string DriverInfo
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("DriverInfo", false); }
        }

        public string DriverVersion
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("DriverVersion", false); }
        }

        public EquatorialCoordinateType EquatorialSystem
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("EquatorialCoordinateType", false); }
        }

        public void FindHome()
        {
            // TODO Replace this with your implementation
            throw new MethodNotImplementedException("FindHome");
        }

        public double FocalLength
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("FocalLength", false); }
        }

        public double GuideRateDeclination
        {
            get { return 1; }
            set { throw new PropertyNotImplementedException("GuideRateDeclination", true); }
        }

        public double GuideRateRightAscension
        {
            get { return 1; }
            set { throw new PropertyNotImplementedException("GuideRateRightAscension", true); }
        }

        public short InterfaceVersion
        {
            // TODO Replace this with your implementation
            get { throw new PropertyNotImplementedException("InterfaceVersion", false); }
        }

        public bool IsPulseGuiding
        {
            get { return this.isPulseGuiding; }
        }

        public void MoveAxis(TelescopeAxes Axis, double Rate)
        {
            this.isSlewing = true;

            switch (Axis)
            {
                case TelescopeAxes.axisPrimary:
                    if (Rate > 0)
                    {
                        SerialConnection.Transmit(": E #");
                    }
                    else if (Rate < 0)
                    {
                        SerialConnection.Transmit(": W #");
                    }
                    break;
                case TelescopeAxes.axisSecondary:
                    if (Rate > 0)
                    {
                        SerialConnection.Transmit(": N #");
                    }
                    else if (Rate < 0)
                    {
                        SerialConnection.Transmit(": S #");
                    }
                    break;
            }

            this.isSlewing = false;
        }

        public string Name
        {
            get { return "Arduino EQ-3M"; }
        }

        public void Park()
        {
            // TODO Replace this with your implementation
            throw new MethodNotImplementedException("Park");
        }

        public void PulseGuide(GuideDirections Direction, int Duration)
        {
            this.isPulseGuiding = true;

            if (this.isSlewing)
                return;

            switch (Direction)
            {
                case GuideDirections.guideNorth:
                    this.MoveAxis(TelescopeAxes.axisSecondary, 1);
                    break;
                case GuideDirections.guideSouth:
                    this.MoveAxis(TelescopeAxes.axisSecondary, -1);
                    break;
                case GuideDirections.guideEast:
                    this.MoveAxis(TelescopeAxes.axisPrimary, 1);
                    break;
                case GuideDirections.guideWest:
                    this.MoveAxis(TelescopeAxes.axisPrimary, -1);
                    break;
            }

            HC.WaitForMilliseconds(Duration);

            this.AbortSlew();

            this.isPulseGuiding = false;
        }

        public double RightAscension
        {
            get { throw new PropertyNotImplementedException("RightAscension", false); }
        }

        public double RightAscensionRate
        {
            get { throw new PropertyNotImplementedException("RightAscensionRate", false); }
            set { throw new PropertyNotImplementedException("RightAscensionRate", true); }
        }

        public void SetPark()
        {
            throw new MethodNotImplementedException("SetPark");
        }

        public void SetupDialog()
        {
            SetupDialogForm F = new SetupDialogForm();
            F.ShowDialog();
        }

        public PierSide SideOfPier
        {
            get { throw new PropertyNotImplementedException("SideOfPier", false); }
            set { throw new PropertyNotImplementedException("SideOfPier", true); }
        }

        public double SiderealTime
        {
            get { throw new PropertyNotImplementedException("SiderealTime", false); }
        }

        public double SiteElevation
        {
            get { throw new PropertyNotImplementedException("SiteElevation", false); }
            set { throw new PropertyNotImplementedException("SiteElevation", true); }
        }

        public double SiteLatitude
        {
            get { throw new PropertyNotImplementedException("SiteLatitude", false); }
            set { throw new PropertyNotImplementedException("SiteLatitude", true); }
        }

        public double SiteLongitude
        {
            get { throw new PropertyNotImplementedException("SiteLongitude", false); }
            set { throw new PropertyNotImplementedException("SiteLongitude", true); }
        }

        public short SlewSettleTime
        {
            get { throw new PropertyNotImplementedException("SlewSettleTime", false); }
            set { throw new PropertyNotImplementedException("SlewSettleTime", true); }
        }

        public void SlewToAltAz(double Azimuth, double Altitude)
        {
            throw new MethodNotImplementedException("SlewToAltAz");
        }

        public void SlewToAltAzAsync(double Azimuth, double Altitude)
        {
            throw new MethodNotImplementedException("SlewToAltAzAsync");
        }

        public void SlewToCoordinates(double RightAscension, double Declination)
        {
            throw new MethodNotImplementedException("SlewToCoordinates");
        }

        public void SlewToCoordinatesAsync(double RightAscension, double Declination)
        {
            throw new MethodNotImplementedException("SlewToCoordinatesAsync");
        }

        public void SlewToTarget()
        {
            throw new MethodNotImplementedException("SlewToTarget");
        }

        public void SlewToTargetAsync()
        {
            throw new MethodNotImplementedException("SlewToTargetAsync");
        }

        public bool Slewing
        {
            get { return this.isSlewing; }
        }

        public void SyncToAltAz(double Azimuth, double Altitude)
        {
            throw new MethodNotImplementedException("SyncToAltAz");
        }

        public void SyncToCoordinates(double RightAscension, double Declination)
        {
            throw new MethodNotImplementedException("SyncToCoordinates");
        }

        public void SyncToTarget()
        {
            throw new MethodNotImplementedException("SyncToTarget");
        }

        public double TargetDeclination
        {
            get { throw new PropertyNotImplementedException("TargetDeclination", false); }
            set { throw new PropertyNotImplementedException("TargetDeclination", true); }
        }

        public double TargetRightAscension
        {
            get { throw new PropertyNotImplementedException("TargetRightAscension", false); }
            set { throw new PropertyNotImplementedException("TargetRightAscension", true); }
        }

        public bool Tracking
        {
            get { throw new PropertyNotImplementedException("Tracking", false); }
            set { throw new PropertyNotImplementedException("Tracking", true); }
        }

        public DriveRates TrackingRate
        {
            get { throw new PropertyNotImplementedException("TrackingRate", false); }
            set { throw new PropertyNotImplementedException("TrackingRate", true); }
        }

        public ITrackingRates TrackingRates
        {
            get { return m_TrackingRates; }
        }

        public DateTime UTCDate
        {
            get { throw new PropertyNotImplementedException("UTCDate", false); }
            set { throw new PropertyNotImplementedException("UTCDate", true); }
        }

        public void Unpark()
        {
            throw new MethodNotImplementedException("Unpark");
        }

        #endregion
    }

    //
    // The Rate class implements IRate, and is used to hold values
    // for AxisRates. You do not need to change this class.
    //
    // The Guid attribute sets the CLSID for ASCOM.Arduino.Rate
    // The ClassInterface/None addribute prevents an empty interface called
    // _Rate from being created and used as the [default] interface
    //
    [Guid("7303efed-dedb-48c6-8ce2-5715f61cd00e")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Rate : IRate
    {
        private double m_dMaximum = 1;
        private double m_dMinimum = 1;

        //
        // Default constructor - Internal prevents public creation
        // of instances. These are values for AxisRates.
        //
        internal Rate(double Minimum, double Maximum)
        {
            m_dMaximum = Maximum;
            m_dMinimum = Minimum;
        }

        #region IRate Members

        public double Maximum
        {
            get { return m_dMaximum; }
            set { m_dMaximum = value; }
        }

        public double Minimum
        {
            get { return m_dMinimum; }
            set { m_dMinimum = value; }
        }

        #endregion
    }

    //
    // AxisRates is a strongly-typed collection that must be enumerable by
    // both COM and .NET. The IAxisRates and IEnumerable interfaces provide
    // this polymorphism. 
    //
    // The Guid attribute sets the CLSID for ASCOM.Arduino.AxisRates
    // The ClassInterface/None addribute prevents an empty interface called
    // _AxisRates from being created and used as the [default] interface
    //
    [Guid("2568f9c4-bec3-4325-b792-d6093a5132eb")]
    [ClassInterface(ClassInterfaceType.None)]
    public class AxisRates : IAxisRates, IEnumerable
    {
        private TelescopeAxes m_Axis;
        private Rate[] m_Rates;

        //
        // Constructor - Internal prevents public creation
        // of instances. Returned by Telescope.AxisRates.
        //
        internal AxisRates(TelescopeAxes Axis)
        {
            m_Axis = Axis;
            //
            // This collection must hold zero or more Rate objects describing the 
            // rates of motion ranges for the Telescope.MoveAxis() method
            // that are supported by your driver. It is OK to leave this 
            // array empty, indicating that MoveAxis() is not supported.
            //
            // Note that we are constructing a rate array for the axis passed
            // to the constructor. Thus we switch() below, and each case should 
            // initialize the array for the rate for the selected axis.
            //
            switch (Axis)
            {
                case TelescopeAxes.axisPrimary:
                    // TODO Initialize this array with any Primary axis rates that your driver may provide
                    // Example: m_Rates = new Rate[] { new Rate(10.5, 30.2), new Rate(54.0, 43.6) }
                    m_Rates = new Rate[0];
                    break;
                case TelescopeAxes.axisSecondary:
                    // TODO Initialize this array with any Secondary axis rates that your driver may provide
                    m_Rates = new Rate[0];
                    break;
                case TelescopeAxes.axisTertiary:
                    // TODO Initialize this array with any Tertiary axis rates that your driver may provide
                    m_Rates = new Rate[0];
                    break;
            }
        }

        #region IAxisRates Members

        public int Count
        {
            get { return m_Rates.Length; }
        }

        public IEnumerator GetEnumerator()
        {
            return m_Rates.GetEnumerator();
        }

        public IRate this[int Index]
        {
            get { return (IRate)m_Rates[Index - 1]; }	// 1-based
        }

        #endregion

    }

    //
    // TrackingRates is a strongly-typed collection that must be enumerable by
    // both COM and .NET. The ITrackingRates and IEnumerable interfaces provide
    // this polymorphism. 
    //
    // The Guid attribute sets the CLSID for ASCOM.Arduino.TrackingRates
    // The ClassInterface/None addribute prevents an empty interface called
    // _TrackingRates from being created and used as the [default] interface
    //
    [Guid("145eed98-33f2-41be-ba0a-74fc252243fb")]
    [ClassInterface(ClassInterfaceType.None)]
    public class TrackingRates : ITrackingRates, IEnumerable
    {
        private DriveRates[] m_TrackingRates;

        //
        // Default constructor - Internal prevents public creation
        // of instances. Returned by Telescope.AxisRates.
        //
        internal TrackingRates()
        {
            //
            // This array must hold ONE or more DriveRates values, indicating
            // the tracking rates supported by your telescope. The one value
            // (tracking rate) that MUST be supported is driveSidereal!
            //
            m_TrackingRates = new DriveRates[] { DriveRates.driveSidereal };
            // TODO Initialize this array with any additional tracking rates that your driver may provide
        }

        #region ITrackingRates Members

        public int Count
        {
            get { return m_TrackingRates.Length; }
        }

        public IEnumerator GetEnumerator()
        {
            return m_TrackingRates.GetEnumerator();
        }

        public DriveRates this[int Index]
        {
            get { return m_TrackingRates[Index - 1]; }	// 1-based
        }

        #endregion
    }
}
