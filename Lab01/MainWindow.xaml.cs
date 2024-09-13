using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab01
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            AddNewTab();
        }

        private void AddNewTab(string filePath = null, string fileContent = "")
        {
            var newTab = new TabItem();
            var tabHeader = new StackPanel { Orientation = Orientation.Horizontal };

       
            var headerText = new TextBlock { Text = filePath ?? "Новий файл", Margin = new Thickness(0, 0, 10, 0) };
            tabHeader.Children.Add(headerText);

           
            var closeButton = new Button { Content = "X", Width = 20, Height = 20 };
            closeButton.Click += (s, e) => CloseTab(newTab);
            tabHeader.Children.Add(closeButton);

            newTab.Header = tabHeader;

         
            var textBox = new TextBox
            {
                AcceptsReturn = true,
                AcceptsTab = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Background = Brushes.White,
                Text = fileContent,
                ContextMenu = FindResource("TextBoxContextMenu") as ContextMenu
            };

            newTab.Content = textBox;
            Tabs.Items.Add(newTab);
            Tabs.SelectedItem = newTab;
        }

        private void CloseTab(TabItem tabItem)
        {
            Tabs.Items.Remove(tabItem);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);
                AddNewTab(filePath, fileContent);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = Tabs.SelectedItem as TabItem;
            if (selectedTab == null) return;

            TextBox textBox = selectedTab.Content as TextBox;
            string content = textBox.Text;

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, content);
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = Tabs.SelectedItem as TabItem;
            if (selectedTab == null) return;

            TextBox textBox = selectedTab.Content as TextBox;
            if (textBox.CanUndo)
            {
                textBox.Undo();
            }
        }

        private void ChangeBackground(Brush color)
        {
            foreach (TabItem tab in Tabs.Items)
            {
                TextBox textBox = tab.Content as TextBox;
                if (textBox != null)
                {
                    textBox.Background = color;
                }
            }
        }

        private void ChangeBackgroundToYellow_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackground(Brushes.LightYellow);
        }

        private void ChangeBackgroundToBlue_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackground(Brushes.LightBlue);
        }

        private void ChangeBackgroundToGreen_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackground(Brushes.LightGreen);
        }

        private void ChangeBackgroundToPink_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackground(Brushes.LightPink);
        }

        private void ChangeBackgroundToWhite_Click(object sender, RoutedEventArgs e)
        {
            ChangeBackground(Brushes.White);
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = Tabs.SelectedItem as TabItem;
            if (selectedTab == null) return;

            TextBox textBox = selectedTab.Content as TextBox;
            if (textBox != null)
            {
                textBox.Cut();
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = Tabs.SelectedItem as TabItem;
            if (selectedTab == null) return;

            TextBox textBox = selectedTab.Content as TextBox;
            if (textBox != null)
            {
                textBox.Copy();
            }
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = Tabs.SelectedItem as TabItem;
            if (selectedTab == null) return;

            TextBox textBox = selectedTab.Content as TextBox;
            if (textBox != null)
            {
                textBox.Paste();
            }
        }

        private void NewTabContext_Click(object sender, RoutedEventArgs e)
        {
            AddNewTab();
        }

        private void CloseTabContext_Click(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = Tabs.SelectedItem as TabItem;
            if (selectedTab != null)
            {
                CloseTab(selectedTab);
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }
    }
}
