﻿using CrossesAndNoughts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrossesAndNoughts;

public class App : Application
{
    private readonly StartWindow _startWindow;
    private readonly GameWindow _gameWindow;

    public App(StartWindow startWindow, GameWindow gameWindow)
    {
        _startWindow = startWindow;
        _gameWindow = gameWindow;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _startWindow.Show();
        base.OnStartup(e);
    }
}