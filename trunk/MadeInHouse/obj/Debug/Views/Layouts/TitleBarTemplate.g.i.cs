﻿#pragma checksum "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5570643ACD53821BAF8E9A0757682CC6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34003
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MadeInHouse.Views.Layouts {
    
    
    /// <summary>
    /// TitleBarTemplate
    /// </summary>
    public partial class TitleBarTemplate : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel titleBar;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseWin;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MinimizeWin;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MadeInHouse;component/views/layouts/titlebartemplate.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.titleBar = ((System.Windows.Controls.DockPanel)(target));
            
            #line 40 "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml"
            this.titleBar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.titleBar_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CloseWin = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml"
            this.CloseWin.Click += new System.Windows.RoutedEventHandler(this.CloseWin_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MinimizeWin = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\..\Views\Layouts\TitleBarTemplate.xaml"
            this.MinimizeWin.Click += new System.Windows.RoutedEventHandler(this.MinimizeWin_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

