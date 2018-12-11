using App1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Interface
{
    public interface IAuthentication
    {
        bool IsHardwareDetected();
        bool IsDeviceSecured();
        bool IsFingerPrintEnrolled();
        bool IsPermissionGranted();
        void Authenticate(Action successAction = null, Action failAction = null);
        void CancelCurrentAuthentication();
        bool IsFingerprintAuthenticationPossible();
        LocalAuthType GetLocalAuthType();
    }
}
