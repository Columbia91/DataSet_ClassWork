using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Student.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. База данных создана
            DataSet studentsDB = new DataSet("StudentsDB");

            // 2. Таблица БД
            DataTable students = new DataTable("Students");
            DataTable genders = new DataTable("Genders");

            // 3. Колонки
            InitStudentTable(ref students);

            InitGenderTable(ref genders);

            // добавление в базу
            //studentsDB.Tables.Add(students);
            //studentsDB.Tables.Add(genders);

            ForeignKeyConstraint FK_Gender = new ForeignKeyConstraint
                (genders.Columns["Id"], students.Columns["GenderId"]);
            //{
            //    DeleteRule = Rule.Cascade,
            //    UpdateRule = Rule.Cascade
            //};
            students.Constraints.Add(FK_Gender);

            studentsDB.Tables.AddRange(new DataTable[]
            {
                genders,
                students
            });

            #region Write record
            DataRow newRow = genders.NewRow();
            newRow["Name"] = "Мужской";
            genders.Rows.Add(newRow);

            newRow = genders.NewRow();
            newRow["Name"] = "Женский";
            genders.Rows.Add(newRow);
            genders.WriteXml("genders.xml");
            #endregion
        }

        private static void InitGenderTable(ref DataTable genders)
        {
            DataColumn id = new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AllowDBNull = false,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                Caption = "Идентификатор",
                Unique = true,
                ReadOnly = true
            };
            DataColumn name = new DataColumn("Name", typeof(string))
            {
                Caption = "Гендер",
                MaxLength = 10,
                Unique = true
            };

            genders.Columns.AddRange(new DataColumn[]
            {
                id, name
            });

            genders.PrimaryKey = new DataColumn[] { genders.Columns[0] };
        }

        private static void InitStudentTable(ref DataTable students)
        {
            DataColumn id = new DataColumn("Id", typeof(int));
            id.AllowDBNull = false;
            id.AutoIncrement = true;
            id.AutoIncrementSeed = 1;
            id.AutoIncrementStep = 1;
            id.Caption = "Идентификатор";
            id.Unique = true;
            id.ReadOnly = true;

            DataColumn fio = new DataColumn("FIO", typeof(string));
            fio.Caption = "ФИО студента";
            fio.MaxLength = 60;

            DataColumn genderId = new DataColumn("GenderId", typeof(int))
            {
                Caption = "Пол",
                AllowDBNull = false
            };

            students.Columns.AddRange(new DataColumn[]
            {
                id,
                fio,
                genderId
            });

            students.PrimaryKey = new DataColumn[] { students.Columns["Id"] }; // вместо ID можно 0
        }
    }
}