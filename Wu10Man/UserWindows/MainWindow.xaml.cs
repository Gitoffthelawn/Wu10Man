﻿using System;
using System.Windows;
using System.Windows.Controls;
using WereDev.Utils.Wu10Man.Core;
using WereDev.Utils.Wu10Man.Core.Interfaces;
using WereDev.Utils.Wu10Man.Services;
using WereDev.Utils.Wu10Man.UserControls;
using WereDev.Utils.Wu10Man.UserWindows.Models;

namespace WereDev.Utils.Wu10Man.UserWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogWriter _logWriter;
        private readonly MainWindowModel _mainWindowModel;

        public MainWindow()
        {
            _logWriter = DependencyManager.LogWriter;
            _mainWindowModel = new MainWindowModel()
            {
                TabItems = new ITabItemModel<UserControl>[]
                {
                    new TabItemModel<WindowsServicesControl> { BackgroundColor = "#f25022", Header = "Windows Services" },
                    new TabItemModel<PauseUpdatesControl> { BackgroundColor = "#01a4ef", Header = "Pause Updates" },
                    new TabItemModel<DeclutterControl> { BackgroundColor = "#ffb901", Header = "BETA - Declutter" },
                    new TabItemModel<HostsFileControl> { BackgroundColor = "LightGray", Header = "LEGACY - Hosts File", IsLegacy = true },
                    new TabItemModel<GroupPolicyControl> { BackgroundColor = "LightGray", Header = "LEGACY - Group Policy", IsLegacy = true },
                },
            };

            _logWriter.LogInfo("Main window initializing.");
            InitializeComponent();
            DataContext = _mainWindowModel;
            _logWriter.LogInfo("Main window initialized.");
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            DisplayWindow(new About());
        }

        private void LogFilesItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start((_logWriter as Wu10Logger)?.LogFolder);
        }

        private void ReadmeItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://weredev.com/developer/wu10man/");
        }

        private void DisplayWindow(Window window)
        {
            window.Left = Left + ((Width - window.Width) / 2);
            window.Top = Top + ((Height - window.Height) / 2);
            window.ShowDialog();
        }

        private void BuyMeACoffee_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.buymeacoffee.com/weredev");
        }

        private void ViewLegacy_Click(object sender, RoutedEventArgs e)
        {
            _mainWindowModel.ShowLegacy = !_mainWindowModel.ShowLegacy;
        }
    }
}
