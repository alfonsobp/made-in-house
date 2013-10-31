using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MadeInHouse.Dictionary
{
    class DynamicGrid : Grid
    {
        public static readonly DependencyProperty NumColumnsProperty =
           DependencyProperty.Register("NumColumns", typeof(Int32), 
           typeof(DynamicGrid),new PropertyMetadata(0,new PropertyChangedCallback(OnNumRowsOrColumnsChanged)) );

        public Int32 NumColumns
        {
            get { return (Int32)GetValue(NumColumnsProperty); }
            set { SetValue(NumColumnsProperty, value); }
        }


        public static readonly DependencyProperty NumRowsProperty =
            DependencyProperty.Register("NumRows", typeof(Int32),
            typeof(DynamicGrid),new PropertyMetadata(0,new PropertyChangedCallback(OnNumRowsOrColumnsChanged)));

        public Int32 NumRows
        {
            get { return (Int32)GetValue(NumRowsProperty); }
            set { SetValue(NumRowsProperty, value); }
        }

        static void OnNumRowsOrColumnsChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            ((DynamicGrid)sender).RecreateGridCells();
        }


        public void RecreateGridCells()
        {
            int numRows = NumRows;
            int currentNumRows = RowDefinitions.Count;

            int currentNumCols = ColumnDefinitions.Count;
            int numCols = NumColumns;

            this.Children.Clear();
                
            while (numRows > currentNumRows)
            {
                RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                currentNumRows++;
            }

            while (numRows < currentNumRows)
            {
                currentNumRows--;
                RowDefinitions.RemoveAt(currentNumRows);
            }

            


            while (numCols > currentNumCols)
            {

                ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                currentNumCols++;
            }

            while (numCols < currentNumCols)
            {
                currentNumCols--;
                ColumnDefinitions.RemoveAt(currentNumCols);
                
            }
            for (int i = 0; i < numCols;i++)
                for (int j = 0; j < numRows; j++)
                {
                        Button btnTest = new Button();
                        btnTest.Name = "Button" + i + j; //X,Y Columna,Fila
                        btnTest.Content = "Button" + i + j;

                        Grid.SetRow(btnTest, j);
                        Grid.SetColumn(btnTest, i);
                        this.Children.Add(btnTest);

                  
                }

            UpdateLayout();

        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            RecreateGridCells();
        }




    }
}
