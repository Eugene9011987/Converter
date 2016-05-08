using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private double curseOfDollar;
        private double curseOfEuro;
        private double curseOfRubl;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "1";

            string s = "";// строка с записью по последней дате = monthCalendar1.SelectionEnd.Day.ToString() + "." + monthCalendar1.SelectionEnd.Month.ToString() + "." + monthCalendar1.SelectionEnd.Year.ToString();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Converter.xml");
            DateTime maxDate = DateTime.MinValue;

            //определение последней даты с записью курсов
            foreach (XmlNode task in xmlDoc.DocumentElement.ChildNodes)
            {
                DateTime varDate = DateTime.Parse(task.Attributes.GetNamedItem("dt").Value);
                monthCalendar1.AddBoldedDate(varDate);
                if (varDate >= maxDate)
                {
                    maxDate = varDate;
                    s = maxDate.Day.ToString() + "." + maxDate.Month.ToString() + "." + maxDate.Year.ToString();
                }
            }

            // запись в поля курсов по последней дате записи
            foreach (XmlNode task in xmlDoc.DocumentElement.ChildNodes)
            {
                if (task.Attributes.GetNamedItem("dt").Value == s)
                {
                    foreach (XmlNode param in task.ChildNodes)
                    {
                        if (param.Name == "usd")
                        {
                            s+= Environment.NewLine + "USD: " + param.InnerText;
                            txtUSD.Text = param.InnerText;
                        }
                        if (param.Name == "eur")
                        {
                            s += Environment.NewLine + "EUR: " + param.InnerText;
                            txtEUR.Text = param.InnerText;
                        }
                        if (param.Name == "rub")
                        {
                            s += Environment.NewLine + "RUB: " + param.InnerText;
                            txtRUB.Text = param.InnerText;
                        }

                    }
                } 
            }
            richTextBox1.Text = s;
            radioButton3.Checked = true;
        }

        // функции конвертирования и вывода в метки результатов
        private void grn()
        {
            if (!textBox1.Text.Equals(""))
            {
                label1.Text = "";
                label2.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) / curseOfRubl);
                label3.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) / curseOfDollar);
                label4.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) / curseOfEuro);
            }
        }
        private void rub()
        {
            if (!textBox1.Text.Equals(""))
            {
                label2.Text = "";
                label1.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfRubl);
                label3.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfRubl / curseOfDollar);
                label4.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfRubl / curseOfEuro);
            }
        }
        private void dollar()
        {
            if (!textBox1.Text.Equals(""))
            {
                label3.Text = "";
                label1.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfDollar);
                label2.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfDollar / curseOfRubl);
                label4.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfDollar / curseOfEuro);
            }
        }
        private void euro()
        {
            if (!textBox1.Text.Equals(""))
            {
                label4.Text = "";
                label1.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfEuro);
                label2.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfEuro / curseOfRubl);
                label3.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * curseOfEuro / curseOfDollar);
            }
        }

        // кнопка конвертировать
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                double a = Convert.ToDouble(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный ввод");
                textBox1.Text = "";
                return;
            }

            double us;
            double eu;
            double ru;

            try
            {
                us = Convert.ToDouble(txtUSD.Text.Replace(',', '.'));
                eu = Convert.ToDouble(txtEUR.Text.Replace(',', '.'));
                ru = Convert.ToDouble(txtRUB.Text.Replace(',', '.'));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректный ввод в одном из полей курсов валют");
                return;
            }

            curseOfDollar = us;
            curseOfEuro = eu;
            curseOfRubl = ru;

            if (!textBox1.Text.Equals(""))
            {
                if (radioButton1.Checked)
                {
                    grn();
                }
                if (radioButton2.Checked)
                {
                    rub();
                }
                if (radioButton3.Checked)
                {
                    dollar();
                }
                if (radioButton4.Checked)
                {
                    euro();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // создание  XML файла
            ////<?xml version = "1.0" encoding = "iso-8859-1"?>
            //XmlTextWriter textWritter = new XmlTextWriter("Converter.xml", Encoding.UTF8);
            //textWritter.Formatting = Formatting.Indented;
            //textWritter.IndentChar = '\t';
            //textWritter.Indentation = 1;

            //textWritter.WriteStartDocument();
            //textWritter.WriteStartElement("converter");
            //textWritter.WriteStartElement("date");

            //textWritter.WriteStartAttribute("dt");
            //textWritter.WriteString(DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString());
            //textWritter.WriteEndAttribute();

            //textWritter.WriteStartElement("usd");
            //textWritter.WriteString("21.56");
            //textWritter.WriteEndElement();

            //textWritter.WriteStartElement("eur");
            //textWritter.WriteString("23.34");
            //textWritter.WriteEndElement();

            //textWritter.WriteStartElement("rub");
            //textWritter.WriteString("0.37");
            //textWritter.WriteEndElement();

            //textWritter.WriteEndElement();
            //textWritter.WriteEndElement();
            //textWritter.Close();

            string s = monthCalendar1.SelectionEnd.Day.ToString() + "." + monthCalendar1.SelectionEnd.Month.ToString() + "." + monthCalendar1.SelectionEnd.Year.ToString();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Converter.xml");

            // чтение XML файла

            // удаление записи, с одинаковой датой
            foreach (XmlNode task in xmlDoc.DocumentElement.ChildNodes)
            {
                if (task.Attributes.GetNamedItem("dt").Value == s)
                {
                    var navigator = task.CreateNavigator();
                    navigator.DeleteSelf();
                    //task.RemoveAll();
                    xmlDoc.Save("Converter.xml");
                    break;
                }
            }

            //  запись в XML файд
            XmlDocument document = new XmlDocument();
            document.Load("Converter.xml");

            XmlNode element = document.CreateElement("date");
            document.DocumentElement.AppendChild(element); // указываем родителя
            XmlAttribute attribute = document.CreateAttribute("dt"); // создаём атрибут
            attribute.Value = monthCalendar1.SelectionEnd.Day.ToString() + "." + monthCalendar1.SelectionEnd.Month.ToString() + "." + monthCalendar1.SelectionEnd.Year.ToString(); // устанавливаем значение атрибута
            element.Attributes.Append(attribute); // добавляем атрибут

            XmlNode subElement1 = document.CreateElement("usd"); // даём имя
            subElement1.InnerText = txtUSD.Text; // и значение
            element.AppendChild(subElement1); // и указываем кому принадлежит

            XmlNode subElement2 = document.CreateElement("eur"); // даём имя
            subElement2.InnerText = txtEUR.Text; // и значение
            element.AppendChild(subElement2); // и указываем кому принадлежит

            XmlNode subElement3 = document.CreateElement("rub"); // даём имя
            subElement3.InnerText = txtRUB.Text; // и значение
            element.AppendChild(subElement3); // и указываем кому принадлежит

            document.Save("Converter.xml");
            monthCalendar1.AddBoldedDate(DateTime.Parse(s));
            monthCalendar1.UpdateBoldedDates();
            monthCalendar1_DateSelected(null, null);
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            string s = monthCalendar1.SelectionEnd.Day.ToString() + "." + monthCalendar1.SelectionEnd.Month.ToString() + "." + monthCalendar1.SelectionEnd.Year.ToString();
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Converter.xml");
            string buff = "";                        // строка для вывода информации в текстовое поле

            // чтение XML файла
            foreach (XmlNode task in xmlDoc.DocumentElement.ChildNodes)
            {
               
                if (task.Attributes.GetNamedItem("dt").Value == s)
                {
                    foreach (XmlNode param in task.ChildNodes)
                    {
                        if (param.Name == "usd")
                        {
                            buff += Environment.NewLine + "USD: " + param.InnerText;
                            txtUSD.Text = param.InnerText;
                        }
                        if (param.Name == "eur")
                        {
                            buff += Environment.NewLine + "EUR: " + param.InnerText;
                            txtEUR.Text = param.InnerText;
                        }
                        if (param.Name == "rub")
                        {
                            buff += Environment.NewLine + "RUB: " + param.InnerText;
                            txtRUB.Text = param.InnerText;
                        }

                    }
                }
            }
            richTextBox1.Text = s + buff;
            button1_Click(null, null);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    } 
} 
