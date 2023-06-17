﻿using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace GTA5MenuExtra;

/// <summary>
/// HeistsEditWindow.xaml 的交互逻辑
/// </summary>
public partial class HeistsEditWindow
{
    /// <summary>
    /// 导航字典
    /// </summary>
    private readonly Dictionary<string, UserControl> NavDictionary = new();

    public HeistsEditWindow()
    {
        InitializeComponent();

        CreateView();
    }

    private void Window_HeistsEdit_Loaded(object sender, RoutedEventArgs e)
    {
        Navigate(NavDictionary.First().Key);
    }

    private void Window_HeistsEdit_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// 创建页面
    /// </summary>
    private void CreateView()
    {
        foreach (var item in ControlHelper.GetControls(Grid_NavMenu).Cast<RadioButton>())
        {
            var viewName = item.CommandParameter.ToString();

            if (!NavDictionary.ContainsKey(viewName))
            {
                var type = Type.GetType($"GTA5MenuExtra.Views.HeistsEdit.{viewName}");
                if (type == null)
                    continue;

                NavDictionary.Add(viewName, Activator.CreateInstance(type) as UserControl);
            }
        }
    }

    /// <summary>
    /// View页面导航
    /// </summary>
    /// <param name="viewName"></param>
    [RelayCommand]
    private void Navigate(string viewName)
    {
        if (!NavDictionary.ContainsKey(viewName))
            return;

        if (ContentControl_NavRegion.Content != NavDictionary[viewName])
            ContentControl_NavRegion.Content = NavDictionary[viewName];
    }
}