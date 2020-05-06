using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace winformuniversity
{
    class Procedure_Class
    {
        SqlCommand command = new SqlCommand("", Configuration_class.connection);
        /// <summary>
        /// Метод обращения к любой хранимой процедуре
        /// </summary>
        /// <param name="Procedure_name"></param>водимое название процедуры бд
        /// <param name="filed_value"></param>не типизированнная коллекция значений приложения
        public void procedure_Execution(string Procedure_name, ArrayList fileld_value)
        {
            //Запрос на вывод списка параметров, процедуры
            Table_Class table = new Table_Class(string.Format("select name from sys.parameters where object_id = (select object_id from sys.procedures where name = '{0}')", Procedure_name));
            try
            {
                //Настройка SqLCommand для работы с хранимыми процедурами
                command.CommandType = CommandType.StoredProcedure;
                //Присвоение в тексте команды названия хранимой процедуре
                command.CommandText = string.Format("[dbo].[{0}]", Procedure_name);
                //отчистка параметров
                command.Parameters.Clear();
                for (int i = 0; i < table.table.Rows.Count; i++)
                {
                    //
                    command.Parameters.AddWithValue(table.table.Rows[i][0].ToString(),
                        fileld_value[i]);
                }
                //открытие подключения
                Configuration_class.connection.Open();
                //оюъявление событий на перехват сообщений из бд
                Configuration_class.connection.InfoMessage += Connection_InfoMessege;
                //выполнение запросы
                command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                //закрытие подключения
                Configuration_class.connection.Close();
            }
        }
        /// <summary>
        /// Обработка событий о полуении сообщения с сервером БД
        /// </summary>
        /// <param name="sender"></param>ссылка на объект
        /// <param name="e"></param>аргумент сообщения сервера
        private void Connection_InfoMessege(object sender, SqlInfoMessageEventArgs e)
        {
            //Вывод сообщения с сервера в диалоговом окне
            System.Windows.Forms.MessageBox.Show(e.Message);
            //Снятие с события обработчика метода
            Configuration_class.connection.InfoMessage -= Connection_InfoMessege;
        }
        public string fDostup(string login, string password)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT [Dostup] FROM [dbo].[Admin] WHERE [Login_Admin] = '" + login + "' AND [Password_Admin] = '" + password + "'";
            Configuration_class.connection.Open();
            string Dostup = command.ExecuteScalar().ToString();
            Configuration_class.connection.Close();
            return (Dostup);
        }
    }
}
