using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformuniversity
{
    static class Program
    {
        public static Int32 intDropStatis = 0;
        public static string intID = "", strStatus = "Null";
        public static bool connect = false;
        private static Mutex _instanse;
        //Тут написать как называется модуль тип где C#
        private const string _app_Name = "Universitet_Bekov";


        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool Create_app;
            _instanse = new Mutex(true, _app_Name, out Create_app);
            if (Create_app)
            {
                try
                {
                    //В случае если процесс не найден в системе
                    //создается экзепляр класс конфигурации подключения
                    //к источнику данных
                    Configuration_class configuration = new Configuration_class();
                    configuration.SQL_Server_Configuration_get();
                    //Попытка открыть подключение к источнику данных
                    Configuration_class.connection.Open();
                    connect = true;
                }
                catch
                {
                  //  Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //Connection_Form connection = new Connection_Form();
                    //connection.ShowDialog();
                }
                finally
                {
                    Configuration_class.connection.Close();
                    switch (connect)
                    {
                        case false:

                            MessageBox.Show("Ошибка подключения к исчточнику данных", "Внимание",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            Environment.Exit(0);
                            break;
                        case true:
                            try
                            {
                                Application.EnableVisualStyles();
                                Application.SetCompatibleTextRenderingDefault(false);
                                //Та форма которая буд запус
                                Application.Run(new Form8());
                            }
                            catch
                            {

                            }
                            break;
                    }
                }
            }

        }
    }
}
