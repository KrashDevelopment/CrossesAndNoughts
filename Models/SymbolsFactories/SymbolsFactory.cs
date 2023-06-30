﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CrossesAndNoughts.Models.SymbolsFactories;

public abstract class SymbolsFactory
{
    public abstract Image CreateSymbol();

    protected static Image CustomizedSymbol(string path)
    {
        return new Image()
        {
            Source = BitmapFrame.Create(new Uri(path)),
            Visibility = Visibility.Visible,
            Margin = new Thickness(10)
        };
    }
}

public class CrossesFactory : SymbolsFactory
{
    public override Image CreateSymbol()
    {
        return CustomizedSymbol(Directory.GetCurrentDirectory() + @"\Cross.png");
    }
}

public class NoughtsFactory : SymbolsFactory
{
    public override Image CreateSymbol()
    {
        return CustomizedSymbol(Directory.GetCurrentDirectory() + @"\Nought.png");
    }
}
