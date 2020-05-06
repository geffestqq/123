using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace winformuniversity
{
    class Configuration_class
    {
        public event Action<DataTable> Server_Collection;
        //получает колекцию достпнх серверов
        public event Action<DataTable> Data_Base_Collection;
        //получает колекцю достпных БД на сервере
        public event Action<bool> Conection_Checked;
        //определяет статус подключения
        public string DS = "Empty", //переменная Data Source
            IC = "Empty"; //переменная initial Catalog
        public string ds = ""; //проверка подключения Data Source
        public static SqlConnection connection = new SqlConnection();
        /// <summary>
        /// Метод получения информации о строке подключения к БД
        /// </summary>
        public void SQL_Server_Configuration_Get()
        {
            //Создание каталога в одном из еорней реестра ОС
            RegistryKey registry = Registry.CurrentUser;
            //Создает папку в выбраном корневом каталоге в реестре ОС
            RegistryKey key = registry.CreateSubKey("Server_Configuration");
            try
            {
                //Пытаюсь получить значения из переменных в реестре
                DS = key.GetValue("DS").ToString();
                IC = key.GetValue("IC").ToString();
            }
            catch
            {
                DS = "Empty";
                IC = "Empty";
            }
            finally
            {
                connection.ConnectionString = "Data Source = " + DS +
                    "; Initial Catalog = " + IC +
                    "; Integrated Security = true;";
            }
        }
        /// <summary>
        /// Метод обновления информации о подключении 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ic"></param>
        public void SQL_Server_Configuration_Set(string ds, string ic)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("Sever_Configuration");
            key.SetValue("DS", ds);
            key.SetValue("IC", ic);
            SQL_Server_Configuration_Get();
        }
        /// <summary>
        /// Метод возвращает список доступных серверов в локальном окружении
        /// </summary>
        public void SQL_Server_Enumurator()
        {
            SqlDataSourceEnumerator sourceEnumerator
                = SqlDataSourceEnumerator.Instance;
            Server_Collection(sourceEnumerator.GetDataSources());
        }
        /// <summary>
        /// Метод проверки подключения к источнику данных
        /// </summary>
        public void SQL_Data_Base_Checking()
        {
            connection.ConnectionString = "Data Source = " + ds + "; " +
                "Initial Catalog = master; Interated Security = True";
            try
            {
                connection.Open();
                Conection_Checked(true);
            }
            catch
            {
                Conection_Checked(false);
            }
            finally
            {
                connection.Close();
            }
        }
        public void SQL_Data_base_Collection()
        {
            SqlCommand command = new SqlCommand("select name from sys.databases " +
                "where name not in ('master','tempdb','model','msdb') " +
                "and name like 'Sale_Data_Base%'", connection);
            try
            {
                connection.Open();
                DataTable table = new DataTable();
                table.Load(command.ExecuteReader());
                Data_Base_Collection(table);
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
        }
        public static Int32 IDrecord, IDuser;
        public static string strDostup;
        public void dbEnter(string login, string password)
        {
            SqlCommand command = new SqlCommand("Data Source = DESKTOP-T3ECMD0\\GEFFEST; Initial Catalog = Universitet_Bekov; Persist Security Info = true; User ID = sa; Password = \"c2f5i4f53\"");

            command.CommandText = "SELECT count (*) FROM [dbo].[Admin]" +
                "where [Login_Admin] = '" + login + "' and [Password_Admin] = '" +
                password + "'";
            Configuration_class.connection.Open();
            IDuser = Convert.ToInt32(command.ExecuteScalar().ToString());
            Configuration_class.connection.Close();
        }


        //Строковые статические переменные название организации , путь сохранения документа, название персонального компьютера
        public static string Organization_Name,
            Save_Files_Path, Machine_Name;
        //переменные отступа в документа
        public static Int32 doc_Left_Merge,
            doc_Right_Merg,
            doc_Top_Merge, doc_Buttom_Merge;
        /// <summary>
        /// Плучение данных о конфигурации документа
        /// </summary>
        public void Document_Configuration_Get()
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key
                = registry.
                CreateSubKey("Server_Configuration");
            try
            {
                Organization_Name = key.GetValue("Organization_Name")
                    .ToString();
                doc_Left_Merge
                    = Convert.ToInt32(key
                    .GetValue("doc_Left_Merge").ToString());
                doc_Right_Merg = Convert.ToInt32(key
                    .GetValue("doc_Right_Merg").ToString());
                doc_Top_Merge = Convert.ToInt32(key
                    .GetValue("doc_Top_Merge").ToString());
                doc_Buttom_Merge = Convert.ToInt32(key
                    .GetValue("doc_Buttom_Merge").ToString());
            }
            catch
            {
                Organization_Name = "Empty";
                doc_Left_Merge = 0;
                doc_Right_Merg = 0;
                doc_Top_Merge = 0;
                doc_Buttom_Merge = 0;
            }


        }
        public void SQL_Server_Configuration_get()
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("Server_Configuration");
            try
            {
                DS = key.GetValue("DS").ToString();
                IC = key.GetValue("IC").ToString();
            }
            catch
            {
                DS = "Empty";
                IC = "Empty";
            }
            finally
            {
                //Обновление строки подключения
                connection.ConnectionString = "Data Source = " + DS + "; Initial Catalog = " + IC + "; Integrated Security = true;";
            }

        }
        
    }
}
