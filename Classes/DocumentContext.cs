using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Documents_Kylosov.Classes
{
    public class DocumentContext : Model.Document, Interfaces.IDocument
    {
        public List<DocumentContext> AllDocuments()
        {
            List<DocumentContext> allDocuments = new List<DocumentContext>();
            OleDbConnection connection = Common.DBConnection.Connection();
            OleDbDataReader dataDocuments = Common.DBConnection.Query("SELECT * FROM [Документы]", connection); 
            while (dataDocuments.Read())
            {
                DocumentContext newDocument = new DocumentContext();
                newDocument.id = dataDocuments.GetInt32(0);
                newDocument.src = dataDocuments.GetString(1);
                newDocument.name = dataDocuments.GetString(2);
                newDocument.user = dataDocuments.GetString(3);
                newDocument.id_document = dataDocuments.GetString(4);
                newDocument.date = dataDocuments.GetDateTime(5);
                newDocument.status = dataDocuments.GetInt32(6);
                newDocument.vector = dataDocuments.GetString(7);
                allDocuments.Add(newDocument);
            }
            Common.DBConnection.CloseConnection(connection);
            return allDocuments;
        }

        public List<DocumentContext> AllUsers()
        {
            List<DocumentContext> AllUsers = new List<DocumentContext>();
            OleDbConnection connection = Common.DBConnection.Connection();
            OleDbDataReader dataDocuments = Common.DBConnection.Query("SELECT * FROM [Ответственные]", connection);
            while (dataDocuments.Read())
            {
                DocumentContext newDocument = new DocumentContext();
                newDocument.id = dataDocuments.GetInt32(0);
                newDocument.user = dataDocuments.GetString(1);
                AllUsers.Add(newDocument);
            }
            Common.DBConnection.CloseConnection(connection);
            return AllUsers;
        }

        public void Save(bool Update = false, bool User = false)
        {
            OleDbConnection connection = Common.DBConnection.Connection();
            if (!User)
            {
                if (Update)
                    Common.DBConnection.Query("UPDATE [Документы] " +
                                            "SET " +
                                            $"[Изображение] = '{this.src}', " +
                                            $"[Наименование] = '{this.name}',  " +
                                            $"[Ответственный] = '{this.user}', " +
                                            $"[Код документа] = '{this.id_document}', " +
                                            $"[Дата поступления] = '{this.date.ToString("dd.MM.yyyy")}', " +
                                            $"[Статус] = '{this.status}', " +
                                            $"[Направление] = '{this.vector}' " +
                                            $"WHERE [Код] = {this.id}", connection);

                
                else
                    Common.DBConnection.Query("INSERT INTO [Документы]" +
                                            $"([Изображение], [Наименование], [Ответственный]," +
                                            $"[Код документа], [Дата поступления], [Статус], " +
                                            $"[Направление]) VALUES (" +
                                            $"'{this.src}', '{this.name}', '{this.user}'," +
                                            $"'{this.id_document}', '{this.date.ToString("dd.MM.yyyy")}', '{this.status}', '{this.vector}')"
                                            , connection);
            }
            else
                if(Update)
                Common.DBConnection.Query("UPDATE [Ответственные] SET " +
                        $"[ФИО] = '{this.user}' " +
                        $"WHERE [Код] = {this.id}", connection);
                else
                Common.DBConnection.Query("INSERT INTO [Ответственные]" +
                        $"([ФИО]) VALUES (" +
                        $"'{this.user}')"
                        , connection);
            Common.DBConnection.CloseConnection(connection);
        }
        public void Delete(bool User = false)
        {
            OleDbConnection connection = Common.DBConnection.Connection();
            if (!User)
                Common.DBConnection.Query($"DELETE FROM [Документы] WHERE [Код] = {this.id}", connection);

            else
                Common.DBConnection.Query($"DELETE FROM [Ответственные] WHERE [Код] = {this.id}", connection);

            Common.DBConnection.CloseConnection(connection);
        }
    }
}
