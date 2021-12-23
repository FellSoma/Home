using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RegistrY
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            mainTreeView.Items.Clear();

            // открыть ветку в определённом разделе
            RegistryKey[] baseKey = new RegistryKey[]
                {
                Registry.LocalMachine,
                Registry.CurrentUser,
                Registry.ClassesRoot,
                Registry.CurrentConfig,
//                    Registry.DynData,
                Registry.Users,
                Registry.PerformanceData
                };

            // получить 
            foreach (RegistryKey itemKey in baseKey)
            {
                TreeViewItem item = new TreeViewItem();

                // назначить обработчик разворачивания узла дерева
                item.Expanded += Item_Expanded;

                // сохранить информацию о диске в пункте дерева
                item.Tag = itemKey;

                // текст пункта
                item.Header = itemKey.Name;

                // добавить пустой элемент для возможности развернуть узел
                item.Items.Add("*");

                mainTreeView.Items.Add(item);
            }
        }
        RegistryKey current;
        RegistryKey truePath;
        RegistryKey firstRoot;
        string secondPath;



        private void CreateDir(object sender, RoutedEventArgs e)
        {
            if (current == null || PathREG.Text == "")
            {
                MessageBox.Show("1. Заполните поля ввода \n 2. Откройти один из родительских католог ");
            }
            else
            {
                try
                {
                    secondPath = SerchPath(current.Name);
                    truePath = firstRoot.OpenSubKey(secondPath, true);
                    truePath.CreateSubKey(PathREG.Text);
                    MessageBox.Show("Коталог создан");
                }
                catch (Exception)
                {
                    MessageBox.Show("Нет прав к этой ветке");

                }
            }
        }

        private void CreateKey(object sender, RoutedEventArgs e)
        {

            if (current == null || KeyName.Text == "" || KeyContent.Text == "")
            {
                MessageBox.Show("1. Заполните поля ввода \n 2. Откройти один из родительских католог ");
            }
            else
            {
                try
                {

                    secondPath = SerchPath(current.Name);
                    truePath = firstRoot.OpenSubKey(secondPath, true);
                    truePath.SetValue(Convert.ToString(KeyName.Text), Convert.ToString(KeyContent.Text));
                    truePath.Close();

                    MessageBox.Show("Ключ создан");

                }
                catch (Exception)
                {

                    MessageBox.Show("Ключ не создан");
                }
            }
        }

        private void DeeteDir(object sender, RoutedEventArgs e)
        {
            if (current == null || PathREG.Text == "")
            {
                MessageBox.Show("1. Заполните поля ввода \n 2. Откройти один из родительских католог ");
            }
            else
            {
                try
                {
                    secondPath = SerchPath(current.Name);
                    truePath = firstRoot.OpenSubKey(secondPath, true);
                    truePath.DeleteSubKey(Convert.ToString(PathREG.Text));

                    MessageBox.Show("Каталог удалён");

                }
                catch (Exception)
                {

                    MessageBox.Show("1. Каталог не пустой. \n 2. Каталога не существует. ");
                }
            }

        }

        private void DeleteKey(object sender, RoutedEventArgs e, bool update = false)
        {
            if (current == null)
            {
                MessageBox.Show("1. Заполните поля ввода \n 2. Откройти один из родительских католог ");
            }
            else
            {
                try
                {
                    secondPath = SerchPath(current.Name);
                    truePath = firstRoot.OpenSubKey(secondPath, true);

                    if (update == false)
                    {
                       
                        
                        truePath.DeleteValue(Convert.ToString(KeyName.Text));
                        MessageBox.Show("Ключ удалён");

                    }
                    else
                    {
                        if (LastNameKey.Text == "")
                            MessageBox.Show(" Заполните поле ввода <<Старое имя ключа>> ");
                        truePath.DeleteValue(Convert.ToString(LastNameKey.Text));
                        
                    }
                }
                catch (Exception)
                {
                    if (update == false)
                        MessageBox.Show("1. Заполните поле ввода <<Имя ключа>>.\n 2. Ключа нет.");
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            mainTreeView.Items.Clear();

            // открыть ветку в определённом разделе
            RegistryKey[] baseKey = new RegistryKey[]
                {
                Registry.LocalMachine,
                Registry.CurrentUser,
                Registry.ClassesRoot,
                Registry.CurrentConfig,
//                    Registry.DynData,
                Registry.Users,
                Registry.PerformanceData
                };

            // получить 
            foreach (RegistryKey itemKey in baseKey)
            {
                TreeViewItem item = new TreeViewItem();

                // назначить обработчик разворачивания узла дерева
                item.Expanded += Item_Expanded;

                // сохранить информацию о диске в пункте дерева
                item.Tag = itemKey;

                // текст пункта
                item.Header = itemKey.Name;

                // добавить пустой элемент для возможности развернуть узел
                item.Items.Add("*");

                mainTreeView.Items.Add(item);
            }
        }
        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            // получить ссылку на разворачиваемый пункт
            TreeViewItem parentItem = (TreeViewItem)e.OriginalSource;

            // очистить старые дочерние элементы
            parentItem.Items.Clear();


            if (firstRoot != null)
            {
            }
            else
                firstRoot = null;
            current = null;
            if (parentItem.Tag is RegistryKey)
            {
                // получить 
                current = (RegistryKey)parentItem.Tag;
                if (firstRoot != null)
                {
                }
                else
                    firstRoot = (RegistryKey)parentItem.Tag;
            }

            try
            {
                // получить подпапки текущей папки
                foreach (string subDir in current?.GetSubKeyNames())
                {
                    // создать для каждой подпапки отдельный узел дерева
                    TreeViewItem newItem = new TreeViewItem();
                    RegistryKey new_subkey = current.OpenSubKey(subDir, false);
                    newItem.Tag = new_subkey;
                    newItem.Header = new_subkey.Name.Replace(current.Name, "");
                    newItem.Items.Add("*");

                    //                    // включение поддержки Drag-n-Drop
                    //                    newItem.AllowDrop = true;
                    //                    newItem.DragEnter += NewItem_DragEnter;
                    //                    newItem.Drop += NewItem_Drop;


                    parentItem.Items.Add(newItem);
                }
            }
            catch
            { }
        }

        string SerchPath(string cuerrentPath)
        {
            string fullPath;
            fullPath = "";
            string[] array = cuerrentPath.Split('\\');
            for (int i = 1; i < array.Length; i++)
            {
                fullPath = fullPath + array[i] + '\\';

            }
            return fullPath;
            
        }

        private void UpdateKey(object sender, RoutedEventArgs e)
        {

            DeleteKey(sender, e, true);
            if (current == null || NewNameKey.Text == "")
            {
                MessageBox.Show("1. Заполните поля ввода \n 2. Откройти один из родительских католог ");
            }
            else
            {
                try
                {
                    secondPath = SerchPath(current.Name);
                    truePath = firstRoot.OpenSubKey(secondPath, true);
                    if (NewContentKey == null)
                    {
                        string newContentKey = truePath.GetValue(NewNameKey.Text).ToString();
                        truePath.SetValue(Convert.ToString(NewNameKey.Text), Convert.ToString(newContentKey));
                        truePath.Close();
                        MessageBox.Show("Ключ изменён");
                    }
                    else
                    {
                        truePath.SetValue(Convert.ToString(NewNameKey.Text), Convert.ToString(NewContentKey.Text));
                        truePath.Close();
                        MessageBox.Show("Ключ изменён");
                    }


                }
                catch (Exception)
                {

                    MessageBox.Show("Ключ не изменён");
                }
            }

        }

       

        private void Regedit(object sender, RoutedEventArgs e)
        {
            Process.Start("Regedit");


        }

        private void DeleteKey(object sender, RoutedEventArgs e)
        {
            DeleteKey(sender, e, false);
        }
    }
}
