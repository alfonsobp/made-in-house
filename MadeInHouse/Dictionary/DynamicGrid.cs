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



        private static readonly DependencyProperty ColumnaUProperty = DependencyProperty.Register("ColumnaU", typeof(List<Ubicacion>), typeof(DynamicGrid));

        public  List<Ubicacion> ColumnaU
        {
            get { return (List<Ubicacion>)GetValue(ColumnaUProperty); }
            set { SetValue(ColumnaUProperty, value); }
        }


        private static readonly DependencyProperty ColumnaProperty = DependencyProperty.Register("Columna", typeof(List<Ubicacion>), typeof(DynamicGrid), new PropertyMetadata(new List<Ubicacion>(), new PropertyChangedCallback(OnColumnChanged)));

        public List<Ubicacion> Columna
        {
            get { return (List<Ubicacion>)GetValue(ColumnaProperty); }
            set { SetValue(ColumnaProperty, value); }
        }

        

        public static readonly DependencyProperty NumRowsUProperty = DependencyProperty.Register("NumRowsU", typeof(int), typeof(DynamicGrid));
        public int NumRowsU
        {
            get { return (int)GetValue(NumRowsUProperty); }
            set { SetValue(NumRowsUProperty, value); }
        }

        public static readonly DependencyProperty NumColumnsUProperty = DependencyProperty.Register("NumColumnsU", typeof(int), typeof(DynamicGrid));
        public int NumColumnsU
        {
            get { return (int)GetValue(NumColumnsUProperty); }
            set { SetValue(NumColumnsUProperty, value); }
        }

        public static readonly DependencyProperty AlturaUProperty = DependencyProperty.Register("AlturaU", typeof(int), typeof(DynamicGrid));
        public int AlturaU
        {
            get { return (int)GetValue(AlturaUProperty); }
            set { SetValue(AlturaUProperty, value); }
        }




        public static readonly DependencyProperty EnableProperty = DependencyProperty.Register("Enable", typeof(bool), typeof(DynamicGrid));
        public bool Enable
        {
            get { return (bool)GetValue(EnableProperty); }
            set { SetValue(EnableProperty, value); }
        }


        public static readonly DependencyProperty TipoProperty = DependencyProperty.Register("Tipo", typeof(int), typeof(DynamicGrid));
        public int Tipo
        {
            get { return (int)GetValue(TipoProperty); }
            set { SetValue(TipoProperty, value); }
        }

        private static readonly DependencyProperty CantActualProperty = DependencyProperty.Register("CantActual", typeof(string), typeof(DynamicGrid));
        public string CantActual
        {
            get { return (string)GetValue(CantActualProperty); }
            set { SetValue(CantActualProperty, value); }
        }

        private static readonly DependencyProperty VolOcuProperty = DependencyProperty.Register("VolOcu", typeof(string), typeof(DynamicGrid));
        public string VolOcu
        {
            get { return (string)GetValue(VolOcuProperty); }
            set { SetValue(VolOcuProperty, value); }
        }


        public static readonly DependencyProperty ObjetoProperty = DependencyProperty.Register("Objeto", typeof(Prueba), typeof(DynamicGrid));
        public Prueba Objeto
        {
            get { return (Prueba)GetValue(ObjetoProperty); }
            set { SetValue(ObjetoProperty, value); }
        }


        public static readonly DependencyProperty GridChildProperty = DependencyProperty.Register("GridChild", typeof(DynamicGrid), typeof(DynamicGrid));
        public DynamicGrid GridChild
        {
            get { return (DynamicGrid)GetValue(GridChildProperty); }
            set { SetValue(GridChildProperty, value); }
        }

        public static readonly DependencyProperty AccionProperty = DependencyProperty.Register("Accion", typeof(int), typeof(DynamicGrid));//, new PropertyMetadata(1, new PropertyChangedCallback(OnNumRowsOrColumnsChanged)));
        public int Accion
        {
            get { return (int)GetValue(AccionProperty); }
            set { SetValue(AccionProperty, value); }
        }

        public static readonly DependencyProperty lstZonasProperty = DependencyProperty.Register("lstZonas", typeof(List<TipoZona>), typeof(DynamicGrid), new PropertyMetadata(new List<TipoZona>(), new PropertyChangedCallback(OnLstZonasChanged)));
        public List<TipoZona> lstZonas
        {
            get { return (List<TipoZona>)GetValue(lstZonasProperty); }
            set { SetValue(lstZonasProperty, value); }
        }


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

        static void OnLstZonasChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((DynamicGrid)sender).cargarGrid();
        }

        static void OnColumnChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((DynamicGrid)sender).Mostrar();
        }

        public List<List<List<Ubicacion>>> Ubicaciones { get; set; }

        public Dictionary<int, int> listaZonas { get; set; }

        public void RecreateGridCells()
        {
            
            Ubicaciones = new List<List<List<Ubicacion>>>();
            int altura = Altura;
            int numRows = NumRows;
            int currentNumRows = RowDefinitions.Count;
            int currentNumCols = ColumnDefinitions.Count;
            int numCols = NumColumns; 
            this.Children.Clear();

            System.Windows.GridLength CL = new System.Windows.GridLength(Tipo==2 ? 250:53);
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
                        Ubicaciones[i][j][k].Cantidad = 0;
                        Ubicaciones[i][j][k].VolOcupado = 0;
                    }
                    
                    Button btnTest = new Button();
                    btnTest.Name = "B"+i + "X" + "Y" +j ; //X,Y Fila,Columna
                    
                    btnTest.Click += new RoutedEventHandler(onClickChange);
                    Grid.SetRow(btnTest, i);
                    Grid.SetColumn(btnTest, j);
                    this.Children.Add(btnTest);
                    
                }
                UpdateLayout();
            }
        }

        public void Mostrar()
        {

            BrushConverter conv = new BrushConverter();
            SolidColorBrush colorname = conv.ConvertFromString("White") as SolidColorBrush;
            //SolidColorBrush ocupado = conv.ConvertFromString("Gray") as SolidColorBrush;
            SolidColorBrush foreground = conv.ConvertFromString("WhiteSmoke") as SolidColorBrush;
            
            bool enable = true;

            if (Columna != null)
            {
                for (int j = 0; j < Columna.Count; j++)
                {
                    idSelectedProduct = Columna[j].IdProducto;
                    if (Columna[j].IdProducto == (SelectedProduct == null ? -1 : SelectedProduct.IdProducto))
                    {
                        enable = true;
                        (this.Children[Columna[j].CordZ] as Button).IsEnabled = enable;
                        (this.Children[Columna[j].CordZ] as Button).Foreground = foreground;
                        (this.Children[Columna[j].CordZ] as Button).FontSize = 25;
                        (this.Children[Columna[j].CordZ] as Button).FontWeight = FontWeights.Bold;
                        (this.Children[Columna[j].CordZ] as Button).Content = SelectedProduct.CodigoProd;
                       
                        LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
                        myLinearGradientBrush.StartPoint = new System.Windows.Point(0, 0.5);
                        myLinearGradientBrush.EndPoint = new System.Windows.Point(Columna[j].VolOcupado / 10, 0.5);
                        myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.1));
                        myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.LightGreen, 0.1));

                        (this.Children[Columna[j].CordZ] as Button).Background = myLinearGradientBrush;
                        (this.Children[Columna[j].CordZ] as Button).Content = Columna[j].Cantidad + " - " + Columna[j].VolOcupado + "%";

                    }
                    else if (Columna[j].IdProducto==0)
                    {
                        (this.Children[Columna[j].CordZ] as Button).Background = colorname;
                        (this.Children[Columna[j].CordZ] as Button).Content = "VACIO";
                    }
                    else 
                    {
                        enable = false;
                        LinearGradientBrush ocupado = new LinearGradientBrush();
                        ocupado.StartPoint = new System.Windows.Point(0, 0.5);
                        ocupado.EndPoint = new System.Windows.Point(10, 0.5);
                        ocupado.GradientStops.Add(new GradientStop(Colors.Gray, 0.1));

                        (this.Children[Columna[j].CordZ] as Button).Background = ocupado;
                        (this.Children[Columna[j].CordZ] as Button).Content = "OCUPADO";
                        
                        
                    }
                    
                    

                }
            }
        }

        private int xAnt;
        private int yAnt;
        private SolidColorBrush colorAnt;
        private LinearGradientBrush colorAntU;

        public void onClickChange(object sender, RoutedEventArgs e) {

            int X = Int16.Parse((sender as Button).Name.Substring(1, (sender as Button).Name.IndexOf("X") - 1));
            int Y = Int16.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("Y") + 1));
            BrushConverter conv = new BrushConverter();
            SolidColorBrush colorClick = conv.ConvertFromString("LightSkyBlue") as SolidColorBrush;
            //Colors.
            if (Accion == 1)
            {
                (sender as Button).Background = this.Background;

                if (listaZonas == null)
                {
                    listaZonas = new Dictionary<int, int>();
                }

                try
                {

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
                catch (Exception d)
                {
                    listaZonas.Add(this.IdZona, 1);
                }


                for (int k = 0; k < Altura; k++)
                    Ubicaciones[X][Y][k].IdTipoZona = this.IdZona;
            }
            else if (Accion ==2)
            {
                if (colorAnt != null)
                {
                    (this.Children[Ubicaciones[xAnt][yAnt][0].CordY + Ubicaciones[xAnt][yAnt][0].CordX * NumColumns] as Button).Background = colorAnt;
                }
                else if (colorAntU != null)
                {
                    (this.Children[Ubicaciones[xAnt][yAnt][0].CordY + Ubicaciones[xAnt][yAnt][0].CordX * NumColumns] as Button).Background = colorAntU;
                }

                xAnt = X;
                yAnt = Y;
                if (Tipo==1)
                    colorAnt = (this.Children[Ubicaciones[X][Y][0].CordY + Ubicaciones[X][Y][0].CordX * NumColumns] as Button).Background as SolidColorBrush ;
                else 
                    colorAntU = (this.Children[Ubicaciones[X][Y][0].CordY + Ubicaciones[X][Y][0].CordX * NumColumns] as Button).Background as LinearGradientBrush;

                (this.Children[Ubicaciones[X][Y][0].CordY + Ubicaciones[X][Y][0].CordX * NumColumns] as Button).Background = colorClick;

                if (Tipo == 1)
                {

                    NumColumnsU = 1;
                    AlturaU = 1;
                    NumRowsU = Altura;
                    ColumnaU = Ubicaciones[X][Y];
                    
                }
                if (Tipo == 2)
                {
                    if (Columna[X].IdProducto == idSelectedProduct)
                    {
                        selectedUbicacion = Columna[X];
                        CantActual = Columna[X].Cantidad.ToString();
                        VolOcu = Columna[X].VolOcupado.ToString();
                        if (int.Parse(VolOcu) > 0) Enable = false;
                        else Enable = true;
                    }
                    else
                    {
                        CantActual = "---";
                        VolOcu = "---";
                    }
                }

            }
            else if (Accion == 3)
            {


            }
        }

        private Ubicacion selectedUbicacion;

        public void cargarGrid(List<TipoZona> lstZonas = null)
        {
            if (lstZonas != null) 
                this.lstZonas = lstZonas;

           
            BrushConverter conv = new BrushConverter();
            SolidColorBrush colorname = conv.ConvertFromString("Black") as SolidColorBrush;
            for (int i = 0; i < this.lstZonas.Count ; i++)
            {
                for (int j = 0; j < this.lstZonas[i].LstUbicaciones.Count; j++)
                {
                    
                    SolidColorBrush brush = conv.ConvertFromString(this.lstZonas[i].Color) as SolidColorBrush;
                    (this.Children[this.lstZonas[i].LstUbicaciones[j].CordY + NumColumns * this.lstZonas[i].LstUbicaciones[j].CordX] as Button).Background = brush;
                    (this.Children[this.lstZonas[i].LstUbicaciones[j].CordY + NumColumns * this.lstZonas[i].LstUbicaciones[j].CordX] as Button).ToolTip = this.lstZonas[i].Nombre;
                    Ubicaciones[this.lstZonas[i].LstUbicaciones[j].CordX][this.lstZonas[i].LstUbicaciones[j].CordY][this.lstZonas[i].LstUbicaciones[j].CordZ].IdTipoZona = this.lstZonas[i].LstUbicaciones[j].IdTipoZona;
                    Ubicaciones[this.lstZonas[i].LstUbicaciones[j].CordX][this.lstZonas[i].LstUbicaciones[j].CordY][this.lstZonas[i].LstUbicaciones[j].CordZ].IdProducto = this.lstZonas[i].LstUbicaciones[j].IdProducto;
                    Ubicaciones[this.lstZonas[i].LstUbicaciones[j].CordX][this.lstZonas[i].LstUbicaciones[j].CordY][this.lstZonas[i].LstUbicaciones[j].CordZ].IdUbicacion = this.lstZonas[i].LstUbicaciones[j].IdUbicacion;
                    Ubicaciones[this.lstZonas[i].LstUbicaciones[j].CordX][this.lstZonas[i].LstUbicaciones[j].CordY][this.lstZonas[i].LstUbicaciones[j].CordZ].Cantidad = this.lstZonas[i].LstUbicaciones[j].Cantidad;
                    Ubicaciones[this.lstZonas[i].LstUbicaciones[j].CordX][this.lstZonas[i].LstUbicaciones[j].CordY][this.lstZonas[i].LstUbicaciones[j].CordZ].VolOcupado = this.lstZonas[i].LstUbicaciones[j].VolOcupado;
                    
                    

                }
            }
            

        }

        public ProductoCant SelectedProduct;
        public int idSelectedProduct;

        public void UbicarProducto(int idProducto)
        {

            BrushConverter conv = new BrushConverter();
            SolidColorBrush colorname = conv.ConvertFromString("White") as SolidColorBrush;
            

            for (int i = 0; i < this.lstZonas.Count; i++)
            {
                for (int j = 0; j < this.lstZonas[i].LstUbicaciones.Count; j++)
                {
                        (this.Children[this.lstZonas[i].LstUbicaciones[j].CordY + NumColumns * this.lstZonas[i].LstUbicaciones[j].CordX] as Button).Content = "";
                }
            }


            int acumulado = 0;

            for (int i = 0; i < Ubicaciones.Count; i++)
            {
                for (int j = 0; j < Ubicaciones[i].Count; j++)
                {
                    acumulado = 0;
                    for (int k = 0; k < Ubicaciones[i][j].Count; k++)
                    {
                        if (Ubicaciones[i][j][k].IdProducto == idProducto)
                        {
                            (this.Children[Ubicaciones[i][j][k].CordY + NumColumns * Ubicaciones[i][j][k].CordX] as Button).Foreground = colorname;
                             acumulado += Ubicaciones[i][j][k].Cantidad;
                            (this.Children[Ubicaciones[i][j][k].CordY + NumColumns * Ubicaciones[i][j][k].CordX] as Button).Content = "" + acumulado;
                            (this.Children[Ubicaciones[i][j][k].CordY + NumColumns * Ubicaciones[i][j][k].CordX] as Button).FontSize = 25;
                        }
                    }

                }
            }
        }

        public int AgregarProductos(int cant, int vol, int idProducto)
        {

            int volAux;
            if (selectedUbicacion.VolOcupado != 0)
            {
             
                volAux =selectedUbicacion.VolOcupado + cant * selectedUbicacion.VolOcupado / selectedUbicacion.Cantidad;
                if (volAux > 100)
                {
                    System.Windows.MessageBox.Show("No se pudo ingresar , se superó la capacidad");
                    return -1;
                }
                else if (volAux == 100)
                {
                    System.Windows.MessageBox.Show("La ubicación esta en su máxima capacidad");
                    selectedUbicacion.VolOcupado=volAux;
                }
                else selectedUbicacion.VolOcupado = volAux;
            }
            else
            {
                selectedUbicacion.IdProducto = idProducto;
                selectedUbicacion.VolOcupado = vol;
            }

            
            
            Ubicacion ubiModificada = new Ubicacion();
            ubiModificada.IdProducto = SelectedProduct.IdProducto;
            ubiModificada.IdUbicacion = selectedUbicacion.IdUbicacion;
            ubiModificada.Cantidad = cant;
            SelectedProduct.Ubicaciones.Add(ubiModificada);

            selectedUbicacion.Cantidad += cant;
            Columna[selectedUbicacion.CordZ].Cantidad = selectedUbicacion.Cantidad;
            Columna[selectedUbicacion.CordZ].VolOcupado = selectedUbicacion.VolOcupado;
            Columna[selectedUbicacion.CordZ].IdProducto = selectedUbicacion.IdProducto;

            CantActual = selectedUbicacion.Cantidad.ToString();
            VolOcu = selectedUbicacion.VolOcupado.ToString();
            this.Mostrar();

            return 1;
        }

        public int DisminuirProductos(int cant, int idProducto)
        {

            int cantAux;
            if (selectedUbicacion.Cantidad>0)
            {

                cantAux = selectedUbicacion.Cantidad - cant ;
                if (cantAux < 0)
                {
                    System.Windows.MessageBox.Show("No hay suficientes productos");
                    return -1;
                }
                else selectedUbicacion.VolOcupado = selectedUbicacion.VolOcupado - cant * selectedUbicacion.VolOcupado / selectedUbicacion.Cantidad; 
            }
            else
            {
                System.Windows.MessageBox.Show("No hay productos en esa ubicacion");
                return -1;
            }



            Ubicacion ubiModificada = new Ubicacion();
            ubiModificada.IdProducto = SelectedProduct.IdProducto;
            ubiModificada.IdUbicacion = selectedUbicacion.IdUbicacion;
            ubiModificada.Cantidad = cant;
            SelectedProduct.Ubicaciones.Add(ubiModificada);

            selectedUbicacion.Cantidad -= cant;
            Columna[selectedUbicacion.CordZ].Cantidad = selectedUbicacion.Cantidad;
            Columna[selectedUbicacion.CordZ].VolOcupado = selectedUbicacion.VolOcupado;

            CantActual = selectedUbicacion.Cantidad.ToString();
            VolOcu = selectedUbicacion.VolOcupado.ToString();
            this.Mostrar();

            return 1;
        }




        protected override void OnInitialized(EventArgs e) { 
            
            base.OnInitialized(e);
            RecreateGridCells();
            
        } 
    } 
}