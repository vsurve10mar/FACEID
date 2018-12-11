using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App1.Common;
using App1.Interface;
using App1.iOS.Helpers;
using App1.Views;
using Foundation;
using LocalAuthentication;
using UIKit;
using Xamarin.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationiOS))]
namespace App1.iOS.Helpers
{
    public class AuthenticationiOS : IAuthentication
    {
        LAContext _context;
       
        public async void Authenticate(Action successAction, Action failAction)
        {
            NSError AuthError;
            if (_context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
            {
                var replyHandler = new LAContextReplyHandler(async (success, error) => {

                    if (success)
                    {
                        await GlobalObject.curMainPage.Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        //Show fallback mechanism here
                        if (failAction != null)
                            failAction.Invoke();
                    }
                });
                _context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Fingerprint Authentication", replyHandler);
            };
        }

        public void CancelCurrentAuthentication()
        {

        }

        public bool IsDeviceSecured()
        {
            NSError error = null;
            bool isDeviceSecured = _context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out error);
            return isDeviceSecured;
        }

        public bool IsFingerprintAuthenticationPossible()
        {
            if (isIOSVersionSupportFingerprint())
            {
                if (IsHardwareDetected())
                {
                    _context = new LAContext();
                    if (IsDeviceSecured())
                    {
                        if (IsFingerPrintEnrolled())
                        {
                            return true;
                        }
                        else
                        {
                            promptDialog("Fingerprint Authentication",
                               "Need to enroll finger first",
                               "Settings",
                               "Cancel", () =>
                               {
                                   NSUrl url = new NSUrl("App-Prefs:root=TOUCHID_PASSCODE");
                                   if (UIApplication.SharedApplication.CanOpenUrl(url))
                                   {
                                       UIApplication.SharedApplication.OpenUrl(url);
                                   }
                               });
                            return false;
                        }
                    }
                    else
                    {
                        promptDialog("Fingerprint Authentication",
                             "Please set the passcode first",
                             "Settings",
                             "Cancel", () =>
                             {
                                 NSUrl url = new NSUrl("App-Prefs:root=TOUCHID_PASSCODE");
                                 if (UIApplication.SharedApplication.CanOpenUrl(url))
                                 {
                                     UIApplication.SharedApplication.OpenUrl(url);
                                 }
                             });
                        return false;
                    }
                }
                else
                {
                    //Action if hardware not support
                    return false;
                }
            }
            else
            {
                //Action if OS not support
                return false;
            }
        }
        public LocalAuthType GetLocalAuthType()
        {
            var localAuthContext = new LAContext();
            NSError AuthError;
            if (localAuthContext.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out AuthError))
            {
                if (localAuthContext.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    if (GetOsMajorVersion() >= 11 && localAuthContext.BiometryType == LABiometryType.TypeFaceId)
#pragma warning restore CS0618 // Type or member is obsolete
                    {
                        return LocalAuthType.FaceId;
                    }
                    return LocalAuthType.TouchId;
                }
                return LocalAuthType.Passcode;
            }
            return LocalAuthType.None;
        }
        private static int GetOsMajorVersion()
        {
            return int.Parse(UIDevice.CurrentDevice.SystemVersion.Split('.')[0]);
        }
        private bool isIOSVersionSupportFingerprint()
        {
            return UIDevice.CurrentDevice.CheckSystemVersion(8, 0);
        }
        public bool IsFingerPrintEnrolled()
        {
            NSError error = null;
            bool isDeviceSecured = _context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error);
            return isDeviceSecured;
        }

        public bool IsHardwareDetected()
        {
            string model = UIDevice.CurrentDevice.Model;
            string deviceVersion = DeviceHardware.Version;
            if (deviceVersion.ToLower().Contains("ipad"))
            {
                List<string> nonSupportediPadList = new List<string>
                {
                 "ipad1,1","ipad2,1","ipad2,2","ipad2,3","ipad2,4","ipad2,5","ipad2,6","ipad2,7","ipad3,1","ipad3,2","ipad3,3",
                    "ipad3,4","ipad3,5","ipad3,6","ipad4,1","ipad4,2","ipad4,3","ipad4,4","ipad4,5","ipad4,6"
                };
                if (nonSupportediPadList.Contains(deviceVersion.ToLower()))
                {
                    return false;
                }
            }
            else if (deviceVersion.ToLower().Contains("iphone"))
            {
                string[] versionName = deviceVersion.Split(',');
                var charArray = versionName.FirstOrDefault().ToCharArray();
                int versionNumber = int.Parse(charArray[charArray.Length - 1].ToString());
                if (versionNumber <= 5)
                    return false;
            }

            return true;
        }

        public bool IsPermissionGranted()
        {
            return true;
        }

        private async void promptDialog(string title, string message, string ok_str, string cancel_str, Action ok_action)
        {
            bool result = await GlobalObject.curMainPage.DisplayAlert(title, message, ok_str, cancel_str);
            if (result)
            {
                ok_action.Invoke();
            }
        }
    }
}