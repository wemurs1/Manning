using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BinaryNode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        SortedBinaryNode<int> Root = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set focus to the TextBox.
            ValueTextBox.Focus();

            // Make a new sentinel root node.
            Root = ...

            // Run some tests.
            // Comment this out to start with an empty tree.
            RunTests();

            // Display the tree.
            DrawTree();
        }

        // Build a test tree.
        private void RunTests()
        {
            ...

            // Find each node.
            ...

            MessageBox.Show("Found all nodes");
        }

        // Draw the tree.
        private void DrawTree()
        {
            // Remove all shapes from the Canvas.
            mainCanvas.Children.Clear();

            // Draw the tree.
            Root.ArrangeAndDrawSubtree(mainCanvas, 5, 5);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the entered value.
            int value;
            if (!int.TryParse(ValueTextBox.Text, out value) || (value < 0))
            {
                MessageBox.Show("The value must be a non-negative integer.");
            }
            else
            {
                // Make a node to hold the value.
                ...

                // Try to add the node to the tree.
                try
                {
                    ...
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Redraw the tree.
            DrawTree();

            // Prepare to get the next value.
            ValueTextBox.Focus();
            ValueTextBox.Clear();
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the entered value.
            int value;
            if (!int.TryParse(ValueTextBox.Text, out value))
            {
                MessageBox.Show("The value must be an integer.");
            }
            else
            {
                // Try to find the value.
                ...

                // Display whatever we got.
                if (node == null)
                    MessageBox.Show(string.Format(
                        "The value {0} is not in the tree.", value));
                else
                    MessageBox.Show(string.Format(
                        "Found value {0}.", node.Value));
            }

            // Redraw the tree.
            DrawTree();

            // Prepare to get the next value.
            ValueTextBox.Focus();
            ValueTextBox.Clear();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            // Make a new sentinel root node.
            ...

            // Display the tree.
            DrawTree();

            // Prepare to get the next value.
            ValueTextBox.Focus();
            ValueTextBox.Clear();
        }
    }
}
