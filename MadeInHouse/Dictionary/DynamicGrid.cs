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

            while (numRows > currentNumRows)
            {
                Button btnTest = new Button();
                btnTest.Name = "Button"+currentNumRows;
                btnTest.Content = "Button"+currentNumRows;
                Grid.SetRow(btnTest, currentNumRows);
                Grid.SetColumn(btnTest, currentNumRows);
                this.Children.Add(btnTest);
                RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                
                currentNumRows++;
            }

            while (numRows < currentNumRows)
            {
                currentNumRows--;
                RowDefinitions.RemoveAt(currentNumRows);
            }

            int numCols = NumColumns;
            int currentNumCols = ColumnDefinitions.Count;

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

            UpdateLayout();

        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            RecreateGridCells();
        }




    }
}
