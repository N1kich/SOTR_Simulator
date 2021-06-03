using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;


namespace SOTR_Simulator
{
    unsafe public partial class Form1 : Form
    {
        string path = "SOTR_Simulator.exe";
        Random randomTempretureRange;
        Random randomGroupRange;
        Dictionary<string, double> ConstForRandomGen;
        Dictionary<string, string> ControlCommands;
        //const double minimumTempreture = -180.00;
        //const double maximumTempreture = 190.00;
        struct DiagnosticADT_CarcassB
        {
            public DiagnosticADT_CarcassB(double maxTempPanel, double minTempPanel, double averageTempPanel, int quantityOfNotWorkingSensors)
            {
                this.maxTempPanel = maxTempPanel;
                this.minTempPanel = minTempPanel;
                this.averageTempPanel = averageTempPanel;
                this.quantityOfNotWorkingSensors = quantityOfNotWorkingSensors;
            }
            public double maxTempPanel { get; set; }
            public double minTempPanel { get; set; }
            public double averageTempPanel { get; set; }
            public int quantityOfNotWorkingSensors { get; set; }
        }
        struct BKUPI_AFAR
        {
            public BKUPI_AFAR(int flag, int quantityIterations, int quantitySensors)
            {
                this.flag = flag;
                quantityOfIterations = quantityIterations;
                quantityOfNotWorkingSensors = quantitySensors;
            }
            public int flag { get; set; }
            public int quantityOfIterations { get; set; }
            public int quantityOfNotWorkingSensors { get; set; }
        }
        struct AFAR_AMT
        {
            public AFAR_AMT(int flag, int quantity)
            {
                Flag = flag;
                QuantityOfNotWorking = quantity;

            }
            public int Flag { get; set; }
            public int QuantityOfNotWorking { get; set; }
        }
        public Form1()
        {
            
            InitializeComponent();
            FillDictionaries();
        }
        int k;
        double[] PanelSensors;
        int[] WorkablePanelSensors;
        double[] AverageTempGroup;
        string DescriptionDataBox = "Введите пару значений в формате:" + Environment.NewLine + "ЗНАЧЕНИЕ СТАТУС."
            + Environment.NewLine + "Пример: " + Environment.NewLine + "174 1" 
            + Environment.NewLine + "24 3" + Environment.NewLine + "45 2" +
            Environment.NewLine + " и т.д." + Environment.NewLine + "Или выберите опцию 'Сгенирировать входные данные' " +
            Environment.NewLine;

        
        [DllImport("..\\Debug\\TestLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern DiagnosticADT_CarcassB Diagnostic_CarcassB(DiagnosticADT_CarcassB example, double[] PanelSensors, int[] WorkablePanelSensors, double[] AverageGroupTemp);
        [DllImport("..\\Debug\\TestLibrary.dll", EntryPoint = "DiagnosticBKUPI_AFAR", CallingConvention = CallingConvention.Cdecl)]
        private static extern BKUPI_AFAR DiagnosticBKUPI_AFAR(BKUPI_AFAR example, double[] PanelSensors, int[] WorkableSensors, double[] AverageTemp);
        [DllImport("..\\Debug\\TestLibrary.dll", EntryPoint = "DiagnosticAFARAMT", CallingConvention = CallingConvention.Cdecl)]
        private static extern AFAR_AMT DiagnosticAFARAMT(double[] SensorValue, int[] SensorIndicator, AFAR_AMT example, double[] AverageTemp);
        public void FillArrayFromDataBox(double[] PanelSensors,int[] WorkableSensors, int k)
        {
            string[] ss = inputDataBox.Text.Split('\n',' ');
            int n = ss.Length;
            int flagCurr = 0;
            int flagStat = 0;
            if (2*k!=n)
            {
                Console.WriteLine(n.ToString());
                MessageBox.Show("Кол-во указанных данных датчиков превышает или меньше" +
                    " допустимого кол-ва информации.Смотрите описание алгоритма ", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                for (int i = 0; i < n; i++)
                {
                    if (i%2==0)
                    {
                        PanelSensors[flagCurr] = double.Parse(ss[i]);
                        flagCurr++;
                    }
                    else if (i % 2 == 1)
                    {
                        WorkablePanelSensors[flagStat] = int.Parse(ss[i]);
                        flagStat++;
                    }
                    
                }
                //for (int i = 0; i < k; i++)
                //{
                //    resultBox.Text += PanelSensors[i].ToString() +"\t"+ WorkablePanelSensors[i].ToString() + Environment.NewLine;
                //}
                Console.WriteLine(n.ToString());
            }
        }
        public void FillDictionaries()
        {
            ConstForRandomGen = new Dictionary<string, double>()
            {
                {"CarcassB_Min", -180 },
                {"CarcassB_Max", 190 },
                {"CarcassB_dtADT", 3.5 },
                {"BKUPI_AFAS_Min", -200 },
                {"BKUPI_AFAS_Max", 200 },
                {"BKUPI_AFAS_dtADT", 8.5 },
                {"AFAR_Min", -50},
                {"AFAR_Max", 45 }

            };
            ControlCommands = new Dictionary<string, string>()
            {
                {"OFFH","0111.100Y.YYYX.XXXX" },
                {"ONFD","0111.101Y.YYYX.XXXX" },
                {"ONALDPANEL","1100.0000.0000.0001" },
                {"ONALDBKAFAS","1100.0000.1100.0001" },
                {"ONALDAFAS","1100.0000.1100.0011" },
                {"ONALDAFARBAO","1100.0000.0011.0001" },
                {"STRECVALSENSOR","0101.0101.0101.0101" },
            };
        }
        private void DisplayResults(double[] PanelSensors, int[] WorkableSensors, int k, double[] AverageTempGroup, DiagnosticADT_CarcassB carcassB)
        {
            resultBox.Text = "\tПолучена команда управления на запуск алгоритма - \t"
                + ControlCommands["ONALDPANEL"] + Environment.NewLine;
            resultBox.Text += "\tСостояния датчиков после диагностики" + Environment.NewLine;
            for (int i = 0; i < k; i++)
            {
                progressDiagnostic.PerformStep();
                resultBox.Text += PanelSensors[i].ToString() + "\t" + WorkablePanelSensors[i].ToString() + Environment.NewLine;
            }
            resultBox.Text += "Средняя температура по группам:\t";
            for (int i = 0; i < k/3; i++)
            {
                resultBox.Text += AverageTempGroup[i].ToString() + " ";
            }
            
            
                resultBox.Text += Environment.NewLine + "\tМаксимальная средняя температура панели " + carcassB.maxTempPanel.ToString() +
                Environment.NewLine + "\tМинимальная средняя температура панели " + carcassB.minTempPanel.ToString() +
                Environment.NewLine + "\tСредняя температура панели " + carcassB.averageTempPanel.ToString() +
                Environment.NewLine + "\tКол-во нерабочих датчиков " + carcassB.quantityOfNotWorkingSensors.ToString();
            
            
        }
        private void DisplayResults(double[] PanelSensors, int[] WorkableSensors, int k, double[] AverageTempGroup, BKUPI_AFAR bkupi)
        {
            if (k == 12)
            {
                resultBox.Text = "\tПолучена команда управления на запуск алгоритма - \t"
                + ControlCommands["ONALDBKAFAS"] + Environment.NewLine;
            }
            else
            {
                resultBox.Text = "\tПолучена команда управления на запуск алгоритма - \t"
                + ControlCommands["ONALDAFAS"] + Environment.NewLine;
            }
            resultBox.Text += "\tСостояния датчиков после диагностики" + Environment.NewLine;
            for (int i = 0; i < k; i++)
            {
                resultBox.Text += (PanelSensors[i].ToString() + "\t" + WorkablePanelSensors[i].ToString()) + Environment.NewLine;
            }
            resultBox.Text += "Средняя температура по группам:\t";
            for (int i = 0; i < k / 3; i++)
            {
                resultBox.Text += AverageTempGroup[i].ToString() + " ";
            }
            resultBox.Text += Environment.NewLine + "\tКол-во итераций " + bkupi.quantityOfIterations.ToString() +
                Environment.NewLine + "\tКол-во рабочих датчиков " + bkupi.quantityOfNotWorkingSensors.ToString();
        }
        private void DisplayResults(double[] PanelSensors, int[] WorkableSensors, int k, double[] AverageTempGroup, AFAR_AMT example)
        {
            resultBox.Text = "\tПолучена команда управления на запуск алгоритма - \t"
                + ControlCommands["ONALDAFARBAO"] + Environment.NewLine;

            resultBox.Text += "\tСостояния датчиков после диагностики" + Environment.NewLine;

            for (int i = 0; i < k; i++)
            {
                resultBox.Text += (PanelSensors[i].ToString() + "\t" + WorkablePanelSensors[i].ToString()) + Environment.NewLine;
            }
            resultBox.Text += "Средняя температура по группам:\t";
            for (int i = 0; i < k / 3; i++)
            {
                resultBox.Text += AverageTempGroup[i].ToString() + " ";
            }
            resultBox.Text += Environment.NewLine + "\tКол-во итераций " + example.Flag.ToString() + Environment.NewLine +
                "\tКол-во нерабочих датчиков " + example.QuantityOfNotWorking.ToString();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            if (inputDataBox.Text==DescriptionDataBox)
            {
                MessageBox.Show("Вы не ввели входные параметры, попробуйте снова", "Предупреждение!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else 
            {
                switch (listOfAlgorithms.SelectedIndex)
                {
                    case 0:
                        {
                            if (RandomGenButton.Checked)
                            {
                                k = 9;
                                DiagnosticADT_CarcassB carcassB = new DiagnosticADT_CarcassB();
                                DiagnosticADT_CarcassB result = Diagnostic_CarcassB(carcassB, PanelSensors, WorkablePanelSensors, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k, result.maxTempPanel, result.minTempPanel);
                            }
                            else
                            {
                                k = 9;
                                progressDiagnostic.Step = k;
                                progressDiagnostic.Value += k;
                                PanelSensors = new double[k];
                                WorkablePanelSensors = new int[k];
                                AverageTempGroup = new double[k / 3];
                                FillArrayFromDataBox(PanelSensors, WorkablePanelSensors, k);
                                DiagnosticADT_CarcassB carcassB = new DiagnosticADT_CarcassB();
                                DiagnosticADT_CarcassB result = Diagnostic_CarcassB(carcassB, PanelSensors, WorkablePanelSensors, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k);
                            }
                            
                            break;
                        }
                    case 1:
                        {
                            if(RandomGenButton.Checked)
                            {
                                k = 12;
                                BKUPI_AFAR bkupi = new BKUPI_AFAR(k, 0, 0);
                                BKUPI_AFAR result = DiagnosticBKUPI_AFAR(bkupi, PanelSensors, WorkablePanelSensors, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k);
                            }
                            else
                            {
                                k = 12;
                                progressDiagnostic.Step = k;
                                //progressDiagnostic.Value += k;
                                PanelSensors = new double[k];
                                WorkablePanelSensors = new int[k];
                                AverageTempGroup = new double[k / 3];
                                FillArrayFromDataBox(PanelSensors, WorkablePanelSensors, k);
                                BKUPI_AFAR bkupi = new BKUPI_AFAR(k, 0, 0);
                                BKUPI_AFAR result = DiagnosticBKUPI_AFAR(bkupi, PanelSensors, WorkablePanelSensors, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (RandomGenButton.Checked)
                            {
                                k = 6;
                                BKUPI_AFAR bkupi = new BKUPI_AFAR(k, 0, 0);
                                BKUPI_AFAR result = DiagnosticBKUPI_AFAR(bkupi, PanelSensors, WorkablePanelSensors, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k);
                            }
                            else
                            {
                                k = 6;
                                progressDiagnostic.Step = k;
                                progressDiagnostic.Value += k;
                                PanelSensors = new double[k];
                                WorkablePanelSensors = new int[k];
                                AverageTempGroup = new double[k / 3];
                                FillArrayFromDataBox(PanelSensors, WorkablePanelSensors, k);
                                BKUPI_AFAR bkupi = new BKUPI_AFAR(k, 0, 0);
                                BKUPI_AFAR result = DiagnosticBKUPI_AFAR(bkupi, PanelSensors, WorkablePanelSensors, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k);
                            }
                            
                            break;
                        }
                    case 3:
                        {
                            if (RandomGenButton.Checked)
                            {
                                k = 24;
                                AFAR_AMT afarAMT = new AFAR_AMT(k, 0);
                                AFAR_AMT result = DiagnosticAFARAMT(PanelSensors, WorkablePanelSensors, afarAMT, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k);
                            }
                            else
                            {
                                k = 24;
                                progressDiagnostic.Step = k;
                                progressDiagnostic.Value += k;
                                PanelSensors = new double[k];
                                WorkablePanelSensors = new int[k];
                                AverageTempGroup = new double[k / 3];
                                FillArrayFromDataBox(PanelSensors, WorkablePanelSensors, k);
                                AFAR_AMT afarAMT = new AFAR_AMT(k, 0);
                                AFAR_AMT result = DiagnosticAFARAMT(PanelSensors, WorkablePanelSensors, afarAMT, AverageTempGroup);
                                DisplayResults(PanelSensors, WorkablePanelSensors, k, AverageTempGroup, result);
                                FillProgressBar(k);
                                ControlHeaters(AverageTempGroup, k);
                            }
                            
                            break;
                        }
                    default:
                        break;
                }

                
            }
            
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listOfAlgorithms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listOfAlgorithms.SelectedIndex==0)
            {
                descriptionBox.Text = "АДТ расположены на панелях БI, БII, БIII и БIV группами из трёх датчиков на каждой панели." +
                    " В ходе алгоритма диагностики АДТ определяется исправность датчиков, средняя температура панели," +
                    " а также максимальная и минимальная средняя температура групп АДТ на панелях. Введите 9 значений датчиков панели, а также их статус:" +
                    "'1' - датчик работоспособный, '2' - датчик отложенный, '3' - датчик неработоспособный.";
            }
            else if(listOfAlgorithms.SelectedIndex == 1)
            {
                descriptionBox.Text = "АДТ расположены на приводах антенн по две группы из трёх датчиков на каждом приводе." +
                    " Привод состоит из модулей Y и Z, на модуле установлено по одной группе датчиков соответственно." +
                    " В ходе алгоритма диагностики АДТ определяется исправность датчиков и средняя температура модуля. Введите 12 значений датчиков панели, а также их статус:" +
                    "'1' - датчик работоспособный, '2' - датчик отложенный, '3' - датчик неработоспособный.";
            }
            else if (listOfAlgorithms.SelectedIndex == 2)
            {
                descriptionBox.Text = "АДТ расположены на приводах антенн по две группы из трёх датчиков на каждом приводе." +
                    " Привод состоит из модулей Y и Z, на модуле установлено по одной группе датчиков соответственно." +
                    " В ходе алгоритма диагностики АДТ определяется исправность датчиков и средняя температура модуля. Введите 6 значений датчиков панели, а также их статус:" +
                    "'1' - датчик работоспособный, '2' - датчик отложенный, '3' - датчик неработоспособный.";
            }
            else if (listOfAlgorithms.SelectedIndex == 3)
            {
                descriptionBox.Text = "АДТ расположены на коллекторных трубах (две группы) и по одной группе на каждом из трех РТО. В ходе алгоритма диагностики АДТ определяется" +
                    " исправность датчиков и средняя температура в группах. В ходе алгоритма диагностики АДТ определяется исправность датчиков и средняя температура модуля. Введите 24" +
                    " значения датчиков панели, а также их статус:" +
                    "'1' - датчик работоспособный, '2' - датчик отложенный, '3' - датчик неработоспособный.";
            }
            RandomGenButton.Visible = true;
            RandomGenButton.Enabled = true;
            RandomGenButton.Checked = false;
            inputDataBox.Text = DescriptionDataBox;
            startButton.Visible = true;
            resultBox.Clear();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void inputDataBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void inputDataBox_Click(object sender, EventArgs e)
        {
            inputDataBox.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resultBox.Text += Environment.NewLine + path;
        }

        private void RandomGenButton_CheckedChanged(object sender, EventArgs e)
        {
            if (RandomGenButton.Checked)
            {
                switch (listOfAlgorithms.SelectedIndex)
                {
                    case 0:
                        {
                            k = 9;
                            PanelSensors = new double[k];
                            WorkablePanelSensors = new int[k];
                            AverageTempGroup = new double[k / 3];
                            RandomFillArrays(k, PanelSensors, WorkablePanelSensors);
                            break;
                        }
                    case 1:
                        {
                            k = 12;
                            PanelSensors = new double[k];
                            WorkablePanelSensors = new int[k];
                            AverageTempGroup = new double[k / 3];
                            RandomFillArrays(k, PanelSensors, WorkablePanelSensors);
                            break;
                        }
                    case 2:
                        {
                            k = 6;
                            PanelSensors = new double[k];
                            WorkablePanelSensors = new int[k];
                            AverageTempGroup = new double[k / 3];
                            RandomFillArrays(k, PanelSensors, WorkablePanelSensors);
                            break;
                        }
                    case 3:
                        {
                            k = 24;
                            PanelSensors = new double[k];
                            WorkablePanelSensors = new int[k];
                            AverageTempGroup = new double[k / 3];
                            RandomFillArrays(k, PanelSensors, WorkablePanelSensors);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        private void RandomFillArrays(int k, double[] PanelSensors, int[] WorkablePanelSensors)
        {
            int temp;
            double minimumTempreture=0;
            double maximumTempreture=0;
            double dtADT=0;
            randomTempretureRange = new Random();
            randomGroupRange = new Random();
            inputDataBox.Clear();
            switch (k)
            {
                case (9):
                    {
                            minimumTempreture = ConstForRandomGen["CarcassB_Min"];
                            maximumTempreture = ConstForRandomGen["CarcassB_Max"];
                            dtADT = ConstForRandomGen["CarcassB_dtADT"];
                            break;
                    }
                case (12):
                    {
                        minimumTempreture = ConstForRandomGen["BKUPI_AFAS_Min"];
                        maximumTempreture = ConstForRandomGen["BKUPI_AFAS_Max"];
                        dtADT = ConstForRandomGen["BKUPI_AFAS_dtADT"];
                        break;
                    }
                case (6):
                    {
                        minimumTempreture = ConstForRandomGen["BKUPI_AFAS_Min"];
                        maximumTempreture = ConstForRandomGen["BKUPI_AFAS_Max"];
                        dtADT = ConstForRandomGen["BKUPI_AFAS_dtADT"];
                        break;
                    }
                case (24):
                    {
                        minimumTempreture = ConstForRandomGen["AFAR_Min"];
                        maximumTempreture = ConstForRandomGen["AFAR_Max"];
                        dtADT = 6.5;
                        break;
                    }
                default:
                    break;
            }
            for (int i = 0; i < k; i= i+3)
            {
                temp = randomTempretureRange.Next((int)minimumTempreture, (int)maximumTempreture);
                PanelSensors[i] = temp + randomGroupRange.NextDouble() * 2*dtADT - dtADT;
                PanelSensors[i+1] = temp + randomGroupRange.NextDouble() * 2 * dtADT - dtADT;
                PanelSensors[i+2] = temp + randomGroupRange.NextDouble() * 2 * dtADT - dtADT;
                Math.Round(PanelSensors[i], 2);
                Math.Round(PanelSensors[i+1], 2);
                Math.Round(PanelSensors[i+2], 2);
            }
            for (int i = 0; i < k; i++)
            {
                WorkablePanelSensors[i] = 1;
                inputDataBox.Text += PanelSensors[i].ToString() + " " + WorkablePanelSensors[i] + Environment.NewLine;
            }

        }
        private void FillProgressBar(int k)
        {
            progressDiagnostic.Refresh();
            progressDiagnostic.Minimum = 1;
            progressDiagnostic.Maximum = k;
            progressDiagnostic.Value = 1;
            progressDiagnostic.Step = 1;
            for (int i = 0; i < k; i++)
            {
                progressDiagnostic.PerformStep();
            }
        }
        private void ControlHeaters(double[] AverageTempGroup, int k, double max = 0, double min = 0 )
        {
            switch (k)
            {
                case (9):
                    {
                        double enumMAx = Convert.ToDouble(CarcassB_Heaters.TmaxABS);
                        double enumMin1 = Convert.ToDouble(CarcassB_Heaters.Tmin1);
                        double enumMin2 = Convert.ToDouble(CarcassB_Heaters.Tmin2);
                        double enumMin3 = Convert.ToDouble(CarcassB_Heaters.Tmin3);
                        if (max == 55 )
                        {
                            double second_max = 0;
                            for (int i = 0; i < k/3; i++)
                            {
                                if (AverageTempGroup[i] > second_max && AverageTempGroup[i]!=55)
                                {
                                    second_max = AverageTempGroup[i];
                                }
                            }
                            max = second_max;
                        }
                        if (max > enumMAx)
                        {
                            resultBox.Text += Environment.NewLine + "Максимальное значение средней температуры" +
                                " одной из групп датчиков панели > " + CarcassB_Heaters.TmaxABS.ToString() + ". Выдается команда управления на выключение" +
                                "всех групп нагревателей - " + ControlCommands["OFFH"];
                        }
                        if (min<enumMin1)
                        {
                            resultBox.Text += Environment.NewLine + "Минимальное значение средней температуры групп панели < " + CarcassB_Heaters.Tmin1.ToString() +
                            " Выдается команда на включение 3-ех групп нагревателей - " + ControlCommands["ONFD"];
                        }
                        else if (min<enumMin2)
                        {
                            resultBox.Text += Environment.NewLine + "Минимальное значение средней температуры групп панели < " + CarcassB_Heaters.Tmin2.ToString() +
                            " Выдается команда на включение 1-ой и 2-ой групп нагревателей - " + ControlCommands["ONFD"];
                        }
                        else if (min<enumMin3)
                        {
                            resultBox.Text += Environment.NewLine + "Минимальное значение средней температуры групп панели < " + CarcassB_Heaters.Tmin3.ToString() +
                            " Выдается команда на включение 1 группы нагревателей - " + ControlCommands["ONFD"];
                        }
                        break;
                    }
                case (12):
                    {
                        double enumBKUPI_minh = Convert.ToDouble(BkupiAFAS_Heaters.Tonh);
                        double enumBKUPI_maxh = Convert.ToDouble(BkupiAFAS_Heaters.Tofh);
                        for (int i = 0; i < k/3; i++)
                        {
                            if (AverageTempGroup[i] != -1)
                            {
                                if (AverageTempGroup[i] < enumBKUPI_minh)
                                {
                                    resultBox.Text += Environment.NewLine + "Средняя температура привода < " + BkupiAFAS_Heaters.Tonh.ToString() + ". Выдается команда управления на включение " +
                                        "электронагревателей привода - " + ControlCommands["ONFD"] + Environment.NewLine;
                                }
                                else if (AverageTempGroup[i] > enumBKUPI_maxh)
                                {
                                    resultBox.Text += Environment.NewLine + "Средняя температура привода  >" + BkupiAFAS_Heaters.Tonh.ToString() + ". Выдается команда управления на выключение " +
                                       "электронагревателей привода - " + ControlCommands["OFFH"] + Environment.NewLine;
                                }
                            }
                            else
                            {
                                resultBox.Text += "\t Датчики вышли из строя. Управление электронагревателями"+ (i+1).ToString()+ " привода невозможное\t";
                            }
                        }
                        break;
                    }
                case (6):
                    {
                        double enumAFAS_minh = Convert.ToDouble(BkupiAFAS_Heaters.Tonh);
                        double enumAFAS_maxh = Convert.ToDouble(BkupiAFAS_Heaters.Tofh);
                        for (int i = 0; i < k/3; i++)
                        {
                            if (AverageTempGroup[i]!= -1)
                            {
                                if (AverageTempGroup[i] < enumAFAS_minh)
                                {
                                    resultBox.Text += Environment.NewLine + "Средняя температура привода < " + BkupiAFAS_Heaters.Tonh.ToString() + ". Выдается команда управления на включение " +
                                        "электронагревателей привода - " + ControlCommands["ONFD"] + Environment.NewLine;
                                }
                                else if (AverageTempGroup[i] > enumAFAS_maxh)
                                {
                                    resultBox.Text += Environment.NewLine + "Средняя температура привода  >" + BkupiAFAS_Heaters.Tonh.ToString() + ". Выдается команда управления на выключение " +
                                       "электронагревателей привода - " + ControlCommands["OFFH"] + Environment.NewLine;
                                }
                            }
                            else
                            {
                                resultBox.Text += "\t Датчики вышли из строя. Управление электронагревателями " + (i+1).ToString() + " привода невозможное\t";
                            }
                            
                        }
                        break;
                    }
                case (24):
                    {
                        double enumAFARBAO_minh = Convert.ToDouble(AFARBAO_Heaters.Tonh);
                        double enumAFARBAO_maxh = Convert.ToDouble(AFARBAO_Heaters.Toff);
                        for (int i = 0; i < k/3; i++)
                        {
                            if (AverageTempGroup[i]!= -1)
                            {
                                if (AverageTempGroup[i] < enumAFARBAO_minh)
                                {
                                    resultBox.Text += Environment.NewLine+ "Средняя температура групп датчиков на панели < " + BkupiAFAS_Heaters.Tonh.ToString() + ". Выдается команда управления на включение " +
                                        "электронагревателей, расположенных на панели - " + ControlCommands["ONFD"] + Environment.NewLine;
                                }
                                else if (AverageTempGroup[i] > enumAFARBAO_maxh)
                                {
                                    resultBox.Text += Environment.NewLine + "Средняя температура групп датчиков на панели >" + BkupiAFAS_Heaters.Tonh.ToString() + ". Выдается команда управления на выключение " +
                                       "электронагревателей, расположенных на панели - " + ControlCommands["OFFH"] + Environment.NewLine;
                                }
                            }
                            else
                            {
                                resultBox.Text += Environment.NewLine + "\t Датчики вышли из строя. Управление электронагревателями " + (i+1).ToString() + " панели невозможно\t";
                            }
                            
                        }
                        break;
                    }

                default:
                    break;
            }
        }

        private void resultBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
