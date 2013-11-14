using System;
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

        public void Mostar(List<Ubicacion> lu, int idProducto = -1)
        {

            BrushConverter conv = new BrushConverter();
            SolidColorBrush colorname = conv.ConvertFromString("White") as SolidColorBrush; ;

            columna = lu;

            for (int j = 0; j < lu.Count; j++)
            {
                if (lu[j].IdProducto == idProducto)
                {
                    colorname = conv.ConvertFromString("Fuchsia") as SolidColorBrush;
                }
                else
                {
                    colorname = conv.ConvertFromString("White") as SolidColorBrush;
                }
                (this.Children[lu[j].CordZ] as Button).Background = colorname;
                (this.Children[lu[j].CordZ] as Button).Content = lu[j].IdProducto;

            }
        }

        

        public void onClickChange(object sender, RoutedEventArgs e) {

            int X = Int16.Parse((sender as Button).Name.Substring(1, (sender as Button).Name.IndexOf("X") - 1));
            int Y = Int16.Parse((sender as Button).Name.Substring((sender as Button).Name.IndexOf("Y") + 1));

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


                if (Tipo == 1)
                {
                    columna = Ubicaciones[X][Y];
                    /*BrushConverter conv = new BrushConverter();
                    SolidColorBrush colorname = conv.ConvertFromString("Black") as SolidColorBrush;
                    (sender as Button).Background = colorname;*/
                }
                if (Tipo == 2)
                {
                    selectedUbicacion = columna[X];
                    CantActual = columna[X].Cantidad.ToString();
                    VolOcu = columna[X].VolOcupado.ToString();
                    if (int.Parse(VolOcu) > 0) Enable = false;
                    else Enable = true;
                }
            }
            else if (Accion == 3)
            {


            }
        }

        private Ubicacion selectedUbicacion;

        public List<Ubicacion> columna;

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

        public void UbicarProducto(int idProducto, int limpia)
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


            for (int i = 0; i < this.lstZonas.Count; i++)
            {
                for (int j = 0; j < this.lstZonas[i].LstUbicaciones.Count; j++)
                {
                   
                    if (this.lstZonas[i].LstUbicaciones[j].IdProducto == idProducto)
                    {

                        (this.Children[this.lstZonas[i].LstUbicaciones[j].CordY + NumColumns * this.lstZonas[i].LstUbicaciones[j].CordX] as Button).Foreground = colorname;
                        (this.Children[this.lstZonas[i].LstUbicaciones[j].CordY + NumColumns * this.lstZonas[i].LstUbicaciones[j].CordX] as Button).Content = "X";
                        (this.Children[this.lstZonas[i].LstUbicaciones[j].CordY + NumColumns * this.lstZonas[i].LstUbicaciones[j].CordX] as Button).FontSize = 25;
                        
                    }
                   
                }
            }

        }

        public void AgregarProductos(int cant, int vol, int idProducto, List<TipoZona> zonas)
        {
            /*estos dos valores influyen en el otro GRID*/
            TipoZona tz= zonas.Find(x => x.IdTipoZona == selectedUbicacion.IdTipoZona);
            Ubicacion ub =tz.LstUbicaciones.Find(x => x.IdUbicacion == selectedUbicacion.IdUbicacion);

            int volAux;
            if (selectedUbicacion.VolOcupado != 0)
            {
             
                volAux =selectedUbicacion.VolOcupado + cant * selectedUbicacion.VolOcupado / selectedUbicacion.Cantidad;
                if (volAux > 100)
                {
                    System.Windows.MessageBox.Show("No se pudo ingresar , se superó la capacidad");
                    return;
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

            selectedUbicacion.Cantidad += cant;
            
            ub.Cantidad = selectedUbicacion.Cantidad;
            ub.VolOcupado = selectedUbicacion.VolOcupado;
            ub.IdProducto = selectedUbicacion.IdProducto;

            CantActual = selectedUbicacion.Cantidad.ToString();
            VolOcu = selectedUbicacion.VolOcupado.ToString();
            this.Mostar(columna,selectedUbicacion.IdProducto);
           
            
        }

        protected override void OnInitialized(EventArgs e) { 
            
            base.OnInitialized(e);
            RecreateGridCells();
            
        } 
    } 
}