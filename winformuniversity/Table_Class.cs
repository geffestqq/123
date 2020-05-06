using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace winformuniversity
{
    class Table_Class
    {
        public DataTable table = new DataTable();
        //локальная переменная
        private SqlCommand command = new SqlCommand("", Configuration_class.connection);
        //глобальная переменная организации зависимости и прослушивания сервера
        public SqlDependency Dependency = new SqlDependency();
        /// <summary>
        /// Зполнение DataTAble в зависимосьти от введенного SQL запроса
        /// </summary>
        /// <param name="SQL_Select_Query"></param>
        public Table_Class(string SQL_Select_Query)
        {
            command.Notification = null;//отключение оповещений у команды
            command.CommandText = SQL_Select_Query;//присвоение SQL запроса
            Dependency.AddCommandDependency(command);//присвоение команды в связку
            //прослушивание
            try
            {
                //запуск прослушивания
                SqlDependency.Start(Configuration_class.connection.ConnectionString);
                //открытия подключения
                Configuration_class.connection.Open();
                // записать данные в табличном виде в виртуальной таблице
                table.Load(command.ExecuteReader());
            }
            catch (Exception ex)
            {
                //Вывод сообщения от ошибке
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                //Закрытие подключения
                Configuration_class.connection.Close();
            }
        }

    }
}