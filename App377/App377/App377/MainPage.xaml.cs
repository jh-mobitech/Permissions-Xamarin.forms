using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App377
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            bool a = await GetPermissions();
        }

        public static async Task<bool> GetPermissions()
        {
            bool permissionsGranted = true;

            var permissionsStartList = new List<Permission>()
        {
            Permission.Location,
            Permission.LocationAlways,
            Permission.LocationWhenInUse,
            Permission.Storage,
            Permission.Camera
        };

            var permissionsNeededList = new List<Permission>();
            try
            {
                foreach (var permission in permissionsStartList)
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                    if (status != PermissionStatus.Granted)
                    {
                        permissionsNeededList.Add(permission);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionsNeededList.ToArray());

            try
            {
                foreach (var permission in permissionsNeededList)
                {
                    var status = PermissionStatus.Unknown;
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(permission))
                        status = results[permission];
                    if (status == PermissionStatus.Granted || status == PermissionStatus.Unknown)
                    {
                        permissionsGranted = true;
                    }
                    else
                    {
                        permissionsGranted = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return permissionsGranted;
        }
    }



}
