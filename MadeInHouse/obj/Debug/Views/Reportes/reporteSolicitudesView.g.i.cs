﻿#pragma checksum "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B58DF9FC91B637B6667FA30BDCA8A7A8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18051
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MadeInHouse.Views.Layouts;
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


namespace MadeInHouse.Views.Reportes {
    
    
    /// <summary>
    /// reporteSolicitudesView
    /// </summary>
    public partial class reporteSolicitudesView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxProveedores1;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxProveedores2;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button derecha1;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button izquierda1;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxCategorias1;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxCategorias2;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button derecha2;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button izquierda2;
        
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
            System.Uri resourceLocater = new System.Uri("/MadeInHouse;component/views/reportes/reportesolicitudesview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.listBoxProveedores1 = ((System.Windows.Controls.ListBox)(target));
            
            #line 44 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxProveedores1.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Unselect);
            
            #line default
            #line hidden
            
            #line 44 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxProveedores1.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListBoxItem_DoubleClicked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.listBoxProveedores2 = ((System.Windows.Controls.ListBox)(target));
            
            #line 49 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxProveedores2.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Unselect);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxProveedores2.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListBoxItem_DoubleClicked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.derecha1 = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.derecha1.Click += new System.Windows.RoutedEventHandler(this.Pasar_Todo);
            
            #line default
            #line hidden
            return;
            case 4:
            this.izquierda1 = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.izquierda1.Click += new System.Windows.RoutedEventHandler(this.Pasar_Todo);
            
            #line default
            #line hidden
            return;
            case 5:
            this.listBoxCategorias1 = ((System.Windows.Controls.ListBox)(target));
            
            #line 62 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxCategorias1.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Unselect);
            
            #line default
            #line hidden
            
            #line 62 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxCategorias1.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListBoxItem_DoubleClicked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.listBoxCategorias2 = ((System.Windows.Controls.ListBox)(target));
            
            #line 70 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxCategorias2.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Unselect);
            
            #line default
            #line hidden
            
            #line 70 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.listBoxCategorias2.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListBoxItem_DoubleClicked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.derecha2 = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.derecha2.Click += new System.Windows.RoutedEventHandler(this.Pasar_Todo);
            
            #line default
            #line hidden
            return;
            case 8:
            this.izquierda2 = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\..\Views\Reportes\reporteSolicitudesView.xaml"
            this.izquierda2.Click += new System.Windows.RoutedEventHandler(this.Pasar_Todo);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

