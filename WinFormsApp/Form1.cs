using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Xml;
using WinFormsApp.Models;

namespace WinFormsApp
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("bmw");
            comboBox1.Items.Add("skoda");
            comboBox1.Items.Add("vw");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
         string name;
         string age;
         Car car;
        const string path = @"C:\test\test.txt";
        const string path1 = @"C:\test\test.xml";
        const string errorEmpty = "List is empty\n";
        const string errorExist = "File is not exists";
        const string errorChoseFile = "Chose file";
            private void Save_Click(object sender, EventArgs e)
        {
                name = textBox1.Text.Trim();
                age = maskedTextBox1.Text;
            int ageEmp = Convert.ToInt32(age);
            if (String.IsNullOrEmpty(name))
                {

                    MessageBox.Show("Invalid value of name\n");
                }
                else if (String.IsNullOrEmpty(age) || ageEmp< 18 || ageEmp > 99) MessageBox.Show("Invalid value of age\n");
                else if (String.IsNullOrEmpty(comboBox1.Text)) MessageBox.Show("Invalid value of car\n");
                else if (!Enum.TryParse<Car>(comboBox1.Text, true, out car)) MessageBox.Show("Invalid value \n");
                else
                {
              
                    List<Employee> list = new List<Employee>();
                    Employee emp = new Employee(name, ageEmp, car);
                    if (json.Checked)
                    {

                        if (!File.Exists(path))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\test");
                            dirInfo.Create();

                            try
                            {

                                list.Add(emp);
                                string json = FileHelper.FileHelper.ReadJson(list);

                            FileHelper.FileHelper.Write(json);
                            MessageBox.Show("Saved successfully!");
                        }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            string text = File.ReadAllText(@"C:\test\test.txt");
                            if (!String.IsNullOrEmpty(text))
                            {
                                try
                                {

                                    list = JsonSerializer.Deserialize<List<Employee>>(text);
                                    list.Add(emp);
                                    string json = FileHelper.FileHelper.ReadJson(list);

                                    FileHelper.FileHelper.Write(json);
                                MessageBox.Show("Saved successfully!");

                            }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            else
                            {

                                list.Add(emp);
                                string json = FileHelper.FileHelper.ReadJson(list);
                                FileHelper.FileHelper.Write(json);
                            MessageBox.Show("Saved successfully!");
                        }
                        }

                    }
                    else if (xml.Checked)
                    {
                        if (!File.Exists(path1))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\test");
                            dirInfo.Create();
                            XmlDocument xdoc = new XmlDocument();
                            XmlElement employees = xdoc.CreateElement("employees");
                            xdoc.AppendChild(employees);
                            XmlElement user = xdoc.CreateElement("user");

                            XmlElement name = xdoc.CreateElement("Name");
                            XmlText nameText = xdoc.CreateTextNode(emp.Name);
                            XmlElement age = xdoc.CreateElement("Age");
                            XmlText ageText = xdoc.CreateTextNode(emp.Age.ToString());
                            XmlElement carEmp = xdoc.CreateElement("Car");
                            XmlText carText = xdoc.CreateTextNode(emp.Car.ToString());
                            employees.AppendChild(user);
                            user.AppendChild(name);
                            user.AppendChild(age);
                            user.AppendChild(carEmp);
                            name.AppendChild(nameText);
                            age.AppendChild(ageText);
                            carEmp.AppendChild(carText);
                            xdoc.Save(@"C:\test\test.xml");

                        }
                        else
                        {
                            try
                            {

                                XmlDocument xdoc = new XmlDocument();
                                xdoc.Load(@"C:\test\test.xml");
                                XmlElement xRoot = xdoc.DocumentElement;

                                XmlElement user = xdoc.CreateElement("user");

                                XmlElement name = xdoc.CreateElement("Name");
                                XmlText nameText = xdoc.CreateTextNode(emp.Name);
                                XmlElement age = xdoc.CreateElement("Age");
                                XmlText ageText = xdoc.CreateTextNode(emp.Age.ToString());
                                XmlElement carEmp = xdoc.CreateElement("Car");
                                XmlText carText = xdoc.CreateTextNode(emp.Car.ToString());

                                user.AppendChild(name);
                                user.AppendChild(age);
                                user.AppendChild(carEmp);
                                name.AppendChild(nameText);
                                age.AppendChild(ageText);
                                carEmp.AppendChild(carText);
                                xRoot.AppendChild(user);
                                xdoc.Save(@"C:\test\test.xml");
                            MessageBox.Show("Saved successfully!");
                        }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else MessageBox.Show(errorChoseFile);

                }
            
        }

        private void View_Click(object sender, EventArgs e)
        {
                if (json.Checked)
                {
                    if (File.Exists(path))
                    {
                        try
                        {

                            string text = File.ReadAllText(path);
                            if (!String.IsNullOrEmpty(text))
                            {
                                List<Employee> list = FileHelper.FileHelper.ReadList(text);

                                string users = "";
                                for (int i = 0; i < list.Count; i++)
                                {
                                    users += list[i].Name + "\t" + list[i].Age + "\t" + list[i].Car + "\n";

                                }
                                if (!String.IsNullOrEmpty(users)) MessageBox.Show(users);
                                else MessageBox.Show("User is not found");


                            }
                            else MessageBox.Show(errorEmpty);


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        MessageBox.Show(errorExist);
                    }
                }
                else if (xml.Checked)
                {
                    if (File.Exists(path1))
                    {
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.Load(@"C:\test\test.xml");
                        XmlElement xRoot = xdoc.DocumentElement;
                        if (xRoot != null)
                        {
                            XmlNodeList childnodes = xRoot.SelectNodes("//user");
                            string users = "";
                            for (int i = 0; i < childnodes.Count; i++)
                            {
                                users += childnodes[i].SelectSingleNode("Name").InnerText + "\t" + childnodes[i].SelectSingleNode("Age").InnerText + "\t" + childnodes[i].SelectSingleNode("Car").InnerText + "\n";

                            }
                            if (!String.IsNullOrEmpty(users)) MessageBox.Show(users);
                            else MessageBox.Show("User is not found");
                        }
                        else MessageBox.Show(errorEmpty);
                    }
                    else
                    {
                        MessageBox.Show(errorExist);
                    }
                }
                else
                {
                    MessageBox.Show(errorChoseFile);
                }


            
        }

        private void Find_Click(object sender, EventArgs e)
        {
                string name = textBox1.Text;
                string age = maskedTextBox1.Text;

                string car = comboBox1.Text;
               
                if (json.Checked)
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            string text = File.ReadAllText(path);
                            if (!String.IsNullOrEmpty(text))
                            {
                                List<Employee> list = FileHelper.FileHelper.ReadList(text);

                                string users = "";
                                for (int i = 0; i < list.Count; i++)
                                {
                                    if (name == list[i].Name || age == list[i].Age.ToString() || car == list[i].Car.ToString())
                                        users += list[i].Name + "\t" + list[i].Age + "\t" + list[i].Car + "\n";

                                }
                                if (!String.IsNullOrEmpty(users)) MessageBox.Show(users);
                                else MessageBox.Show("User is not found");

                            }
                            else MessageBox.Show(errorEmpty);



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        MessageBox.Show(errorExist);
                    }
                }
                else if (xml.Checked)
                {
                    if (File.Exists(path1))
                    {
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.Load(@"C:\test\test.xml");
                        XmlElement xRoot = xdoc.DocumentElement;
                        if (xRoot != null)
                        {
                            XmlNodeList childnodes = xRoot.SelectNodes("//user");
                            string users = "";
                            for (int i = 0; i < childnodes.Count; i++)
                            {
                                if (name == childnodes[i].SelectSingleNode("Name").InnerText || age == childnodes[i].SelectSingleNode("Age").InnerText || car == childnodes[i].SelectSingleNode("Car").InnerText)

                                    users += childnodes[i].SelectSingleNode("Name").InnerText + "\t" + childnodes[i].SelectSingleNode("Age").InnerText + "\t" + childnodes[i].SelectSingleNode("Car").InnerText + "\n";

                            }
                            if (!String.IsNullOrEmpty(users)) MessageBox.Show(users);
                            else MessageBox.Show("User is not found");
                        }
                        else MessageBox.Show(errorEmpty);

                    }
                    else
                    {
                        MessageBox.Show(errorExist);
                    }
                }
                else
                {
                    MessageBox.Show(errorChoseFile);
                }
            
        }

        private void Delete_Click(object sender, EventArgs e)
        {
                if (json.Checked)
                {
                    if (File.Exists(path))
                    {
                        try
                        {

                            string text = File.ReadAllText(path);
                            if (!String.IsNullOrEmpty(text))
                            {
                                List<Employee> list = FileHelper.FileHelper.ReadList(text);

                                List<Employee> newList = new List<Employee>();
                                foreach (var i in list)
                                {

                                    if (textBox1.Text != i.Name)
                                    {
                                        newList.Add(i);
                                    }
                                    string json = FileHelper.FileHelper.ReadJson(newList);
                                    FileHelper.FileHelper.Write(json);


                                }
                                MessageBox.Show("Deleted successfully!");

                            }
                            else MessageBox.Show(errorEmpty);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        MessageBox.Show(errorExist);
                    }
                }
                else if (xml.Checked)
                {
                    if (File.Exists(path1))
                    {
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.Load(@"C:\test\test.xml");
                        XmlElement xRoot = xdoc.DocumentElement;
                        if (xRoot != null)
                        {
                            XmlNodeList childnodes = xRoot.SelectNodes("//user");

                            for (int i = 0; i < childnodes.Count; i++)
                            {
                                if (textBox1.Text == childnodes[i].SelectSingleNode("Name").InnerText)
                                    xRoot.RemoveChild(childnodes[i]);


                            }
                            xdoc.Save(@"C:\test\test.xml");
                            MessageBox.Show("Deleted successfully!");
                        }
                        else MessageBox.Show(errorEmpty);
                    }
                    else
                    {
                        MessageBox.Show(errorExist);
                    }
                }
                else
                {
                    MessageBox.Show(errorChoseFile);
                }
            
        }
    }
   
}
