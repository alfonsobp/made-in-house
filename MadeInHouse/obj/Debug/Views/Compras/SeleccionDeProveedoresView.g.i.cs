﻿#pragma checksum "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E06CF4931D49FB9B84AA6936858C7CDA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18051
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro;
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


namespace MadeInHouse.Views.Compras {
    
    
    /// <summary>
    /// SeleccionDeProveedoresView
    /// </summary>
    public partial class SeleccionDeProveedoresView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid LstConsolidado;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Codigo;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Buscar;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid LstRespuesta;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Cantidad;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AbrirMantenerProveedorViewModel;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MedioPago;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Agregar;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Quitar;
        
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
            System.Uri resourceLocater = new System.Uri("/MadeInHouse;component/views/compras/selecciondeproveedoresview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Compras\SeleccionDeProveedoresView.xaml"
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
            this.LstConsolidado = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.Codigo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Buscar = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.LstRespuesta = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.Cantidad = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.AbrirMantenerProveedorViewModel = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.MedioPago = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.Agregar = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.Quitar = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

