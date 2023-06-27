using Microsoft.Maui.Dispatching;
using static ClickFree_Maui.Helpers.DeviceManger;

namespace ClickFree_Maui.Views;

public partial class MainView : ContentView
{
    List<UsbDisk> disks = new List<UsbDisk>();
    string USBName;
    string formatResult;
    public MainView()
    {
        InitializeComponent();

        //BrushConverter bc = new BrushConverter();
        // firstBorder.Background = (Brush)bc.ConvertFrom("#54BAF4");
        DriveManager.MenuName = "Main";
    }
    public void MainBtn(object sender, System.EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            MainPanel.IsVisible = true;
        }
        else
        {
            MainPanel.IsVisible = false;
        }
        DriveManager.MenuName = "Main";
        SettingsPanel.IsVisible = false;
        AboutPanel.IsVisible = false;
    }
    public void SettingsBtn(object sender, System.EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            SettingsPanel.IsVisible = true;
        }
        else
        {
            SettingsPanel.IsVisible = false;
        }
        DriveManager.MenuName = "Setting";
        MainPanel.IsVisible = false;
        AboutPanel.IsVisible = false;

        // firstBorder.Background = Brushes.Transparent;
    }

    public void EmailBtn(object sender, System.EventArgs e)
    {
        //firstBorder.Background = Brushes.Transparent;
    }

    public void ChatBtn(object sender, System.EventArgs e)
    {
        //firstBorder.Background = Brushes.Transparent;
    }

    public void UserControl_Loaded(object sender, EventArgs e)
    {
        IDispatcherTimer timer;

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(1000);
        timer.Tick += dispatcherTimer_Tick;
        timer.Start();
        /* DispatcherTimer dispatcherTimer = new DispatcherTimer();
         dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
         dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
         dispatcherTimer.Start();*/
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            var DiskInfo = DriveManager.GetAvailableDisks().FirstOrDefault();
            if (DiskInfo != null)
            {
                double bytesFs = DiskInfo.FreeSpace;
                double kilobyteFs = bytesFs / 1024;
                double megabyteFs = kilobyteFs / 1024;
                double gigabyteFs = megabyteFs / 1024;

                double bytesS = DiskInfo.Size;
                double kilobyteS = bytesS / 1024;
                double megabyteS = kilobyteS / 1024;
                double gigabyteS = megabyteS / 1024;

                //usbButton.Background = Brushes.Green;
                //connection.Content = "Connected";
                //space.Content = (float)Math.Round(gigabyteFs, 1) + " GB available out of " + (float)Math.Round(gigabyteS, 1) + " GB";


                switch (DriveManager.MenuName)
                {
                    case "Main":
                        {
                            MainBtn(null, null);
                        }
                        break;

                    case "Setting":
                        {
                            SettingsBtn(null, null);
                        }
                        break;
                    case "About":
                        {
                            aboutButton_Clicked(null, null);
                        }
                        break;
                    default:
                        {
                            MainBtn(null, null);
                        }
                        break;
                }

            }

        }
        else
        {
            //usbButton.Background = Brushes.Red;
            /* connection.Content = "Disconnected";
             space.Content = "";*/
            MainPanel.IsVisible = false;
            SettingsPanel.IsVisible = false;
            AboutPanel.IsVisible = false;
        }

    }

    private void aboutButton_Clicked(object sender, EventArgs e)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        if (ifDrive == true)
        {
            AboutPanel.IsVisible = true;
        }
        else
        {
            AboutPanel.IsVisible = false;
        }
        DriveManager.MenuName = "About";
        MainPanel.IsVisible = false;
        SettingsPanel.IsVisible = false;

        // firstBorder.Background = Brushes.Transparent;

        disks = DriveManager.GetAvailableDisks();
        var disk = disks.FirstOrDefault();
        FirmwareVersionlbl.Text = (string)disk.FirmwareRevision;
        lblFileSystem.Text = disk.FileSystem;
        AppVersionlbl.Text = "1.1.1.112";
        Yearlbl.Text = "© " + DateTime.Now.Year + " Me Too Software, Inc. All rights reserved.";
    }
}
