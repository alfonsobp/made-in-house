﻿using System;
using System.Collections.Generic; 
using System.Drawing; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Windows; 
using System.Windows.Controls; 
using MadeInHouse.Models.Almacen;
using System.Windows.Media;


namespace MadeInHouse.Dictionary { 
    class DynamicGrid : Grid {

        public static readonly DependencyProperty IdZonaProperty = DependencyProperty.Register("IdZona", typeof(int), typeof(DynamicGrid));
        public int IdZona
        {
            get { return (int)GetValue(IdZonaProperty); }
            set { SetValue(IdZonaProperty, value); }
        }

        public static readonly DependencyProperty AlturaProperty = DependencyProperty.Register("Altura", typeof(Int32), typeof(DynamicGrid),new PropertyMetadata(0, new PropertyChangedCallback(OnNumRowsOrColumnsChanged)));

        public Int32 Altura
        {
            get { return (Int32)GetValue(AlturaProperty); }
            set { SetValue(AlturaProperty, value); }
        }


        public static readonly DependencyProperty NumColumnsProperty = DependencyProperty.Register("NumColumns", typeof(Int32), typeof(DynamicGrid), new PropertyMetadata(0, new PropertyChangedCallback(OnNumRowsOrColumnsChanged))); 
        
        public Int32 NumColumns { 
            get { return (Int32)GetValue(NumColumnsProperty);} 
            set { SetValue(NumColumnsProperty, value); } 
        
        } 
        public static readonly DependencyProperty NumRowsProperty = DependencyProperty.Register("NumRows", typeof(Int32), typeof(DynamicGrid), new PropertyMetadata(0, new PropertyChangedCallback(OnNumRowsOrColumnsChanged))); 

        public Int32 NumRows { get { return (Int32)GetValue(NumRowsProperty); } 
            set { SetValue(NumRowsProperty, value); }
        } 




        
        static void OnNumRowsOrColumnsChanged(object sender, DependencyPropertyChangedEventArgs e) {
            ((DynamicGrid)sender).RecreateGridCells(); }


        public List<List<List<Ubicacion>>> Ubicaciones { get; set; }

        public Dictionary<int, int> listaZonas { get; set; }

        public void RecreateGridCells()
        {
            
            Ubicaciones = new List<List<List<Ubicacion>>>();
            int altura = Altura;
            int numRows = NumRows;
            int currentNumRows = RowDefinitions.Count;
            int currentNumCols = ColumnDefinitions.Count;
            int numCols = NumColumns; this.Children.Clear();
            System.Windows.GridLength CL = new System.Windows.GridLength(53);
            System.Windows.GridLength RL = new System.Windows.GridLength(34);

            while (numRows > currentNumRows)
            {
                RowDefinitions.Add(new RowDefinition { Height = RL });

                currentNumRows++;
            }
            while (numRows < currentNumRows)
            {
                currentNumRows--;
                RowDefinitions.RemoveAt(currentNumRows);
            }
            while (numCols > currentNumCols)
            {
                ColumnDefinitions.Add(new ColumnDefinition { Width = CL });
                currentNumCols++;
            }
            while (numCols < currentNumCols)
            {
                currentNumCols--;
                ColumnDefinitions.RemoveAt(currentNumCols);
            }
            
            for (int i = 0; i < numRows; i++)
            {

                Ubicaciones.Add(new List<List<Ubicacion>>());
                for (int j = 0; j < numCols; j++)
                {
                    Ubicaciones[i].Add(new List<Ubicacion>());

                    for (int k = 0; k < Altura; k++)
                    {
                        Ubicaciones[i][j].Add(new Ubicacion());
                        Ubicaciones[i][j][k].CordX = i;
                        Ubicaciones[i][j][k].CordY = j;
                        Ubicaciones[i][j][k].CordZ = k;
                    }
                    
                    Button btnTest = new Button();
                   // BrushConverter conv = new BrushConverter();
                    //SolidColorBrush brush = conv.ConvertFromString("Red") as SolidColorBrush;
                    //btnTest.Background = brush;
                    btnTest.Name = "B" + i + j; //X,Y Fila,Columna
                    //btnTest.Content = "Button" + i + j;
                    btnTest.Click += new RoutedEventHandler(onClickChange);
                    //btnTest.Click(); 
                    Grid.SetRow(btnTest, i);
                    Grid.SetColumn(btnTest, j);
                    this.Children.Add(btnTest);
                    
                }
                UpdateLayout();
            }
        }


        public void onClickChange(object sender, RoutedEventArgs e) {
            
            (sender as Button).Background = this.Background;
            int X =Int16.Parse((sender as Button).Name.Substring(1,1));
            int Y =Int16.Parse((sender as Button).Name.Substring(2,1));

            if (listaZonas == null)
            {
                listaZonas = new Dictionary<int, int>();
            }

            try {
                
                if (Ubicaciones[X][Y][0].IdTipoZona == 0)
                {
                    listaZonas[this.IdZona] += 1;
                }
                else if (Ubicaciones[X][Y][0].IdTipoZona != this.IdZona)
                {
                    if (listaZonas[Ubicaciones[X][Y][0].IdTipoZona] < 0) listaZonas[Ubicaciones[X][Y][0].IdTipoZona] = 0;
                    else listaZonas[Ubicaciones[X][Y][0].IdTipoZona] -= 1;
                    listaZonas[this.IdZona] += 1;
                }
                        
            }
            catch (Exception d) {
                listaZonas.Add(this.IdZona, 1);
            }


            for(int k=0;k<Altura;k++)
                Ubicaciones[X][Y][k].IdTipoZona = this.IdZona;

        }


        public void cargarGrid(List<TipoZona> lstZonas)
        {


            BrushConverter conv = new BrushConverter();           
            for (int i = 0; i < lstZonas.Count; i++)
            {
                for (int j = 0; j < lstZonas[i].LstUbicaciones.Count; j++)
                {

                    SolidColorBrush brush = conv.ConvertFromString(lstZonas[i].Color) as SolidColorBrush;
                    (this.Children[lstZonas[i].LstUbicaciones[j].CordY + NumColumns*lstZonas[i].LstUbicaciones[j].CordX] as Button).Background = brush;
                    (this.Children[lstZonas[i].LstUbicaciones[j].CordY + NumColumns * lstZonas[i].LstUbicaciones[j].CordX] as Button).ToolTip = lstZonas[i].Nombre;
                    
                }
            }
            

        }

        protected override void OnInitialized(EventArgs e) { 
            
            base.OnInitialized(e);
            RecreateGridCells();
            
        } 
    } 
}